
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GC_Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;

    public void LoadStartMenu() { SceneManager.LoadScene(0); } //Put Jesse's file name here//
    
    public void LoadNextScene() { SceneManager.LoadScene("GC_NextLevel"); } //this may change when all other files are added//
    
    public void LoadLevelTwo() { SceneManager.LoadScene("KT_Game"); }// this is where Keita's game level scene name will go//
    
    public void LoadGame()
    {
        SceneManager.LoadScene("GC_Game");
        FindObjectOfType<GC_GameSession>().ResetGame();
    }

    public void LoadGameOver() { StartCoroutine(WaitAndLoad()); }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("PH_Game Over");
    }

    public void QuitGame() {Application.Quit(); }
}
