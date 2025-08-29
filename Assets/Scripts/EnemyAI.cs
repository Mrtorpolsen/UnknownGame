using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject fighterPrefab;
    [SerializeField] private GameObject rangerPrefab;
    [SerializeField] private GameObject cavalierPrefab;
    [SerializeField] private UnitSpawner unitSpawner;

    private Queue<GameObject> spawnQueue;

    private void Awake()
    {
        spawnQueue = new Queue<GameObject>(new[]
        {
            fighterPrefab,
            rangerPrefab,
            cavalierPrefab,
            fighterPrefab
        });
    }

    private void Update()
    {
        if(GameManager.main.isGameRunning)
        {
            TrySpawnUnit();
        }
    }

    private void TrySpawnUnit()
    {
        if (GameManager.main.isGameOver) return;

        IUnit nextUnit = spawnQueue.Peek().GetComponent<IUnit>();

        Func<bool> spawnUnit = null;

        switch(nextUnit)
        {
            case FighterStats:
                spawnUnit = unitSpawner.SpawnNorthFighter;
                break;
            case RangerStats:
                spawnUnit = unitSpawner.SpawnNorthRanger;
                break;
            case CavalierStats:
                spawnUnit = unitSpawner.SpawnNorthCavalier;
                break;
        }

        if (spawnUnit != null)
        {
            bool spawningSucess = spawnUnit();
            if(spawningSucess)
            {
                spawnQueue.Dequeue();
                spawnQueue.Enqueue(nextUnit.GetGameObject());
            }
        }
    }

}