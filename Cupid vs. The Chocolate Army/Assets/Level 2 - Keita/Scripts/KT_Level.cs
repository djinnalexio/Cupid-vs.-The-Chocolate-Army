using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KT_Level : MonoBehaviour
{
     
    [SerializeField] float delayInSeconds = 2f;

    public void LoadStartMenu() { SceneManager.LoadScene(0); }

    public void LoadGame()
    {
        SceneManager.LoadScene("KT_Game");
        FindObjectOfType<KT_GameSession>().ResetGame();
    }

    public void LoadLevelThree() { SceneManager.LoadScene("Level 3"); }

    public void LoadGameOver() { StartCoroutine(WaitAndLoad()); }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("PH_Game Over");
    }

    public void QuitGame() { Application.Quit(); }

}