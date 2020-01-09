using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GC_GameSession : MonoBehaviour
{
    int score = 0;
    private void Awake()
    {
//        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessisons = FindObjectsOfType<GC_GameSession>().Length;
        if (numberGameSessisons > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
           
    }

   public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        if (score >= 2000)
        {
            SceneManager.LoadScene("GC_NextLevel");
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
