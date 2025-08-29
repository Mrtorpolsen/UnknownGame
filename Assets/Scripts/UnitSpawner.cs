using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform northSpawn;
    [SerializeField] private Transform southSpawn;

    [Header("Prefabs")]
    [SerializeField] private GameObject fighterPrefab;
    [SerializeField] private GameObject rangerPrefab;
    [SerializeField] private GameObject cavalierPrefab;


    public void SpawnNorthFigterUI()
    {
        SpawnNorthFighter();
    }
    public void SpawnNorthRangerUI()
    {
        SpawnNorthRanger();
    }
    public void SpawnNorthCavalierUI()
    {
        SpawnNorthCavalier();
    }
    public bool SpawnNorthFighter()
    {
        return SpawnUnit(fighterPrefab, northSpawn, Team.North);
    }
    public bool SpawnNorthRanger()
    {
        return SpawnUnit(rangerPrefab, northSpawn, Team.North);
    }
    public bool SpawnNorthCavalier()
    {
        return SpawnUnit(cavalierPrefab, northSpawn, Team.North);
    }
    public void SpawnSouthFigterUI()
    {
        SpawnSouthFighter();
    }
    public void SpawnSouthRangerUI()
    {
        SpawnSouthRanger();
    }
    public void SpawnSouthCavalierUI()
    {
        SpawnSouthCavalier();
    }
    public bool SpawnSouthFighter()
    {
        return SpawnUnit(fighterPrefab, southSpawn, Team.South);
    }
    public bool SpawnSouthRanger()
    {
        return SpawnUnit(rangerPrefab, southSpawn, Team.South);
    }
    public bool SpawnSouthCavalier()
    {
        return SpawnUnit(cavalierPrefab, southSpawn, Team.South);
    }

    public bool SpawnUnit(GameObject prefab, Transform spawnPoint, Team team)
    {
        UnitStats stats = prefab.GetComponent<UnitStats>();

        if (stats == null)
        {
            Debug.LogError("Prefab has no UnitStats component!");
            return false;
        }

        if (GameManager.main.currency[team] >= stats.Cost)
        {
            GameObject unit = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            UnitStats unitStats = unit.GetComponent<UnitStats>();

            unitStats.GetComponent<UnitStats>().Team = team;
            unit.layer = LayerMask.NameToLayer(team.ToString() + "Team");

            GameManager.main.SubtractCurrency(team, stats.Cost);

            return true;
        }
        else
        {
            Debug.Log($"{team} - Insufficient currency");

            return false;
        }
    }
}   

