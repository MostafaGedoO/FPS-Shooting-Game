using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController CharacterController;

    private Vector3 MoveDirection;

    public float Speed = 5;
    [SerializeField]
    private float gravity = 20;

    public float jumpForce = 10f;
    private float VerticalVolacity;

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }


    public void MoveThePlayer()
    {
        if (!GameManager.isPlayerDead)
        {
            //Moving the player using axis
            MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            MoveDirection = transform.TransformDirection(MoveDirection);
            MoveDirection *= Speed * Time.deltaTime;
            ApplyGravity();
            CharacterController.Move(MoveDirection);
        }  
    }

    public void ApplyGravity()
    {
        //Applying gravity to the player and the somthing the movement on the Y axis
        if(CharacterController.isGrounded)
        {
            VerticalVolacity -= gravity * Time.deltaTime;
            //Jumping
            PlayerJump();
        }
        else
        {
            VerticalVolacity -= gravity * Time.deltaTime;
        }

        MoveDirection.y = VerticalVolacity * Time.deltaTime;
    }


    public void PlayerJump()
    {
        //Jumping with the space 
        if (CharacterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            VerticalVolacity = jumpForce;
        }
    }
}
