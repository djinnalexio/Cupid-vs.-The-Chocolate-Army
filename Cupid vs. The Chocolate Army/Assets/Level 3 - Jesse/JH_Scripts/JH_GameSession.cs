using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JH_GameSession : MonoBehaviour
{
    int score = 0;
    int targetScore = 8000;

    void Update()
    {
        if (score >= targetScore)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("JH_Win");
        }
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<JH_GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() { return score; }

    public void AddToScore(int scoreValue) { score += scoreValue; }

    public void ResetGame() { Destroy(gameObject); }
}
