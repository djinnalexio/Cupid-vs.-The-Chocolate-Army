using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JH_Level : MonoBehaviour
{
     [SerializeField] float delayInSeconds = 2f;

        public void LoadStartMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void LoadNextScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }

        public void LoadGame()
        {
            SceneManager.LoadScene("Level3");
            FindObjectOfType<JH_GameSession>().ResetGame();
        }

        public void LoadGameOver()
        {
            StartCoroutine(WaitAndLoad());
        }

        IEnumerator WaitAndLoad()
        {
            yield return new WaitForSeconds(delayInSeconds);
            SceneManager.LoadScene("PH_Game Over");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
}


