using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General
{
    public class LogInMenu : MonoBehaviour
    {
        public TMP_InputField inputEmail;
        public TMP_InputField inputPassword;
        public Toggle checkIsMedico;

        public void SignIn()
        {
            bool emailCorrect = false;
            bool pwdCorrect = false;
            bool esMedico = false; //si es false, el usuario es un paciente

            //primero vemos que sea, efectivamente, un email
            if (inputEmail.text.Contains("@") && ((inputEmail.text.Contains(".com") || inputEmail.text.Contains(".es"))))
            {
                emailCorrect = true;
            }
            else
            {
                Debug.Log("ERROR: email incorrecto");
                //poner Asset para mostrar el mensaje de error
            }

            //después comprobamos que la contraseña sea correcta
            if (inputPassword.text.Length >= 6)
            {
                pwdCorrect = true;
            }
            else
            {
                Debug.Log("ERROR: la contraseña debe tener mínimo 6 caracteres.");
            }

            //vamos is es un médico o no
            if (checkIsMedico.isOn)
            {
                esMedico = true;
            }

            //cuando se hayan hecho esas comprobaciones, creamos el usuario en Firebase
            if (emailCorrect && pwdCorrect)
            {
                string email = inputEmail.text;
                string password = inputPassword.text;
                Debug.Log("Creando usuario...");
                ConectToDatabase.Instance.CreateUser(email, password, "", "", esMedico);
            }

        }

        public void LogIn()
        {
            string email = inputEmail.text;
            string password = inputPassword.text;

            //ConectToDatabase.Instance.Login(email, password);
        }

        //DE PRUEBA, NO VA AQUÍ
        public void LogOut()
        {
            ConectToDatabase.Instance.LogOut();
        }
    }
}
