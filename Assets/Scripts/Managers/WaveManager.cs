using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager main;

    [Header("Settings")]
    [SerializeField] private Transform northSpawn;
    [SerializeField] private int totalWaves = 100;
    [SerializeField] private float timeBetweenWaves = 10f;

    [Header("Prefabs")]
    [SerializeField] private GameObject fighterPrefab;
    [SerializeField] private GameObject rangerPrefab;
    [SerializeField] private GameObject cavalierPrefab;
    [SerializeField] private GameObject gatePrefab;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(main);
            return;
        }
        main = this;
    }

    public void StartWaves()
    {
        StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
        while (currentWaveIndex < totalWaves)
        {
            var wave = GenerateWave(currentWaveIndex);
            yield return StartCoroutine(SpawnWave(wave));
            currentWaveIndex++;

            yield return new WaitForSeconds(timeBetweenWaves);
        }
        Debug.Log("You won! You cheater...");
    }

    private IEnumerator SpawnWave(WaveDefinition wave)
    {
        if(GameManager.main.isGameOver != true)
        {
            isSpawning = true;
            Debug.Log($"Spawning Wave {currentWaveIndex + 1}: {wave.enemiesToSpawn.Count} groups");

            foreach(var group in wave.enemiesToSpawn)
            {
                for(int i = 0; i < group.count; i++)
                {
                    UnitManager.main.SpawnUnit(group.prefab, northSpawn, Team.North);
                    yield return new WaitForSeconds(group.spawnDelay);
                }
            }
            isSpawning = false; 
        }
    }

    private WaveDefinition GenerateWave(int index)
    {
        var wave = new WaveDefinition
        {
            enemiesToSpawn = new List<EnemyGroup>(),
        };

        //scaling
        int fighterCount = 3 + index / 2;
        int rangerCount = 3 + index / 2;
        int cavalierCount = 3 + index / 2;
        int testCount = 1;

        //float spawnDelay = Mathf.Clamp(1.5f - (index * 0.02f), 0.2f, 1.5f);
        float spawnDelay = 0.5f;

        //customise for special waves
        if ((index + 1) % 10 == 0)
        {
            //spawn boss
        }
        else
        {
            //build waves here
            //wave.enemiesToSpawn.Add(new EnemyGroup(fighterPrefab, fighterCount, spawnDelay));
            //wave.enemiesToSpawn.Add(new EnemyGroup(rangerPrefab, rangerCount, spawnDelay));
            //wave.enemiesToSpawn.Add(new EnemyGroup(cavalierPrefab, cavalierCount, spawnDelay));
            wave.enemiesToSpawn.Add(new EnemyGroup(cavalierPrefab, testCount, spawnDelay));
        }
        return wave;
    }

    public bool IsWaveActive() => isSpawning;
    public int GetCurrentWaveNumber() => currentWaveIndex + 1;
}