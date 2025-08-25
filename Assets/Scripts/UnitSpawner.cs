using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] public GameObject fighterPrefab;
    [SerializeField] public GameObject rangerPrefab;
    [SerializeField] private Transform northSpawn;
    [SerializeField] private Transform southSpawn;


    public void SpawnNorthFighter()
    {
        SpawnUnit(fighterPrefab, northSpawn, Team.North);
    }
    public void SpawnNorthRanger()
    {
        SpawnUnit(rangerPrefab, northSpawn, Team.North);
    }
    public void SpawnSouthFighter()
    {
        SpawnUnit(fighterPrefab, southSpawn, Team.South);
    }
    public void SpawnSouthRanger()
    {
        SpawnUnit(rangerPrefab, southSpawn, Team.South);
    }

    public void SpawnUnit(GameObject prefab, Transform spawnPoint, Team team)
    {
        UnitStats stats = prefab.GetComponent<UnitStats>();

        if (stats == null)
        {
            Debug.LogError("Prefab has no UnitStats component!");
            return;
        }

        if (GameManager.main.currency[team] >= stats.Cost)
        {
            GameObject unit = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            UnitStats unitStats = unit.GetComponent<UnitStats>();

            unitStats.GetComponent<UnitStats>().Team = team;
            unit.layer = LayerMask.NameToLayer(team.ToString() + "Team");

            GameManager.main.SubtractCurrency(team, stats.Cost);
        }
        else
        {
            Debug.Log($"{team} - Insufficient currency");
        }
    }
}   

