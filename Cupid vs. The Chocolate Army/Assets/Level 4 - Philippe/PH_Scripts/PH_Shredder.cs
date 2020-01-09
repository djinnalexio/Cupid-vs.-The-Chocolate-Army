using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The script that contains the methods to activate the shredder
/// </summary>
public class PH_Shredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) { Destroy(collision.gameObject); }//destroy any object that touches the bounds.
}
