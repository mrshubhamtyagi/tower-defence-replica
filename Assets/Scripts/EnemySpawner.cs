using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform startPoint;
    public float countdown = 2f;
    //public float timeBtwWaves = 3f;
    //public float timeBtwEachEnemy = 1f;
    [Header("Enemy Waves")] public Wave[] waves;

    private int waveNumber;
    private int wavesSpawned = 0;
    private int waveSpawnedIndex;
    private bool isWaveRunning = false;

    private int totalEnemysSpawned = 0;

    private void Start()
    {
        wavesSpawned = waves.Length;
    }

    void Update()
    {
        if (countdown < 0 && wavesSpawned > 0)
        {
            if (!isWaveRunning)
            {
                SpawnWave();
            }

        }
        if (countdown > 0)
            countdown -= Time.deltaTime;
        else
            countdown = 0;

    }

    private void SpawnWave()
    {
        print(string.Format("Wave Number {0} Spawned", waveSpawnedIndex + 1));
        isWaveRunning = true;
        wavesSpawned--;
        StartCoroutine(SpawnEnemy(waves[waveSpawnedIndex]));
    }

    IEnumerator SpawnEnemy(Wave _wave)
    {
        // Current Wave Properties
        int _enemyCount = _wave.enemyCount;
        GameObject _enemyPrefab = _wave.enemyType;
        int _nextWaveTime = _wave.nextwaveTime;
        float _timeBtwEnemys = _wave.timeBtwEachEnemy;
        int _enemySpeed = _wave.enemySpeed;

        while (_enemyCount > 0)
        {
            GameObject enemy = Instantiate(_enemyPrefab, startPoint.localPosition, Quaternion.identity);
            enemy.transform.SetParent(transform);
            totalEnemysSpawned++;
            // Set current wave enemy's Speed
            enemy.GetComponent<EnemyScript>().SetEnemySpeed(_enemySpeed);
            yield return new WaitForSeconds(_timeBtwEnemys);

            _enemyCount--;
            if (_enemyCount == 0) // Current Wave is finished
            {
                isWaveRunning = false;
                countdown = _nextWaveTime;
                print(string.Format("Wave Number {0} finished", waveSpawnedIndex + 1));
            }
        }
        waveSpawnedIndex++;
    }
}


[Serializable]
public class Wave
{
    public GameObject enemyType;
    [Range(1, 30)] public int enemyCount = 1;
    [Range(.3f, 2f)] public float timeBtwEachEnemy = 0.3f;
    [Range(3, 10)] public int enemySpeed = 5;
    [Range(1, 10)] public int nextwaveTime = 3;
}
