using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The code to control the score bar and allow it to represent current score.
/// </summary>
public class PH_ScoreBar : MonoBehaviour
{
    PH_GameSession gameSession;
    float GoalProgession;
    [SerializeField] Color completedColor = Color.white;


    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<PH_GameSession>();//use the game session
    }

    // Update is called once per frame
    void Update()
    {
        //turns the current score into a decimal between 0 and 1, which is the scale of the score bar
        GoalProgession = Mathf.Clamp( gameSession.GetScore() / gameSession.GetGoal(), 0, 1);
        gameObject.transform.localScale = new Vector3(1, GoalProgession, 0);

        if (GoalProgession == 1) { gameObject.GetComponentInChildren<Image>().color = completedColor; }
        //change the color of the bar when it's filled.
    }
}
