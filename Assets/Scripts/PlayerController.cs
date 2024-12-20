using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
//--------------Componentes------------------
    private CharacterController _controller;

     private Transform _camera;

     private Animator _animator;
//-------------InPuts-----------------------------------

    private float _horizontal;

    private float _vertical;

    [SerializeField] private float _movementSpeed = 10;
   // [SerializeField] private float _pushForce = 10;  //Sesion 8/11

    private float _turnSmoothVelocity;   //Hacer rotación del personaje suave
    [SerializeField] private float _turnSmoothTime = 0.5f;   //Hacer rotación del personaje suave

    [SerializeField] private float _jumpHeight = 1;


//----------------Gravedad--------------------------
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Vector3 _playerGravity;


//----------------Ground Sensor -------------------
    [SerializeField] Transform _sensorPosition;

    [SerializeField] float _sensorRadius = 0.5f;

    [SerializeField] LayerMask _groundLayer;

    private Vector3 moveDirection;

    private bool isDead;

    
   





    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
        _animator = GetComponentInChildren<Animator>();
    }
   
   
    // Start is called before the first frame update
    void Start()
    {
        _animator.SetBool("IsDeath", false);

         Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal"); //examen charactercontroller
        _vertical = Input.GetAxis("Vertical");   //Asignan los inputs de movimiento ,examen charactercontroller
       
       // Movement(); //Si es necesario en el examen

      
       
       
       

   
    if(Input.GetButton("Fire2"))//No es necesario en el examen
    {
        AimMovement();  //No es necesario en el examen
    }
    else if(!isDead)
    {
        Movement();//No es necesario en el examen
    }
    
    
    
    if(Input.GetButtonDown("Jump") && IsGrounded()) //Examen character controller
    {
        Jump();
    }

        Gravity();

        if(Input.GetKeyDown(KeyCode.F))  //En examen de charcater controller no
        {
            RayTest();
        }
    }

    void Movement() //Necesario para el examen
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);  //Almacenará los inputs de movimineto

        _animator.SetFloat("VelZ", direction.magnitude);
        _animator.SetFloat("VelX", 0);

        if(direction != Vector3.zero) //Solo se ejecuta si estoy tocando una flecha de movimineot
        {
      
        float targetAngle /*Establecer angulo de movimineto*/= Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y; //Hacía donde mira la camara es a donde mira el pj
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);

        transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

      Vector3  moveDirection = Quaternion.Euler(0, targetAngle, 0)*Vector3.forward;

        _controller.Move(moveDirection * _movementSpeed * Time.deltaTime);
       
        }
        
    }

    void MovimientoCutre() //Supervivencia examen 2 puntos
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);

        _animator.SetFloat("VelZ", direction.magnitude); //Animaciones
        _animator.SetFloat("VelX", 0);

       

        float targetAngle /*Establecer angulo de movimineto*/= Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; //Esto pa que rote
        transform.rotation = Quaternion.Euler(0, targetAngle, 0); //Esto pa que rote

        _controller.Move(direction * _movementSpeed * Time.deltaTime);
    }

     void AimMovement() //No necesario examen
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);

        _animator.SetFloat("VelZ", _vertical);
        _animator.SetFloat("VelX", _horizontal);
        
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _camera.eulerAngles.y, ref _turnSmoothVelocity, _turnSmoothTime);

        transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
       
       if(direction != Vector3.zero)

       {
       
         moveDirection = Quaternion.Euler(0, targetAngle, 0)*Vector3.forward;

        _controller.Move(moveDirection * _movementSpeed * Time.deltaTime);
       }
        
        
    }

    void Gravity() //Entra examen
    {
       if(!IsGrounded())
       {
        _playerGravity.y += _gravity *Time.deltaTime; //A muy malas esto
       }
        else if(IsGrounded() && _playerGravity.y < 0)
        {
            _playerGravity.y = -1;
            _animator.SetBool("IsJumping",false);
        }

       
        _controller.Move(_playerGravity * Time.deltaTime); //A muy malas esto
        
    }

    void Jump()  //Entra examen  1 punto
    {
        _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity); //Aprenderlo
       // _playerGravity.y = _jumpHeight
        
        
        _animator.SetBool("IsJumping",true);


    }

    bool IsGrounded() //esto examen 1 punto
    {
        return Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);
    }

 
   
   
    /*bool IsGrounded() //sesión 8/11
    {
        RaycastHit hit;
        if(Physics.Raycast(_sensorPosition.position, -transform.up, out hit, 2))
        {
            if(hit.transform.gameObject.layer == 3)
            {
                Debug.DrawRay(_sensorPosition.position, -transform.up * 2, Color.green);
                return true;
            }
            else 
            {
                Debug.DrawRay(_sensorPosition.position, -transform.up * 2, Color.red);
                return false;
            }
        }
        else
        {
            return false;
        }
    }*/


   
   /*void OnControllerColliderHit(ControllerColliderHit hit) //Sesion 8/11
    {
     
     if(hit.gameObject.layer == 7)
     {

     }
     
     
     Rigidbody rBody = hit.collider.attachedRigidbody;
    
      if(rBody != null)
    {
        Vector3 pushDirection = new Vector3(moveDirection.x, 0, moveDirection.z);

        rBody.velocity = pushDirection * _pushForce / rBody.mass;
    } 
    


    }*/
   
   void RayTest() //8/11 De esto habrá examen, durito
   {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            Debug.Log(hit.transform.name);
            Debug.Log(hit.transform.position);
            Debug.Log(hit.transform.gameObject.layer);

            if(hit.transform.gameObject.tag == "Enemy")
            {
                Enemy enemyScript = hit.transform.gameObject.GetComponent<Enemy>();

                enemyScript.TakeDamage();
            }
        }
   }
   
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_sensorPosition.position, _sensorRadius);
    }


    void OnTriggerEnter (Collider collider)
    {

        Death();

    }


    void Death()
    {
        

        isDead= true;
       

        _animator.SetBool("IsDeath", true);
    }
}


