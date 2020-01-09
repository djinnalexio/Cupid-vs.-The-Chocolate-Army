using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC_EnemySpwaner : MonoBehaviour
{

    [SerializeField] List<GC_WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
            
        
              
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

        }
    }

    private IEnumerator SpawnAllEnemiesInWave(GC_WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNUmberOfEnemies(); enemyCount++)
        {
           var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<GC_EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
