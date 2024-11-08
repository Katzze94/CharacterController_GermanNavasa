using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
   //ESTO SALE EN EXAMEN

   void Update()
   {
        if(Input.GetButtonDown("Fire1"))
        {
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

         RaycastHit hit;
         if(Physics.Raycast(ray, out hit))
         {
            Debug.Log(hit.transform.name);
         }
        }
   }
}
