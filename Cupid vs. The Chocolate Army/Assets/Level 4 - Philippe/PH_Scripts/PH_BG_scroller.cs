using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script that makes the background scroll.
/// </summary>
public class PH_BG_scroller : MonoBehaviour
{
    [SerializeField] float BGspeed = 0;// scrolling speed
    Material myMaterial;//material to scroll
    Vector2 offset;//direction


    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;//get the currently assign material
        offset = new Vector2(0, BGspeed);//scroll downwards
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime; //each frame, change the position of the material
    }
}
