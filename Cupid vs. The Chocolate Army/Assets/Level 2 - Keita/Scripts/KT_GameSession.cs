using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KT_GameSession : MonoBehaviour
{

    int score = 0;

    public void Update()
    {

    }
    private void Awake()
    {
//        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<KT_GameSession>().Length;
        if (numberGameSessions > 1)
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
        if (score >= 4000)
        {
            SceneManager.LoadScene("KT_Win Screen");
        }
    }




        public void ResetGame()
        {
            Destroy(gameObject);
        }
}


