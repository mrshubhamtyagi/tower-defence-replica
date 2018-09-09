using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //public Transform enemyPrefab;
    public Transform startPoint;
    public float countdown = 2f;
    public float timeBtwWaves = 3f;
    public float timeBtwEachEnemy = 1f;
    public Wave[] waves;

    private int waveNumber;
    private int wavesSpawned = 0;

    private int waveIndex;

    private void Start()
    {
        wavesSpawned = waves.Length;
    }

    void Update()
    {
        if (countdown <= 0 && wavesSpawned > 0)
        {
            SpawnWave();
            countdown = timeBtwWaves;

        }
        if (countdown > 0)
            countdown -= Time.deltaTime;

    }

    private void SpawnWave()
    {
        print("Wave Spawned");
        wavesSpawned--;
        StartCoroutine(SpawnEnemy(waves[waveIndex].enemyCount, waves[waveIndex].enemyPrefab));

    }

    IEnumerator SpawnEnemy(int _enemyCount, GameObject _enemy)
    {
        waveIndex++;
        while (_enemyCount > 0)
        {
            Instantiate(_enemy, startPoint.localPosition, Quaternion.identity);
            yield return new WaitForSeconds(timeBtwEachEnemy);
            _enemyCount--;
        }
    }


}


[Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public float timeBtwEachEnemy = 0.3f;
    public int enemyCount = 1;
}
