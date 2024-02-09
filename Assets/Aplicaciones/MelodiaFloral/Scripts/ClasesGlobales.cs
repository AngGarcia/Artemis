using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlteracionMelodia
{
    public class TiposNotas
    {
        public static int SILENCIO = 0;
        public static int REDONDA = 1;
        public static int BLANCA = 2;
        public static int NEGRA = 3;
        public static int CORCHEA = 4;
    }

    public class VelocidadNotas
    {
        public static float SILENCIO = 1f;
        public static float REDONDA = 1.2f;
        public static float BLANCA = 1f;
        public static float NEGRA = 0.3f;
        public static float CORCHEA = 0.2f;
    }

    public class Intrumentos
    {
        public static int PIANO = 0;
        public static int FLAUTA = 1;
        public static int CLARINETE = 2;
        public static int OBOE = 3;
        public static int VIOLIN = 4;
    }

    public class PosicionAxisYNotas
    {
        public static float DO = -5.18f;
        public static float RE = -4.63f;
        public static float MI = -4.09f;
        public static float FA = -3.6f;
        public static float SOL = -3.07f;
        public static float LA = -2.6f;
        public static float SI = -1.87f;
        public static float DO_OCT = -1.37f;

    }
}

