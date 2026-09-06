using UnityEngine;

public class UnitAbilityComponent : MonoBehaviour
{
    private BaseUnitStats unitStats;

    private void Awake()
    {
        unitStats = GetComponent<BaseUnitStats>();
    }
}