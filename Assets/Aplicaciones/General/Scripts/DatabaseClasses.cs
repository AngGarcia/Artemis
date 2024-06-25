using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public enum EstadoPaciente
    {
        //sin ansiedad, con un poco de ansiedad, con un poco más de ansiedad, con bastante ansiedad, con mucha ansiedad, con ansiedad extrema.
        Sin_ansiedad = 0,
        un_poco_de_ansiedad = 1,
        algo_de_ansiedad = 2,
        bastante_ansiedad = 3,
        mucha_ansiedad = 4,
        ansiedad_extrema = 5
    }

    public struct Test
    {
        public string momento;
        public EstadoPaciente estado;

        public Dictionary<string, object> TestToDictionary()
        {
            Dictionary<string, object> test = new Dictionary<string, object>
            {
                { "momento", momento },
                { "estado", estado }
            };

            return test;
        }

        public Test DictionaryToTest(object dictionaryAux)
        {
            Dictionary<string, object> dictionary = dictionaryAux as Dictionary<string, object>;
            Debug.Log("Diccionario a test?: " + dictionary);

            Test test = new Test();
            test.momento = dictionary["momento"].ToString();
            test.estado = (EstadoPaciente)int.Parse(dictionary["estado"].ToString());

            return test;
        }
    }

    public class Sesion
    {
        private string fecha; //fecha de realización
        public string duracion; //tiempo en segundos de cuanto tiempo ha durado la sesión
        private string observaciones; //comentarios del terapeuta
        public List<Test> progreso; //diccionario con todos los momentos donde se debe realizar el test psicométrico; son 12
        public string numVecesPausa;

        //CADA VEZ QUE SE LE DE AL BOTÓN DE 'IR AL MAPA' EN LA LISTA DE PACIENTES, SE INICIARÁ UNA NUEVA SESIÓN
        public void initSesion()
        {
            setFecha(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);//lo inicializamos a la del día que se crea la sesión
            setDuracion(0f);
            observaciones = "";
            numVecesPausa = "0";
            progreso = new List<Test>();
            addNuevoEstado("Comienzo", EstadoPaciente.bastante_ansiedad); //de pruebas
        }

        public void setObservaciones(string texto)
        {
            observaciones = texto;
        }

        public string getObservaciones()
        {
            return observaciones;
        }

        public void setFecha(int day2, int month2, int year2)
        {
            DateTime fechaAux = new DateTime(year2, month2, day2);

            int day = fechaAux.Day;
            int month = fechaAux.Month;
            int year = fechaAux.Year;

            fecha = day + "/" + month + "/" + year;
            Debug.Log("fecha: " + fecha);
        }

        public string getFecha()
        {
            return fecha;
        }

        public void setDuracion(float segundosNum)
        {
            float minutos = 0f;
            string segundos;

            if (segundosNum > 60)
            {
                minutos = Mathf.FloorToInt(segundosNum);
                segundos = ((int)(((segundosNum - minutos) * 100) % 100)).ToString("00");
            }
            else
            {
                if (segundosNum < 10)
                {
                    segundos = "0" + segundosNum.ToString();
                }
                else
                {
                    segundos = segundosNum.ToString();
                }
            }

            duracion = minutos.ToString() + ":" + segundos;
        }

        //hacer función de pasar los segundos de duración a minutos y segundos
        public string getDuracion()
        {
            return duracion;
        }

        public void addNuevoEstado(string momento, EstadoPaciente estado)
        {
            Test testAux = new Test();

            //momento y estado se deben pasar por parámetros
            testAux.momento = momento;
            testAux.estado = estado;
            progreso.Add(testAux);
        }

        public Dictionary<string, object> SesionToDictionary()
        {
            //hacer la conversión de progreso primero a un diccionario
            Dictionary<string, object> progresoAux = new Dictionary<string, object>();

            for (int i = 0; i < progreso.Count; i++)
            {
                progresoAux.Add((i + 1).ToString(), progreso[i].TestToDictionary());
            }

            Dictionary<string, object> sesion = new Dictionary<string, object>
            {
                { "fecha" , getFecha() },
                { "duracion", getDuracion() },
                { "observaciones", observaciones },
                { "numVecesPausa", numVecesPausa },
                { "progreso", progresoAux }
            };

            return sesion;
        }

        public Sesion DictionaryToSesion(object dictionaryAux)
        {
            Dictionary<string, object> dictionary = dictionaryAux as Dictionary<string, object>;
            Debug.Log("Diccionario a sesion?: " + dictionary);

            Sesion sesion = new Sesion();
            sesion.progreso = new List<Test>();

            sesion.fecha = dictionary["fecha"].ToString();
            sesion.duracion = dictionary["duracion"].ToString();
            sesion.observaciones = dictionary["observaciones"].ToString();
            sesion.numVecesPausa = dictionary["numVecesPausa"].ToString();

            Dictionary<string, object> diccionario = dictionary["progreso"] as Dictionary<string, object>;

            foreach (KeyValuePair<string, object> entrada in diccionario)
            {
                Test testAux = new Test();
                testAux = testAux.DictionaryToTest(entrada.Value);
                sesion.progreso.Add(testAux);
            }

            Debug.Log("Tamaño del progreso: " + sesion.progreso.Count);

            return sesion;
        }

        public void printSesionValues()
        {
            Debug.Log("Fecha: " + fecha);
            Debug.Log("Duración: " + duracion);
            Debug.Log("Observaciones: " + "");
        }

    }


    public class Paciente
    {
        public string id = "";
        public bool esMedico = false;
        public string idMedico = "";
        public string medicoAsignado = "";
        public string nombre = "";
        public string apellidos = "";

        public List<Sesion> sesiones;
        public Dictionary<int, bool> nivelesSuperados;

        public void initPaciente(string id, string nombre, string apellidos, string idMedico, string medicoAsignado)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.idMedico = idMedico;
            this.medicoAsignado = medicoAsignado;
            sesiones = new List<Sesion>();
           
            addNuevaSesion(); //de prueba, para ver cómo se guardan los datos

            nivelesSuperados = new Dictionary<int, bool>
            {
                { 2, false },
                { 3, false },
                { 4, false },
                { 5, false },
                { 6, false }
            };
        }

        public void addNuevaSesion()
        {
            Sesion aux = new Sesion();
            aux.initSesion();
            sesiones.Add(aux);
        }

        public Dictionary<string, object> returnDatosPaciente()
        {
            //hacer la conversión de sesiones primero a un diccionario
            Dictionary<string, object> sesionesAux = new Dictionary<string, object>();

            for (int i=0; i < sesiones.Count; i++)
            {
                sesionesAux.Add((i+1).ToString(), sesiones[i].SesionToDictionary());
            }


            Dictionary<string, object> paciente = new Dictionary<string, object>
            {
                { "id" , id },
                { "esMedico", esMedico },
                { "idMedico" , idMedico },
                { "medicoAsignado", medicoAsignado },
                { "nombre" , nombre },
                { "apellidos" , apellidos },
                { "sesiones", sesionesAux },
                //{ "nivelesSuperados", nivelesSuperados }
            };

            return paciente;
        }

        //lo usamos para convertir los datos que nos llegan de la base de datos al tipo de clase que queremos
        public Paciente DictionaryToPaciente(object dictionaryAux)
        {
            Dictionary<string, object> dictionary = dictionaryAux as Dictionary<string, object>;
            Debug.Log("Diccionario a paciente?: " + dictionary);

            Paciente paciente = new Paciente();
            paciente.sesiones = new List<Sesion>();

            paciente.id = dictionary["id"].ToString();
            paciente.nombre = dictionary["nombre"].ToString();
            paciente.apellidos = dictionary["apellidos"].ToString();
            paciente.medicoAsignado = dictionary["medicoAsignado"].ToString();
            paciente.idMedico = dictionary["idMedico"].ToString();
            //paciente.nivelesSuperados = dictionary["nivelesSuperados"] as Dictionary<int, bool>;

            Dictionary<string, object> diccionario = dictionary["sesiones"] as Dictionary<string, object>;

            foreach (KeyValuePair<string, object> entrada in diccionario)
            {
                Sesion sesionAux = new Sesion();
                sesionAux = sesionAux.DictionaryToSesion(entrada.Value);
                paciente.sesiones.Add(sesionAux);
            }

            Debug.Log("Tamaño de sesiones: " + paciente.sesiones.Count);

            return paciente;
        }

        public void printValues()
        {
            Debug.Log("ID: " + id);
            Debug.Log("Nombre: " + nombre);
            Debug.Log("Apellidos: " + apellidos);
            Debug.Log("Médico asignado: " + medicoAsignado);
            Debug.Log("Id médico: " + idMedico);
            Debug.Log("Nº de sesiones: " + sesiones.Count);
            //Debug.Log("Niveles superados: " + nivelesSuperados);

            for (int i = 0; i < sesiones.Count; i++)
            {
                Debug.Log("Sesión " + (i + 1));
                sesiones[i].printSesionValues();
            }
        }
    }

    public class Medico
    {
        public string id = "";
        public string email = "";
        public bool esMedico = true;

        public string nombre = "";
        public string apellidos = "";

        public List<Paciente> pacientes;

        public void initMedico(string id, string email, string nombre, string apellidos)
        {
            this.id = id;
            this.email = email;
            this.nombre = nombre;
            this.apellidos = apellidos;
            pacientes = new List<Paciente>();
        }

        public Paciente addNuevoPaciente(string id, string nombre, string apellidos, string idMedico, string nombreMedico)
        {
            Paciente aux = new Paciente();
            aux.initPaciente(id, nombre, apellidos, idMedico, nombreMedico);

            pacientes.Add(aux);
            Debug.Log("Tamaño de pacientes: ");
            Debug.Log(pacientes.Count);

            return aux;
        }

        public Dictionary<string, object> returnDatosMedico()
        {
            //convertir la lista de pacientes en un diccionario
            Dictionary<string, object> pacientesAux = new Dictionary<string, object>();

            for (int i = 0; i < pacientes.Count; i++)
            {
                pacientesAux.Add((i + 1).ToString(), pacientes[i].returnDatosPaciente());
            }


            Dictionary<string, object> medico = new Dictionary<string, object>
            {
                { "id" , id },
                { "esMedico", esMedico },
                { "email" , email },
                { "nombre" , nombre },
                { "apellidos" , apellidos },
                { "pacientes", pacientesAux } 
            };

            return medico;
        }

        //lo usamos para convertir los datos que nos llegan de la base de datos al tipo de clase que queremos
        public Medico DictionaryToMedico(Dictionary<string, object> dictionary)
        {
            Medico medico = new Medico();
            medico.pacientes = new List<Paciente>();

            medico.id = dictionary["id"].ToString();
            medico.email = dictionary["email"].ToString();
            medico.nombre = dictionary["nombre"].ToString();
            medico.apellidos = dictionary["apellidos"].ToString();

            Dictionary<string, object> diccionario = dictionary["pacientes"] as Dictionary<string, object>;

            foreach (KeyValuePair<string, object> entrada in diccionario)
            {
                //Paciente pacienteAux = entrada.Value as Paciente; //ESTO ES NULO
                Paciente pacienteAux = new Paciente();
                pacienteAux = pacienteAux.DictionaryToPaciente(entrada.Value);
                medico.pacientes.Add(pacienteAux);
            }
            Debug.Log("Tamaño de pacientes: " + medico.pacientes.Count);
            return medico;
        }

        public void printValues()
        {
            Debug.Log("ID: " + id);
            Debug.Log("Email: " + email);
            Debug.Log("Nombre: " + nombre);
            Debug.Log("Apellidos: " + apellidos);
            Debug.Log("Nº pacientes: " + pacientes.Count);
            Debug.Log("Lista de pacientes:");

            for(int i=0; i < pacientes.Count; i++)
            {
                Debug.Log("\t-" + pacientes[i].idMedico);
            }
           
        }
    }
}
