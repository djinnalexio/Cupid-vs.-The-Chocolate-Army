using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// The code to get the health and display it on the UI.
/// </summary>

public class PH_HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI HealthDisplay;
    PH_PlayerController Player;
    float visibleHealth;

    // Start is called before the first frame update
    void Start()
    {
        HealthDisplay = GetComponent<TextMeshProUGUI>();//get the text component
        Player = FindObjectOfType<PH_PlayerController>();//get the player object

    }

    // Update is called once per frame
    void Update()
    {//display the current health but doesn't show negatives
        visibleHealth = Mathf.Clamp(Player.GetHealth(), 0, Mathf.Infinity);
        HealthDisplay.text = visibleHealth.ToString();
    }
}
