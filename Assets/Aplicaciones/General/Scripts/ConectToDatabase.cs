using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;


namespace General
{
    //LO HAREMOS 'PersistentSingleton' EN VEZ DE MONOBEHAVIOUR PARA QUE PODAMOS ACCEDER A LOS DATOS DESDE TODA LA APLICACIÓN
    public class ConectToDatabase : PersistentSingleton<ConectToDatabase>
    {
        Firebase.Auth.FirebaseAuth auth;
        FirebaseFirestore db;

        void Start()
        {
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            db = FirebaseFirestore.DefaultInstance;

            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    var app = Firebase.FirebaseApp.DefaultInstance;

                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                }
                else
                {
                    UnityEngine.Debug.LogError(System.String.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }

        //en este script crearemos todas las funciones que tengan que ver con conectarse a la BBDD

        //los parámetros los pasaremos cuando llamemos a esta función en el menú de login
        public void CreateUser(string email, string password, bool esMedico)
        {
            auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // Firebase user has been created.
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                 result.User.DisplayName, result.User.UserId);

                //además, añadimos este usuario a nuestra base de datos
                initializeNewUser(result.User.UserId, email, esMedico);

                Debug.Log("HOLAAAAAAAA");

            });
        }

        public void Login(string email, string password)
        {

            if(auth.CurrentUser != null)
            {
                Debug.Log("Ya hay un usuario loggeado!");
                return;
            }

            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);
            });

        }

        public void LogOut()
        {
            //Para salir de la sesión de un usuario
            Debug.LogFormat("User log out in succesfully: " + auth.CurrentUser.UserId);
            auth.SignOut();
        }

        public string getCurrentUserID()
        {
            Firebase.Auth.FirebaseUser user = auth.CurrentUser;
            string uid = null;
            if (user != null)
            {
                //string name = user.DisplayName;
                //string email = user.Email;

                // The user's Id, unique to the Firebase project.
                // Do NOT use this value to authenticate with your backend server, if you
                // have one; use User.TokenAsync() instead.
                uid = user.UserId;
            }

            return uid;
        }

        private void initializeNewUser(string id, string email, bool esMedico)
        {
            //además, añadimos este usuario a nuestra base de datos
            //indicar si es médico o paciente en el Login
            if (esMedico)
            {
                Debug.Log("Soy un medico");
                DocumentReference docRef = db.Collection("Medicos").Document(id);

                Medico nuevoMedico = new Medico(id, email);
                //Debug.Log(nuevoMedico);
                docRef.SetAsync(nuevoMedico.returnDatosMedico()).ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("Proceso de 'Añadir un nuevo médico' cancelado: " + task.Exception);
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("Proceso de 'Añadir un nuevo médico' encontró un error: " + task.Exception);
                        return;
                    }

                    Debug.Log("Médico " + id + "añadido con éxito a la colección Médicos");
                });
            }
            else
            {
                Debug.Log("Soy un paciente");
                DocumentReference docRef = db.Collection("Pacientes").Document(id);

                Paciente nuevoPaciente = new Paciente(id, email);
               // Debug.Log(nuevoPaciente);
                docRef.SetAsync(nuevoPaciente.returnDatosPaciente()).ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("Proceso de 'Añadir un nuevo paciente' cancelado: " + task.Exception);
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("Proceso de 'Añadir un nuevo paciente' encontró un error: " + task.Exception);
                        return;
                    }

                    Debug.Log("Paciente " + id + "añadido con éxito a la colección Pacientes");
                });
            }
        }

    }
}

