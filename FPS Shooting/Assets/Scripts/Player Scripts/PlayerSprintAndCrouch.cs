using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerFootSteps footSteps;

    private PlayerMovement playerMovement;

    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f;

    private Transform look_Root;
    private float stand_Height = 1.6f;
    private float crouch_Height = 1f;

    private bool isCrouching = false;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        look_Root = transform.GetChild(0);
        footSteps = GetComponent<PlayerFootSteps>();
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();

        Crouch();
    }

    public void Sprint()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.Speed = sprint_Speed;
            footSteps.Volume_Max = 0.4f;
            footSteps.Volume_Min = 0.2f;
            footSteps.Step_Distace = 0.35f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.Speed = move_Speed;
            footSteps.Volume_Max = 0.3f;
            footSteps.Volume_Min = 0.1f;
            footSteps.Step_Distace = 0.6f;
        }
    }

    public void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrouching)
            {
                //if we are crouching - stand up
                look_Root.localPosition = new Vector3(0, stand_Height, 0);
                playerMovement.Speed = move_Speed;
                footSteps.Step_Distace = 0.6f;
                footSteps.Volume_Max = 0.3f;
                footSteps.Volume_Min = 0.1f;
                isCrouching = false;
            }
            else
            {
                //if we are not croching - crouch
                look_Root.localPosition = new Vector3(0, crouch_Height, 0);
                playerMovement.Speed = crouch_Speed;
                footSteps.Step_Distace = 0.75f;
                footSteps.Volume_Max = 0.1f;
                footSteps.Volume_Min = 0.05f;
                isCrouching = true;
            }
        }
    }
}
