using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject fighterPrefab;
    [SerializeField] private GameObject rangerPrefab;
    [SerializeField] private GameObject cavalierPrefab;
    [SerializeField] private GameObject gatePrefab;
    [SerializeField] private UnitManager unitManager;

    private Queue<GameObject> spawnQueue;

    private void Awake()
    {
        spawnQueue = new Queue<GameObject>(new[]
        {
            fighterPrefab,
            //rangerPrefab,
            //cavalierPrefab,
            //fighterPrefab
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
        if (GameManager.main.isGameOver || spawnQueue.Count == 0) return;

        ITargetable nextUnit = spawnQueue.Peek().GetComponent<ITargetable>();

        Func<bool> spawnUnit = null;

        switch(nextUnit)
        {
            case FighterStats:
                spawnUnit = UnitManager.main.SpawnNorthFighter;
                break;
            case RangerStats:
                spawnUnit = UnitManager.main.SpawnNorthRanger;
                break;
            case CavalierStats:
                spawnUnit = UnitManager.main.SpawnNorthCavalier;
                break;
        }

        if (spawnUnit != null)
        {
            bool spawningSucess = spawnUnit();
            if(spawningSucess)
            {
                spawnQueue.Dequeue();
                //spawnQueue.Enqueue(nextUnit.GetGameObject());
            }
        }
    }

}