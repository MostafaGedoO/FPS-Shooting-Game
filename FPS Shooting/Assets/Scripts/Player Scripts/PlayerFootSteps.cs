using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    //foot steps system

    private AudioSource soundManager;

    private CharacterController characterController;

    [SerializeField] private AudioClip[] footStepClip;

    public float Volume_Min, Volume_Max;

    private float accumulated_Distance;

    public float Step_Distace;

    void Awake()
    {
        soundManager = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        CheckToPlayFootStepSound();
    }

    void CheckToPlayFootStepSound()
    {
        if(!characterController.isGrounded || GameManager.isPlayerDead)
        {
            return;
        }

        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumulated_Distance += Time.deltaTime;

            if (accumulated_Distance > Step_Distace)
            {
                soundManager.volume = Random.Range(Volume_Min, Volume_Max);
                soundManager.clip = footStepClip[Random.Range(0, footStepClip.Length)];
                soundManager.Play();

                accumulated_Distance = 0;
            }
        }
        else
        {
            accumulated_Distance = 0;
        }

    }
}
