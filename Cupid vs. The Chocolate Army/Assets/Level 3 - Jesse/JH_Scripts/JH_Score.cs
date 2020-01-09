using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JH_Score : MonoBehaviour
{
    Text scoreText;
    JH_GameSession gameSession;

    // Use this for initialization
    void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<JH_GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}
