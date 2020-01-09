using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script that controls how waves of enemies are spawned
/// </summary>

public class PH_EnemySpawner : MonoBehaviour
{
    [SerializeField] List<PH_WaveConfig> WaveConfigs;
    bool Looping;
    
    public void SetWaveLooping(bool LoopingState) { Looping = LoopingState; }

    // Start is called before the first frame update
    IEnumerator Start()//turns the start into a coroutine
    {
        Looping = true;
        do { yield return StartCoroutine(SpawnAllWaves()); }
        while (Looping); // the routine won't end until looping is turned off
    }

    IEnumerator SpawnAllWaves()//takes the config files and spawn each wave one by one
    {
        for (int waveIndex = 0; waveIndex < WaveConfigs.Count; waveIndex++)
        {
            var currentWave = WaveConfigs[waveIndex];//take the current file to spawn its wave
            yield return StartCoroutine(SpawnEnemyUnit(currentWave));//finish this action before moving on
        }
    }

    IEnumerator SpawnEnemyUnit(PH_WaveConfig waveConfig)
    {
        string WaveTag = waveConfig.name.Replace(" ", "");//create the tag by removing the spaces
        int OnScreenEnemyCount;
        int currentUnit = waveConfig.GetEnemyUnitCount();
        do // add at leat one enemy each cycle
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
            waveConfig.GetWayPoints()[0].transform.position,
            Quaternion.identity);//creates the new object froma prefab, initial position is the first waypoint, default rotation

            newEnemy.GetComponent<PH_EnemyPath>().SetWaveConfig(waveConfig); //gives the information from the enemy template to the enemy path script
            newEnemy.tag = WaveTag;//assign the tag to the enemy

            currentUnit--;
            OnScreenEnemyCount = GameObject.FindGameObjectsWithTag(WaveTag).Length; //returns the length of the list of all gameObjects that wear this tag
            yield return new WaitForSeconds(waveConfig.GetSpawnRate());//wait the delay before cllaing the next foe
        }
        while (OnScreenEnemyCount < waveConfig.GetEnemyUnitCount() && currentUnit > 0);
    }
       
}