using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoarAttack : MonoBehaviour
{

    /*
     * this script for making the boar attack the player and dealing damage to him
     */


    private GameObject Boar;
    public Animator DamgeIndecator;
    public AudioSource attackSound;
    public bool isPlayerInRange;
    bool isAttacking;

    private void Awake()
    {
        Boar = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // chicking if player is in range to attack
        if (isPlayerInRange)
        {
            Attack();
        }
        else
        {
            MoveTowardsThePlayer();
        }
    }




    void Attack()
    {
        if(!isAttacking && !GameManager.isPlayerDead)
        {
            isAttacking = true;
            StartCoroutine(Attacking());
        }
    }


    IEnumerator Attacking()
    {   
        // Applying attack
        attackSound.Play();
        Boar.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        if (isPlayerInRange)
        {
            DamgeIndecator.SetTrigger("Hit");
            GameManager.health -= 20;
        }
        yield return new WaitForSeconds(1.5f);
        isAttacking = false;
    }


    void MoveTowardsThePlayer()
    {
        // Chicking first if the boar is dead before make it move again
        if (Boar.GetComponent<CapsuleCollider>().enabled)
        {
            Boar.GetComponent<Navigation>().enabled = true;
            Boar.GetComponent<NavMeshAgent>().enabled = true;
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        // player enter the attack range
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //player Exits the attack range
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
