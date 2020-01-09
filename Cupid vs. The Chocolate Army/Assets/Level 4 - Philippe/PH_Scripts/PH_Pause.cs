using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A code to pause and unpause the game.
/// Pause works either with the "return/enter" key or a button
/// </summary>

public class PH_Pause : MonoBehaviour
{
    //paused canvas
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject StartGameScreen;
    [SerializeField] GameObject ResumeButton;


    //external scripts
    PH_PlayerController Player;
    PH_GameSession gameSession;
    EventSystem eventSystem;


    bool isPaused = false;
    bool gameStarted = false;

    public bool GetPauseStatus() { return isPaused; }


    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        pauseScreen.SetActive(false);
        StartGameScreen.SetActive(true);
        Time.timeScale = 0;//pause at the beginning of the scene
        Player = FindObjectOfType<PH_PlayerController>();
        gameSession = FindObjectOfType<PH_GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted && !gameSession.GetGoalProgressionStatus())//while the game has started and the target score hasn't been reached yet
        {
            if (Input.GetButtonDown("Pause")) { PauseUnpause(); }//press Return to pause/unpause
        }

        else if (Input.GetButtonDown("Fire1")) { PlayGame(); }//press the Fire button at the beginning to start the level
    }

    void PauseUnpause()
    {
        if (Player)// if the player is there
        {
            if (!isPaused)// if it's not paused, pause
            {
                Time.timeScale = 0;
                isPaused = true;
                pauseScreen.SetActive(isPaused);
                eventSystem.SetSelectedGameObject(ResumeButton);
            }
            else// if it's paused, unpaused
            {
                isPaused = false;
                pauseScreen.SetActive(isPaused);
                Player.ShootingKillSwitch();
                Time.timeScale = 1;
            }
        }
    }

    public void PauseGameButton() { PauseUnpause(); }//function to link to a button
    
    public void PlayGame()
    {
        gameStarted = true;
        StartGameScreen.SetActive(false);
        Time.timeScale = 1;
    }

}

