using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// The code to get the score and display it on the UI.
/// </summary>

public class PH_ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI ScoreDisplay;
    PH_GameSession gameSession;

    
    // Start is called before the first frame update
    void Start()
    {
        ScoreDisplay = GetComponent<TextMeshProUGUI>(); // get the text field attached to the same object
        gameSession = FindObjectOfType<PH_GameSession>();// find the game session to use it's data
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreDisplay.text = Mathf.Clamp(gameSession.GetScore(), 0, gameSession.GetGoal()).ToString();// display the current score and don't let it go over
    }
}
