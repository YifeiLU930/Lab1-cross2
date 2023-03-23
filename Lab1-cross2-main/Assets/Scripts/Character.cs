using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class Character : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float gravity = 9.81f;
    public float jumpSpeed = 10.0f;
   

    Rigidbody rb;
    Vector3 curMoveInput;
    CharacterController charController;
    Animator anim;
    
  

    public Rigidbody projectilePrefab;
    public Transform spawnPoint;

  
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            charController = GetComponent<CharacterController>();
            charController.minMoveDistance = 0.0f;
            anim = GetComponent<Animator>();
           

            if (moveSpeed <= 0.0f)
            {
                moveSpeed = 10.0f;
                throw new UnassignedReferenceException("Speed not set on" + name + "defaulting to" + moveSpeed.ToString());

            }
        }
   
        
        catch(NullReferenceException e)
        {
            Debug.Log(e.Message);
        }
        catch (UnassignedReferenceException e)
        {
            Debug.Log(e.Message);

        }
     }

    // Update is called once per frame
    void Update()
    {
      
        if (charController. isGrounded)
        {
            curMoveInput = transform.TransformDirection(curMoveInput);
           
        }
        else
        {
            curMoveInput.y -= gravity;
        }
       
        charController.Move(curMoveInput * Time.deltaTime);

       


       

    }


    public void MovePlayer(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            curMoveInput = Vector3.zero;
            return;
        }
        
        Vector2 move = context.action.ReadValue<Vector2>();
        move.Normalize();

        Vector3 moveDir = new Vector3(move.x, 0, move.y) * moveSpeed;

        curMoveInput += moveDir; 
        
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            Debug.Log("fire pressed");
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
       if (charController.isGrounded)
            curMoveInput.y += jumpSpeed;
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup1"))
        {
            Debug.Log("Collided with:health up " + other.name);
            Destroy (other.gameObject);

        }

        if (other.CompareTag("Pickup2"))
        {
            Debug.Log("Collided with:speed up " + other.name);
            Destroy(other.gameObject);

        }
        if (other.CompareTag("Pickup3"))
        {
            Debug.Log("lives-1 " + other.name);
            Destroy(other.gameObject);

        }

    }

   

 


}
