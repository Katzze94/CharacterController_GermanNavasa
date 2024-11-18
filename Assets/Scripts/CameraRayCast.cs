using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //Muy necesario
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
   //ESTO SALE EN EXAMEN_para este examen se pude usar la API unity


   public Text cuentaAtrasTexto;  //Necesario para la cuenta atrás
   void Update() //esto son 5 puntos
   {
        if(Input.GetButtonDown("Fire1"))
        {
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

         RaycastHit hit;
         if(Physics.Raycast(ray, out hit))
         {
            
            if(hit.transform.tag == "caja 1")
            {
               StartCoroutine(CuentaAtras()); //Como cargar la corrutina

               SceneManager.LoadScene("Escena que toque"); //Codigo para cargar escena
            }
            else if(hit.transform.tag == "caja 2")
            {

            }
            else if(hit.transform.tag == "caja 3")
            {

            }
            
            Debug.Log(hit.transform.name);
         }
        }
   }




   IEnumerator CuentaAtras() //tambien entra
   {
      cuentaAtrasTexto.text="5";
      yield return new WaitForSeconds(1);


      cuentaAtrasTexto.text="0";









      SceneManager.LoadScene("Escena que toque"); //Codigo para cargar escena
   }

}
