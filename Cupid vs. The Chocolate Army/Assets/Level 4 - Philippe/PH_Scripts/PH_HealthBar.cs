using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The code to control the health bar and allow it to represent current health.
/// </summary>
public class PH_HealthBar : MonoBehaviour
{
    PH_PlayerController player;
    [SerializeField] float criticalPointPercent = 0;//when bar starts flashing
    [SerializeField] [Range(0,2)] float FlashingRate = 0.3f;
    [SerializeField] Color flashingColor = Color.white;

    float healthLevel;
    bool criticalPointOn = false;
    Color progessColor;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PH_PlayerController>();//find the player
        progessColor = gameObject.GetComponentInChildren<Image>().color;//gets the color of the bar
    }

    // Update is called once per frame
    void Update()
    {
        healthLevel = Mathf.Clamp(player.GetHealth() / player.GetstartingHealth(), 0, 1);
        gameObject.transform.localScale = new Vector3(1, healthLevel, 0);
        //controls what health is displayed by controling the scale of the object
        
        if (!criticalPointOn && (healthLevel <= criticalPointPercent / 100))
        {
            criticalPointOn = true;
            StartCoroutine(FlashingBar());
        }//if critical point hasn't been set yet but health dropped below level, set the critical point true and start flashing the bar.
        //because it checks if critical point has been set, it won't start the coroutine each frame afterwards.
    }

    IEnumerator FlashingBar()
    {
        while (true)
        {
            gameObject.GetComponentInChildren<Image>().color = flashingColor;//change color
            yield return new WaitForSeconds(FlashingRate/2);//wait
            gameObject.GetComponentInChildren<Image>().color = progessColor;//change color
            yield return new WaitForSeconds(FlashingRate);//wait
        }//etc.
    }
}
