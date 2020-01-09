using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GC_HealthDisplay : MonoBehaviour
{
    Text healthText;
    GC_Player player;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<GC_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = Mathf.Clamp(player.GetHealth(),0, Mathf.Infinity).ToString();
    }
}

