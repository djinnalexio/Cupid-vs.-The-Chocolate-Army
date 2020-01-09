using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script that manages the score and the life system
/// </summary>

public class PH_GameSession : MonoBehaviour
{
    [SerializeField] int Score = 0;
    [SerializeField] float goal = 16000;
    bool goalAcheived = false;


    private int fullLives = 5;
    int currentLives;
    PH_HeartsDisplay heartsDisplay;//the script to display the hearts
    PH_SceneLoader sceneLoader;


    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();
        currentLives = fullLives;
        sceneLoader = FindObjectOfType<PH_SceneLoader>();
    }

    
    void SetUpSingleton()
    {
        if (FindObjectsOfType<PH_GameSession>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject); 
        }
        else { DontDestroyOnLoad(gameObject); }
        ///when this object is started at the beginning of a scene, check if there is more than one of them.
        ///If there is, destroy this object( the extra one being currently setup)
        ///if not, mark it as the one that will stay on for all following scenes
    }

    void Update()
    {
        if (sceneLoader.GetCurrentSceneName() != "PH_Level 4") { Destroy(gameObject); }
    }//destroy if it's not Level 4

    public int GetScore() { return Score; }
    public float GetGoal() { return goal; }
    public void AddToScore(int scoreValue)
    {
        if (!goalAcheived) { Score += scoreValue; }
        if (Score >= goal) { goalAcheived = true;  FindObjectOfType<PH_PlayerController>().TransitionToWinScreen(); }
    }
    public void ResetScore() { Score = 0; goalAcheived = false; }
    public bool GetGoalProgressionStatus() { return goalAcheived; }


    public int GetCurrentLives() { return currentLives; }
    public int GetMaxLives() { return fullLives; }
    public void LoseALife()
    {
        currentLives--;//-1 to current lives
        heartsDisplay.DisplayCurrentHearts();// update the hearts currently displayed
    }

    public void SetHeartsDisplay(PH_HeartsDisplay currentHearts) { heartsDisplay = currentHearts; }
}
