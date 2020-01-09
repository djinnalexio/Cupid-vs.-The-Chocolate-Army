using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// Script that contains all methods related to scene travel
/// </summary>

public class PH_SceneLoader : MonoBehaviour
{
    [SerializeField] float GameOverDelay = 2f;
    
    //end screens
    [SerializeField] GameObject retryScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject continueButton;

    EventSystem eventSystem;


    private void Start()
    {//start the scene with win and lose screens turned off and find the game session
        if (retryScreen) { retryScreen.SetActive(false); }
        if (winScreen) { winScreen.SetActive(false); }
        eventSystem = FindObjectOfType<EventSystem>();
    }

    //exculsive to start scene
    public void QuitGame() {Application.Quit(); }
    public void LoadInstructions() { SceneManager.LoadScene("KT_Instructions"); }
    public void LoadCredits() { SceneManager.LoadScene("KT_Credits"); }
    public void LoadStartScene() { Time.timeScale = 1; SceneManager.LoadScene(0); }


    //Link to each level
    public void LoadLevelOne() { SceneManager.LoadScene("GC_Game"); }
    public void LoadLevelTwo() { SceneManager.LoadScene("KT_Game"); }
    public void LoadLevelThree() { SceneManager.LoadScene("Level 3"); }
    public void LoadLevelFour() { SceneManager.LoadScene("PH_Level 4"); if (FindObjectOfType<PH_GameSession>()) { FindObjectOfType<PH_GameSession>().ResetScore(); } }


    //Other links
    public void LoadWinScene() { SceneManager.LoadScene("GC_FinalWInScene"); }
    
    public void LoadGameOverScene() { StartCoroutine(WaitAndLoad("PH_Game Over"));  }
    IEnumerator WaitAndLoad(string sceneName)
    {
        yield return new WaitForSeconds(GameOverDelay);
        SceneManager.LoadScene(sceneName);
    }

    public void ShowRetryScreen() { StartCoroutine(LoadRetryScreen()); }
    IEnumerator LoadRetryScreen()
    {
        yield return new WaitForSeconds(GameOverDelay);
        retryScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(retryButton);
    }

    public void ShowWinTransition() { StartCoroutine(LoadWinTransition()); }
    IEnumerator LoadWinTransition()
    {
        yield return new WaitForSeconds(GameOverDelay);
        winScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(continueButton);
    }

    public string GetCurrentSceneName() { return SceneManager.GetActiveScene().name; }

}
