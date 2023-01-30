using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Character : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float gravity = 9.81f;
    public float jumpSpeed = 10.0f;

    Vector3 moveDir;
    CharacterController controller;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            controller = GetComponent<CharacterController>();
            controller.minMoveDistance = 0.0f;

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
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

       
       
        if (controller.isGrounded)
        {
            moveDir = new Vector3(hInput, 0, vInput) * moveSpeed;
            moveDir = transform.TransformDirection(moveDir);

            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }

           
        }


        moveDir.y -= gravity;
        controller.Move(moveDir * Time.deltaTime);


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
