using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Threading.Tasks;

namespace General
{
    public class ConectToDatabase : PersistentSingleton<ConectToDatabase>
    {
        Firebase.Auth.FirebaseAuth auth;
        FirebaseFirestore db;

        //lso 2 tipos de usuarios que pueden estar loggeados en la aplicación
        private Medico currentMedico;
        private Paciente currentPaciente;

        private bool esMedico; //variable para saber si tenemos que usar currentMedico o currentPaciente

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

            currentMedico = new Medico();
            currentPaciente = new Paciente();
        }

        public async Task obtenerUsuario()
        {
            await getActualUser();
            Debug.Log("USUARIO CONSEGUIDO");
        }

        public bool getEsMedico()
        {
            return esMedico;
        }

        public Medico getCurrentMedico()
        {
            return currentMedico;
        }

        public Paciente getCurrentPaciente()
        {
            return currentPaciente;
        }

        //en este script crearemos todas las funciones que tengan que ver con conectarse a la BBDD
        //los parámetros los pasaremos cuando llamemos a esta función en el menú de login
        public async Task CreateUser(string email, string password, string nombre, string apellidos, bool esMedico)
        {
            await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
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
                initializeNewUser(result.User.UserId, email, nombre, apellidos, esMedico);

            });
        }

        public async Task LoginMedico(string email, string password)
        {
            //hacemos login normal y luego establecemos el currentMedico 
            Debug.Log("Loggeandonos como PSICOLOGO");

            //buscamos en la tabla de Medicos el paciente que coincida con el ID del currentUser

            await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
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

                //ahora hacer lo de antes aquí
                esMedico = true;
                CollectionReference medicosRef = db.Collection("Medicos");
                medicosRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {

                    Debug.Log("Antes de ver los medicos");
                    QuerySnapshot snapshot = task.Result;
                    foreach (DocumentSnapshot document in snapshot.Documents)
                    {
                         if (auth.CurrentUser.UserId == document.Id)
                         {
                            Dictionary<string, object> documentDictionary = document.ToDictionary();
                            //establecemos el objeto 'currentMedico' con el usuario actual para poder usuarlo en el resto de la aplicación
                            currentMedico = currentMedico.DictionaryToMedico(documentDictionary);
                            currentMedico.printValues();
                         }
                    }

                });
            });
        }

        public async Task LoginPaciente(string email, string password)
        {
            //hacemos login normal y luego establecemos el currentPaciente 
            Debug.Log("Loggeandonos como PACIENTE");

            await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
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

                //ahora hacer lo de antes aquí
                esMedico = false;
                CollectionReference pacientesRef = db.Collection("Pacientes");
                pacientesRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {

                    Debug.Log("Antes de ver los pacientes");
                    QuerySnapshot snapshot = task.Result;
                    foreach (DocumentSnapshot document in snapshot.Documents)
                    {
                        if (auth.CurrentUser.UserId == document.Id)
                        {
                            Dictionary<string, object> documentDictionary = document.ToDictionary();
                            //establecemos el objeto 'currentPaciente' con el usuario actual para poder usuarlo en el resto de la aplicación
                            currentPaciente = currentPaciente.DictionaryToPaciente(documentDictionary);
                            currentPaciente.printValues();
                        }
                    }

                });
            });
        }

        public void LogOut()
        {
            //Para salir de la sesión de un usuario
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

        public bool isLogged()
        {
            if (auth.CurrentUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void initializeNewUser(string id, string email, string nombre, string apellidos, bool esMedico)
        {
            if (esMedico)
            {
                Debug.Log("Soy un medico");
                DocumentReference docRef = db.Collection("Medicos").Document(id);

                Medico nuevoMedico = new Medico();
                nuevoMedico.initMedico(id, email, nombre, apellidos);
                
                //UpdateAsync lo usaremos cuando queramos modificar datos
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

                Paciente nuevoPaciente = new Paciente();
                nuevoPaciente.initPaciente(id, email, nombre, apellidos);

                //UpdateAsync lo usaremos cuando queramos modificar datos
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

        private async Task getActualUser()
        {
            //recorremos las 2 tablas hasta encontrar al que queremos
            bool esPaciente = false;//variable local para ver si tenemos que buscar tambiñen entre los médicos

            CollectionReference pacientesRef = db.Collection("Pacientes");
            await pacientesRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {

                QuerySnapshot snapshot = task.Result;
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (auth.CurrentUser.UserId == document.Id)
                    {
                        esPaciente = true;
                        esMedico = false; //la variable global de la clase

                        Dictionary<string, object> documentDictionary = document.ToDictionary();
                        currentPaciente = currentPaciente.DictionaryToPaciente(documentDictionary);
                    }
                }

                if (!esPaciente)
                {
                    //hacemos lo mismo para el médico
                    CollectionReference medicosRef = db.Collection("Medicos");
                    medicosRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {

                        QuerySnapshot snapshot = task.Result;
                        foreach (DocumentSnapshot document in snapshot.Documents)
                        {
                            if (auth.CurrentUser.UserId == document.Id)
                            {
                                esMedico = true;//la variable global de la clase

                                Dictionary<string, object> documentDictionary = document.ToDictionary();
                                currentMedico = currentMedico.DictionaryToMedico(documentDictionary);
                            }
                        }

                    });
                }

            });


        }

    }
}

