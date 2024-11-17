using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicloDiaYNoche : MonoBehaviour
{
    
    public int rotationScale = 10;

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationScale * Time.deltaTime, 0, 0);
        
    }
}
