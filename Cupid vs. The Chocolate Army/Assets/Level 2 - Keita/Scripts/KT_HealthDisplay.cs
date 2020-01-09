using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KT_HealthDisplay : MonoBehaviour
{

    Text healthText;
    KT_Player player;

    // Use this for initialization
    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<KT_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }

}

