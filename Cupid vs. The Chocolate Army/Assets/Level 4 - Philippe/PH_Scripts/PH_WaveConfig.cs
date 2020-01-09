using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object that contains information and parameters about
/// the configuration of an enemy wave unit
/// </summary>

[CreateAssetMenu(fileName = "waveconfig", menuName = "Enemy Wave Configuration")]

public class PH_WaveConfig : ScriptableObject
{
    [Header("Wave Configuration Settings")]
    [Space(10)]

    [Tooltip("Place the enemy template for this wave here")]
    [SerializeField] GameObject EnemyPrefab;

    [Header("Enemy Routes")]
    [Space(10)]
    [SerializeField]
    [Tooltip("Place the Path prefab for this wave here ")]
    GameObject PathPrefab;
    [SerializeField]
    [Tooltip("Place the Suicide Path prefab for this wave here ")]
    GameObject SuicidePathPrefab;
    [Tooltip("Starting Position if in loop")]
    [SerializeField] int StartLoopPosition;

    [Header("Spawning Parameters")]
    [Space(10)]
    [Tooltip("Time between each enemy spawn")]
    [SerializeField] float SpawnRate;
    [Tooltip("Number of enemies in this unit")]
    [SerializeField] [Range(0, 15)] int EnemyUnitCount;
    [SerializeField] [Range(0, 60)] float MoveSpeed;

    [Header("Suicide Mode Settings")]
    [Space(10)]
    [SerializeField] bool suicidal = false;
    [Tooltip("Increase in Move speed while in Suicide Mode")]
    [SerializeField] [Range(0,5)] float SuicideSpeedMultiplier = 1;
    [SerializeField]
    [Tooltip("How much can they deviate from the center while in Suicide Mode")]
    [Range(0, 18)] float SuicideLateralOffset = 0;

    //create methods to access values from this script:
    public GameObject GetEnemyPrefab() { return EnemyPrefab; }
    public float GetSpawnRate() { return SpawnRate; }
    public int GetEnemyUnitCount() { return EnemyUnitCount; }
    public float GetMoveSpeed() { return MoveSpeed; }
    public bool GetSuicidal() { return suicidal; }
    public float GetSuicideSpeedMultiplier() { return SuicideSpeedMultiplier; }
    public float GetSuicideLateralOffset() { return SuicideLateralOffset; }
    public int GetStartingPosition() { return StartLoopPosition; }
    public List<Transform> GetSuicideWayPoints()
    {
        if (SuicidePathPrefab)
        {
            var WaveRoute = new List<Transform>();
            foreach (Transform child in SuicidePathPrefab.transform) { WaveRoute.Add(child); }
            return WaveRoute;//creates a list, and adds the coordinates of each child to it
        }
        else { return null; }
    }
    public List<Transform> GetWayPoints()
        {
            var WaveRoute = new List<Transform>();
            foreach(Transform child in PathPrefab.transform) { WaveRoute.Add(child); }
            return WaveRoute;//creates a list, and adds the coordinates of each child to it
        } 
    
}
