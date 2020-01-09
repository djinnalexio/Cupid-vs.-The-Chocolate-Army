using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script that contains the information for dealing damage
/// </summary>

public class PH_DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    public int GetDamage() { return damage; }

    public void Hit()
    {
        PH_Enemy EnemyScript = gameObject.GetComponent<PH_Enemy>();//check if the object has an enemy script
        if (!EnemyScript) { Destroy(gameObject); }//if it doesn't, just destroy it normally on contact
        
        else { EnemyScript.Die(); }//if it is an enemy, use it's 'Die' function.
    }

}
