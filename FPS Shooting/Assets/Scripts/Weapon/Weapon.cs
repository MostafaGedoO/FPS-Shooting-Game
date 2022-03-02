using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Weapon : MonoBehaviour
{
    /*
     * this scrpit for firing the weapon and killing boars
     */

    public AudioSource ShootSound;
    public ParticleSystem MazleFlash;
    private Animator Rifle;
    [SerializeField] private float ShootTime = 0.2f;
    private bool isShooting;

    void Awake()
    {
        Rifle = GetComponent<Animator>();
    }

    void Update()
    {
        Shoot();    
    }

    public void Shoot()
    {
        if (!isShooting && Input.GetMouseButtonDown(0) && !GameManager.isPlayerDead && GameManager.bullets >= 1 && !GameManager.IsPlayerWin)
        {
            isShooting = true;
            StartCoroutine(Shooting());
        }
    }

    IEnumerator Shooting()
    {
        // Shoot animation and sound
        Rifle.SetTrigger("Shoot");
        GameManager.bullets -= 1;
        ShootSound.Play();
        MazleFlash.Play();
        //Shooting a boar
        if(RayCastManager.HitObject.CompareTag("Boar"))
        {
            KillBoar();
        }
        yield return new WaitForSeconds(ShootTime);
        isShooting = false;
    }

    void KillBoar()
    {
        //Kill Boar
        if (RayCastManager.HitObject.GetComponent<CapsuleCollider>().enabled)
        {
            RayCastManager.HitObject.GetComponent<Animator>().SetTrigger("Death");
            RayCastManager.HitObject.GetComponent<NavMeshAgent>().enabled = false;
            RayCastManager.HitObject.GetComponent<Navigation>().enabled = false;
            RayCastManager.HitObject.GetComponent<CapsuleCollider>().enabled = false;
            RayCastManager.HitObject.GetComponent<BoarAttack>().isPlayerInRange = false;
            RayCastManager.HitObject.GetComponent<AudioSource>().Play();
            Destroy(RayCastManager.HitObject, 5f);
        }
    }
}
