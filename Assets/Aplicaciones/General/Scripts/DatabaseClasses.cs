using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class Paciente
    {
        public string id = "";
        public string email = "";

        public string idMedico = "";
        public string nombre = "";
        public string apellidos = "";

        public float tiempoEnRespiracion = 0;
        public float tiempoEnHarmonyHeaven = 0;
        public float tiempoEnRitmoVegetal = 0;
        public float tiempoEnPaisajeSonoro = 0;
        public float tiempoEnMelodiaFloral = 0f;

        public int numVecesPausa = 0;

        //constructor para usar al crear el usuario en la BBDD
        public Paciente(string id, string email)
        {
            this.id = id;
            this.email = email;
        }

        public Dictionary<string, object> returnDatosPaciente()
        {
            Dictionary<string, object> paciente = new Dictionary<string, object>
            {
                { "id" , id },
                { "email" , email },
                { "isMedico" , idMedico },
                { "nombre" , nombre },
                { "apellidos" , apellidos },
                { "tiempoEnRespiracion" , tiempoEnRespiracion },
                { "tiempoEnHarmonyHeaven" , tiempoEnHarmonyHeaven },
                { "tiempoEnRitmoVegetal" , tiempoEnRitmoVegetal },
                { "tiempoEnPaisajeSonoro" , tiempoEnPaisajeSonoro },
                { "tiempoEnMelodiaFloral" , tiempoEnMelodiaFloral },
                { "numvecesPausa" , numVecesPausa },
            };

            return paciente;
        }
    }

    public class Medico
    {
        public string id = "";
        public string email = "";

        public string nombre = "";
        public string apellidos = "";

        public List<Paciente> pacientes;

        //constructor para usar al crear el usuario en la BBDD
        public Medico(string id, string email)
        {
            this.id = id;
            this.email = email;
            pacientes = new List<Paciente>();
        }

        public Dictionary<string, object> returnDatosMedico()
        {
            Dictionary<string, object> medico = new Dictionary<string, object>
            {
                { "id" , id },
                { "email" , email },
                { "nombre" , nombre },
                { "apellidos" , apellidos },
                { "pacientes", pacientes }
            };

            return medico;
        }
    }
}
