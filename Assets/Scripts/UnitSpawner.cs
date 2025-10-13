using Unity.VisualScripting;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("North Reference")]
    [SerializeField] private Transform northSpawn;

    [Header("South Reference")]
    [SerializeField] private Transform southSpawn;
    [SerializeField] private Transform southGateClose;
    [SerializeField] private Transform southGateIntermediate;
    [SerializeField] private Transform southGateFar;

    [Header("Prefabs")]
    [SerializeField] private GameObject fighterPrefab;
    [SerializeField] private GameObject rangerPrefab;
    [SerializeField] private GameObject cavalierPrefab;
    [SerializeField] private GameObject gatePrefab;


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
    public void SpawnNorthGateCloseUI()
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
    public void SpawnSouthGateCloseUI()
    {
        SpawnSouthGateClose();
    }
    public void SpawnSouthGateIntermediateUI()
    {
        SpawnSouthGateIntermediate();
    }
    public void SpawnSouthGateFarUI()
    {
        SpawnSouthGateFar();
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
    public bool SpawnSouthGateClose()
    {
        return SpawnUnit(gatePrefab, southGateClose, Team.South);
    }
    public bool SpawnSouthGateIntermediate()
    {
        return SpawnUnit(gatePrefab, southGateIntermediate, Team.South);
    }
    public bool SpawnSouthGateFar()
    {
        return SpawnUnit(gatePrefab, southGateFar, Team.South);
    }
    public void UpgradeIncome()
    {
        if (GameManager.main.currency[Team.South] >= GameManager.main.incomeUpgradeCost)
        {
            GameManager.main.UpgradeIncomeModifier();
            GameManager.main.SubtractCurrency(Team.South, GameManager.main.incomeUpgradeCost);
        }
        else
        {
            Debug.Log($"{Team.South} - Insufficient currency");
        }
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
            SpriteRenderer sr = unit.GetComponent<SpriteRenderer>();

            if (sr != null) 
            {
                string teamColorCode = team == Team.North ? "#1E3A8A" : "#DC2626";
                Color teamColor;

                if (UnityEngine.ColorUtility.TryParseHtmlString(teamColorCode, out teamColor))
                {
                    sr.color = teamColor;
                }
            }
            
            UnitStats unitStats = unit.GetComponent<UnitStats>();

            unitStats.GetComponent<UnitStats>().Team = team;

            if(prefab != gatePrefab)
            {
                unit.layer = LayerMask.NameToLayer(team.ToString() + "Team");
            }

            GameManager.main.SubtractCurrency(team, stats.Cost);

            return true;
        }
        else
        {
            if(team == Team.South)
            {
                Debug.Log($"{team} - Insufficient currency");
            }

            return false;
        }
    }
}   

