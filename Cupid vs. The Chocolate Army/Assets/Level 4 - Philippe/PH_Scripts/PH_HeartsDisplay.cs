using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The reason why this section became it's own script intead of leaving it in the Game session script 
/// is because the hearts need to be added to the list at the beginning of each scene. But because
/// Game Session is an object that doesn't reset when reloading the scene, when the player tries again,
/// the hearts currently displayed are not updated because they are new hearts that don't belong to the list.
/// Thus, I made this script that is tied to the scene to manage the hearts from the game session.
/// </summary>


public class PH_HeartsDisplay : MonoBehaviour
{
    GameObject[] heartContainers; // list of the heart images
    PH_GameSession gameSession; // connect to the gameSession script


    void Awake()
    {
        gameSession = FindObjectOfType<PH_GameSession>(); //must connect to the gameSession script as soon as possible
        gameSession.SetHeartsDisplay(this);//set itself into the GameSession script
    }

    // Start is called before the first frame update
    void Start()
    {
        heartContainers = GameObject.FindGameObjectsWithTag("Heart");//get the hearts on the canvas
        
        DisplayCurrentHearts();//update hearts
    }

    public void DisplayCurrentHearts()
    {
        foreach (GameObject i in heartContainers) { i.SetActive(false); }//first delete all arts, 
        for (int i = 0; i < gameSession.GetCurrentLives(); i++) { heartContainers[i].SetActive(true); }// then display the correct number of hearts
    }
}
