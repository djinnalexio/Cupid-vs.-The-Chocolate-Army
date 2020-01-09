using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A code to make an object it is attached to spin
/// </summary>

public class PH_Spinner : MonoBehaviour
{
    [SerializeField] [Tooltip("Negative->CW\nPositive->CCW")] float RotationPerSecond = 0;

    void Update() { gameObject.transform.Rotate(0, 0, RotationPerSecond * 360 * Time.deltaTime); }
}
