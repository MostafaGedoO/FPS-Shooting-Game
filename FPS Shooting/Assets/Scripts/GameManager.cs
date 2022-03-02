using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //important values to track

    public static int health = 100;
    public static int bullets = 15;
    public static bool isPlayerDead;
    public static bool IsPlayerWin;

    void Update()
    {
        if(health <= 0)
        {
            health = 0;
            isPlayerDead = true;
        }
    }
}
