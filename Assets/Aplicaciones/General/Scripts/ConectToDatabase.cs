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

        //lso 2 tipos de usuarios que pueden estar loggeados en la aplicaci�n
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

        public void setCurrentPaciente(Paciente nuevoPaciente)
        {
            currentPaciente = nuevoPaciente;
        }

        public void resetCurrentPaciente()
        {
            currentPaciente = new Paciente();
        }

        public Paciente getCurrentPaciente()
        {
            return currentPaciente;
        }

        //en este script crearemos todas las funciones que tengan que ver con conectarse a la BBDD
        //los par�metros los pasaremos cuando llamemos a esta funci�n en el men� de login
        public async Task CreateUser(string email, string password, string nombre, string apellidos, bool esMedico)
        {
            if (esMedico)
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

                    //adem�s, a�adimos este usuario a nuestra base de datos
                    initializeNewTerapeuta(result.User.UserId, email, nombre, apellidos);
                });
            }
        }

        public void CreatePaciente(string id, string nombre, string apellidos)
        {
            initializeNewPaciente(id, nombre, apellidos, currentMedico.id, currentMedico.nombre);
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

                //ahora hacer lo de antes aqu�
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
                            //establecemos el objeto 'currentMedico' con el usuario actual para poder usuarlo en el resto de la aplicaci�n
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

                //ahora hacer lo de antes aqu�
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
                            //establecemos el objeto 'currentPaciente' con el usuario actual para poder usuarlo en el resto de la aplicaci�n
                            currentPaciente = currentPaciente.DictionaryToPaciente(documentDictionary);
                            //currentPaciente.printValues();
                        }
                    }

                });
            });
        }

        public async Task SaveData()
        {
            if (esMedico)
            {
                //guardamos los datos del m�dico
                DocumentReference docRef = db.Collection("Medicos").Document(currentMedico.id);

                await docRef.UpdateAsync(currentMedico.returnDatosMedico()).ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("Proceso de 'Guardar datos del m�dico' cancelado: " + task.Exception);
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("Proceso de 'Guardar datos del m�dico' encontr� un error: " + task.Exception);
                        return;
                    }

                    Debug.Log("M�dico " + currentMedico.id + "actualizado con �xito.");
                });

            }
            else
            {
                //guardamos los datos del paciente
                DocumentReference docRef = db.Collection("Pacientes").Document(currentPaciente.id);

                await docRef.UpdateAsync(currentPaciente.returnDatosPaciente()).ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("Proceso de 'Guardar datos del paciente' cancelado: " + task.Exception);
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("Proceso de 'Guardar datos del paciente' encontr� un error: " + task.Exception);
                        return;
                    }

                    Debug.Log("Paciente actualizado con �xito.");
                });
            }
        }


        public void LogOut()
        {
            //Para salir de la sesi�n de un usuario
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
            Debug.Log(auth);

            if (auth != null || auth.CurrentUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void initializeNewTerapeuta(string id, string email, string nombre, string apellidos)
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
                    Debug.LogError("Proceso de 'A�adir un nuevo m�dico' cancelado: " + task.Exception);
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("Proceso de 'A�adir un nuevo m�dico' encontr� un error: " + task.Exception);
                    return;
                }

                Debug.Log("M�dico " + id + "a�adido con �xito a la colecci�n M�dicos");
            });
        }

        private void initializeNewPaciente(string id, string nombre, string apellidos, string idMedico, string nombreMedico)
        {
            Debug.Log("Soy un paciente");
            DocumentReference docRef = db.Collection("Pacientes").Document(id);

            Paciente nuevoPaciente = currentMedico.addNuevoPaciente(id, nombre, apellidos, idMedico, nombreMedico);

            //UpdateAsync lo usaremos cuando queramos modificar datos
            docRef.SetAsync(nuevoPaciente.returnDatosPaciente()).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("Proceso de 'A�adir un nuevo paciente' cancelado: " + task.Exception);
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("Proceso de 'A�adir un nuevo paciente' encontr� un error: " + task.Exception);
                    return;
                }
                //no hace esta funcion
                Debug.Log("ANTES DE A�ADIR EL PACIENTE AL TERAPEUTA DE LA BBDD");
                addPacienteToTerapeuta();
                Debug.Log("Paciente a�adido con �xito a la colecci�n Pacientes");
            });
        }

        private void addPacienteToTerapeuta()
        {
            Debug.Log("nuevo paciente: " + currentMedico.pacientes.Count);

            DocumentReference docRef = db.Collection("Medicos").Document(currentMedico.id);
            docRef.UpdateAsync(currentMedico.returnDatosMedico()).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("Proceso de 'Guardar datos del m�dico' cancelado: " + task.Exception);
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("Proceso de 'Guardar datos del m�dico' encontr� un error: " + task.Exception);
                    return;
                }

                Debug.Log("M�dico " + currentMedico.id + "actualizado con �xito.");
            });

            currentMedico.printValues();
        }

        public async Task<List<Paciente>> getAllPacientes()
        {
            List<Paciente> allPacientes = new List<Paciente>();

            CollectionReference pacientesRef = db.Collection("Pacientes");

            await pacientesRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {

                if (task.IsCompleted)
                {
                    QuerySnapshot snapshot = task.Result;
                    foreach (DocumentSnapshot document in snapshot.Documents)
                    {
                        //almacenamos los pacientes
                        Paciente pacienteAux = new Paciente();
                        Dictionary<string, object> documentDictionary = document.ToDictionary();
                        pacienteAux = pacienteAux.DictionaryToPaciente(documentDictionary);
                        //pacienteAux.printValues();
                        allPacientes.Add(pacienteAux);
                    }
                }
                else
                {
                    Debug.LogError("Error getting documents: " + task.Exception);
                }
            });

            return allPacientes; //est� mandando el array vac�o
        }

        //en esta versi�n, el usuario s�lo va a ser el terapeuta
        public async Task getActualUser()
        {
            //hacemos lo mismo para el m�dico
            CollectionReference medicosRef = db.Collection("Medicos");
            await medicosRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {

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
    }
}

