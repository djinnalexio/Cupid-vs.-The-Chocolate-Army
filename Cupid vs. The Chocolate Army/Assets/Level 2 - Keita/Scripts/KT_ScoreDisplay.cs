using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KT_ScoreDisplay : MonoBehaviour
{

    Text scoreText;
    KT_GameSession gameSession;

    // Use this for initialization
    void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<KT_GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}
