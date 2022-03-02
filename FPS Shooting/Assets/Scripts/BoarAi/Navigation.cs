using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    //moving with nav Mesh Agent

    private NavMeshAgent boar;
    private Transform player;
    // Start is called before the first frame update
    void Awake()
    {
        boar = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        boar.SetDestination(player.position);
    }
}
