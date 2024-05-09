using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlteracionMelodia
{
    public class ListaNotasFMOD : MonoBehaviour
    {
        [Header("PIANO")]
        public string[] notasRedondasPiano;
        public string[] notasBlancasPiano;
        public string[] notasNegrasPiano;
        public string[] notasCorcheasPiano;
        [Header("FLAUTA")]
        public string[] notasRedondasFlauta;
        public string[] notasBlancasFlauta;
        public string[] notasNegrasFlauta;
        public string[] notasCorcheasFlauta;
        [Header("CLARINETE")]
        public string[] notasRedondasClarinete;
        public string[] notasBlancasClarinete;
        public string[] notasNegrasClarinete;
        public string[] notasCorcheasClarinete;
        [Header("OBOE")]
        public string[] notasRedondasOboe;
        public string[] notasBlancasOboe;
        public string[] notasNegrasOboe;
        public string[] notasCorcheasOboe;
        [Header("VIOLIN")]
        public string[] notasRedondasViolin;
        public string[] notasBlancasViolin;
        public string[] notasNegrasViolin;
        public string[] notasCorcheasViolin;


        public void tocarNotaFMOD(int instrumentoActual, int sonidoActual, int tipoNotaActual)
        {
            if (instrumentoActual == 0) //piano
            {
                if (tipoNotaActual == 1) //1 es redonda 
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasRedondasPiano[sonidoActual]);
                }
                else if (tipoNotaActual == 2)//2 es blanca
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasBlancasPiano[sonidoActual]);
                }
                else if (tipoNotaActual == 3)//3 es negra
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasNegrasPiano[sonidoActual]);
                }
                else if (tipoNotaActual == 4)//4 es corchea
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasCorcheasPiano[sonidoActual]);
                }

            }
            else if (instrumentoActual == 1) //flauta
            {
                if (tipoNotaActual == 1) //1 es redonda 
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasRedondasFlauta[sonidoActual]);
                }
                else if (tipoNotaActual == 2)//2 es blanca
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasBlancasFlauta[sonidoActual]);
                }
                else if (tipoNotaActual == 3)//3 es negra
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasNegrasFlauta[sonidoActual]);
                }
                else if (tipoNotaActual == 4)//4 es corchea
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasCorcheasFlauta[sonidoActual]);
                }
            }
            else if (instrumentoActual == 2) //clarinete
            {
                if (tipoNotaActual == 1) //1 es redonda 
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasRedondasClarinete[sonidoActual]);
                }
                else if (tipoNotaActual == 2)//2 es blanca
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasBlancasClarinete[sonidoActual]);
                }
                else if (tipoNotaActual == 3)//3 es negra
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasNegrasClarinete[sonidoActual]);
                }
                else if (tipoNotaActual == 4)//4 es corchea
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasCorcheasClarinete[sonidoActual]);
                }
            }
            else if (instrumentoActual == 3) //oboe
            {
                if (tipoNotaActual == 1) //1 es redonda 
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasRedondasOboe[sonidoActual]);
                }
                else if (tipoNotaActual == 2)//2 es blanca
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasBlancasOboe[sonidoActual]);
                }
                else if (tipoNotaActual == 3)//3 es negra
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasNegrasOboe[sonidoActual]);
                }
                else if (tipoNotaActual == 4)//4 es corchea
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasCorcheasOboe[sonidoActual]);
                }
            }
            else if (instrumentoActual == 4) //violin
            {
                if (tipoNotaActual == 1) //1 es redonda 
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasRedondasViolin[sonidoActual]);
                }
                else if (tipoNotaActual == 2)//2 es blanca
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasBlancasViolin[sonidoActual]);
                }
                else if (tipoNotaActual == 3)//3 es negra
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasNegrasViolin[sonidoActual]);
                }
                else if (tipoNotaActual == 4)//4 es corchea
                {
                    FMODUnity.RuntimeManager.PlayOneShot(notasCorcheasViolin[sonidoActual]);
                }
            }

        }
    }

}
