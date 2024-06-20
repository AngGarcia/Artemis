using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class Paciente
    {
        //public string id = "";
        public string nick = "";
        public bool esMedico = false;

        public string idMedico = "";
        public string nombre = "";
        public string apellidos = "";

        public float tiempoEnRespiracion = 0;
        public float tiempoEnHarmonyHeaven = 0;
        public float tiempoEnRitmoVegetal = 0;
        public float tiempoEnPaisajeSonoro = 0;
        public float tiempoEnMelodiaFloral = 0f;

        public int numVecesPausa = 0;
        
        public Dictionary<int, bool> nivelesSuperados;

        public void initPaciente(string nick, string nombre, string apellidos, string idMedico)
        {
            //this.id = id;
            this.nick = nick;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.idMedico = idMedico;

            nivelesSuperados = new Dictionary<int, bool>
            {
                { 2, false },
                { 3, false },
                { 4, false },
                { 5, false },
                { 6, false }
            };
        }

        public Dictionary<string, object> returnDatosPaciente()
        {
            Dictionary<string, object> paciente = new Dictionary<string, object>
            {
               // { "id" , id },
                { "esMedico", esMedico },
                { "nick" , nick },
                { "idMedico" , idMedico },
                { "nombre" , nombre },
                { "apellidos" , apellidos },
                { "tiempoEnRespiracion" , tiempoEnRespiracion },
                { "tiempoEnHarmonyHeaven" , tiempoEnHarmonyHeaven },
                { "tiempoEnRitmoVegetal" , tiempoEnRitmoVegetal },
                { "tiempoEnPaisajeSonoro" , tiempoEnPaisajeSonoro },
                { "tiempoEnMelodiaFloral" , tiempoEnMelodiaFloral },
                { "numVecesPausa" , numVecesPausa },
                //{ "nivelesSuperados", nivelesSuperados }
            };

            return paciente;
        }

        //lo usamos para convertir los datos que nos llegan de la base de datos al tipo de clase que queremos
        public Paciente DictionaryToPaciente(Dictionary<string, object> dictionary)
        {
            Paciente paciente = new Paciente();

           // paciente.id = dictionary["id"].ToString();
            paciente.nick = dictionary["nick"].ToString();
            paciente.nombre = dictionary["nombre"].ToString();
            paciente.apellidos = dictionary["apellidos"].ToString();

            paciente.tiempoEnRespiracion = System.Convert.ToSingle(dictionary["tiempoEnRespiracion"]);
            paciente.tiempoEnHarmonyHeaven = System.Convert.ToSingle(dictionary["tiempoEnHarmonyHeaven"]);
            paciente.tiempoEnRitmoVegetal = System.Convert.ToSingle(dictionary["tiempoEnRitmoVegetal"]);
            paciente.tiempoEnPaisajeSonoro = System.Convert.ToSingle(dictionary["tiempoEnPaisajeSonoro"]);
            paciente.tiempoEnMelodiaFloral = System.Convert.ToSingle(dictionary["tiempoEnMelodiaFloral"]);
            paciente.numVecesPausa = Convert.ToInt32(dictionary["numVecesPausa"]);

            //paciente.nivelesSuperados = dictionary["nivelesSuperados"] as Dictionary<int, bool>;

            return paciente;
        }

        public void printValues()
        {
            //Debug.Log("ID: " + id);
            Debug.Log("Nick: " + nick);
            Debug.Log("Nombre: " + nombre);
            Debug.Log("Apellidos: " + apellidos);
            Debug.Log("TiempoEnRespiracion: " + tiempoEnRespiracion);
            Debug.Log("TiempoEnHarmonyHeaven: " + tiempoEnHarmonyHeaven);
            Debug.Log("TiempoEnRitmoVegetal: " + tiempoEnRitmoVegetal);
            Debug.Log("TiempoEnPaisajeSonoro: " + tiempoEnPaisajeSonoro);
            Debug.Log("TiempoEnMelodiaFloral: " + tiempoEnMelodiaFloral);
            Debug.Log("NumVecesPausa: " + numVecesPausa);
            //Debug.Log("Niveles superados: " + nivelesSuperados);
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

        public Dictionary<string, object> returnDatosMedico()
        {
            Dictionary<string, object> medico = new Dictionary<string, object>
            {
                { "id" , id },
                { "esMedico", esMedico },
                { "email" , email },
                { "nombre" , nombre },
                { "apellidos" , apellidos },
                { "pacientes", pacientes }
            };

            return medico;
        }

        //lo usamos para convertir los datos que nos llegan de la base de datos al tipo de clase que queremos
        public Medico DictionaryToMedico(Dictionary<string, object> dictionary)
        {
            Medico medico = new Medico();

            medico.id = dictionary["id"].ToString();
            medico.email = dictionary["email"].ToString();
            medico.nombre = dictionary["nombre"].ToString();
            medico.apellidos = dictionary["apellidos"].ToString();
            medico.pacientes = dictionary["pacientes"] as List<Paciente>;

            return medico;
        }

        public void printValues()
        {
            Debug.Log("ID: " + id);
            Debug.Log("Email: " + email);
            Debug.Log("Nombre: " + nombre);
            Debug.Log("Apellidos: " + apellidos);
            Debug.Log("Nº pacientes: " + pacientes.Count);
            //A PARTIR DE AQUÍ NO PRINTA, PROBLEMAS CON EL ARRAY
            Debug.Log("Lista de pacientes:");

            for(int i=0; i < pacientes.Count; i++)
            {
                Debug.Log("\t-" + pacientes[i].nombre);
            }
           
        }
    }
}
