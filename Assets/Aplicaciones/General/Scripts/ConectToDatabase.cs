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
        private Sesion currentSesion; //currentSesion siempre va a ser la �ltima sesi�n de la lista de sesiones de currentPaciente

        private bool esMedico; //variable para saber si tenemos que usar currentMedico o currentPaciente
        public bool usuarioCorrecto;
        private bool contarTiempoSesion;
        private float tiempoJuego;
        private bool firstLogin = true;

        private string textoBuild;

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

            firstLogin = true;
            currentMedico = new Medico();
            currentPaciente = new Paciente();
            currentSesion = new Sesion();
            usuarioCorrecto = false;
            contarTiempoSesion = false;
            tiempoJuego = 0;
        }

        private void Update()
        {
            //contamos el tiempo (en segundos) que pasa un usuario dentro del juego
            if (contarTiempoSesion)
            {
                tiempoJuego += Time.deltaTime;
                //Debug.Log("tiempoJuego: " + tiempoJuego);
            }
        }

        public string obtenerTextoBuild()
        {
            return textoBuild;
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

        public void setCurrentSesion(Sesion nuevaSesion)
        {
            currentSesion = nuevaSesion;
        }

        public Sesion getCurrentSesion()
        {
            return currentSesion;
        }

        public void startTimeSesion()
        {
            contarTiempoSesion = true;
        }

        public void stopTimeSesion()
        {
            contarTiempoSesion = false;
            currentSesion.setDuracion((int)tiempoJuego);
            tiempoJuego = 0;
        }

        public bool loggedFirstTime()
        {
            return firstLogin;
        }

        public void userLogged()
        {
            firstLogin = false;
        }

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
                    usuarioCorrecto = false;
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    textoBuild = "Cancelado loggeo";
                    return;
                }
                if (task.IsFaulted)
                {
                    //esto pasa si se loggea con un usuario que no existe, y se queda en el await
                    usuarioCorrecto = false;
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    textoBuild = "Error al loggearse";
                    return;
                }

                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);

                firstLogin = false;
                usuarioCorrecto = true;
                textoBuild = "Loggeo exitoso";
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
                            textoBuild = "Loggeo exitoso";
                            //currentMedico.printValues();
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

        public async Task SaveDataTerapeuta()
        {
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

        public async Task SaveDataPaciente()
        {
            //ESTAMOS GUARDANDO LOS DATOS EN LA TABLA 'PACIENTES' NO EN EL ARRAY DE PACIENTES DEL TERAPEUTA
            SaveDataSesion();
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

        public void SaveDataSesion()
        {
            currentPaciente.sesiones[currentPaciente.sesiones.Count - 1] = currentSesion;
        }

        public void LogOut()
        {
            //Para salir de la sesi�n de un usuario
            auth.SignOut();
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

