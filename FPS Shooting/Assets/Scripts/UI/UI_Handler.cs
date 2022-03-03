using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Handler : MonoBehaviour
{
    /*
     * this script for all the UI functions and for switching bettween menus
     */

    public TextMeshProUGUI health, bullets, boars;
    public GameObject WinUI, GameUI, GameOverUI,andriod_UI;
    public static bool isAndroidActive;

    private void Start()
    {
        // checking if android is on to change inputs or not 
        if(andriod_UI.activeInHierarchy)
        {
            isAndroidActive = true;
        }
        else
        {
            isAndroidActive = false;
        }
    }

    void Update()
    {
        //Boars count
        boars.text = GameObject.FindGameObjectsWithTag("Boar").Length.ToString();
        
        //Changing the health text color accouding to the value
        if (GameManager.health <= 40)
            health.color = Color.red;
        else if (GameManager.health <= 60)
            health.color = Color.yellow;
        
        //Showing health and bullets count
        health.text = GameManager.health.ToString();
        bullets.text = GameManager.bullets.ToString();

        //Game over
        if(GameManager.isPlayerDead || (GameObject.FindGameObjectsWithTag("Boar").Length != 0 && GameManager.bullets == 0))
        {
            EndTheGame();
        }
        // if player hunt all the boars
        if (GameObject.FindGameObjectsWithTag("Boar").Length == 0 && !GameManager.isPlayerDead)
        {
            GameWin();
        }
    }

    public void EndTheGame()
    {
        //Game over
        GameManager.isPlayerDead = true;
        GameUI.SetActive(false);
        GameOverUI.SetActive(true);
    }

    public void GameWin()
    {
        //Player win
        GameManager.IsPlayerWin = true;
        GameUI.SetActive(false);
        WinUI.SetActive(true);
    }

    public void Restart()
    {
        // reset the game
        GameManager.bullets = 15;
        GameManager.health = 100;
        GameManager.isPlayerDead = false;
        GameManager.IsPlayerWin = false;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        //Exit
        Application.Quit();
    }    

}
