using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
   
   public Transform player;

   private Vector3 lastMousePosition;
   [SerializeField] float rotationSpeed = 5f;

   
    
   private void LateUpdate()
   {

      float mouseX = Input.GetAxis("Mouse X");

      float rotationAmount = mouseX* rotationSpeed;

      transform.Rotate(0f,0f, -rotationAmount);

    Vector3 newPosition = player.position;

    newPosition.y = transform.position.y;

    transform.position = newPosition;

   

   }
}
