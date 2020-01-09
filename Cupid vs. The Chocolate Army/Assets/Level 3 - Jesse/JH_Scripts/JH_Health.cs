using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JH_Health : MonoBehaviour
{
    Text healthText;
    JH_Player player;
    
  
    // Use this for initialization
    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<JH_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }
}
