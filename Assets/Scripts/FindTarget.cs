using System.Linq;
using UnityEngine;

public class FindTarget : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private float detectionRange = 100f;
    [SerializeField] private ITargetable currentTarget;

    [Header("Debug")]
    [SerializeField] private GameObject debugTarget;

    private ITargetable selfUnit;

    private void Awake()
    {
        selfUnit = GetComponent<ITargetable>();

        currentTarget = GetEnemyCastle(selfUnit);
    }
    // Update is called once per frame
    void Update()
    {

        FindClosestTarget();
        debugTarget = currentTarget?.GetTransform()?.gameObject;

    }

    private void FindClosestTarget()
    {
        var allTargets = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ITargetable>();
        
        float closestDistance = Mathf.Infinity;
        ITargetable nearestEnemy = null;

        foreach (var target in allTargets)
        {
            if(target.GetTeam() == selfUnit.GetTeam())
            {
                continue;
            }

            Transform t = target.GetTransform();
            if (t != null)
            {

                float dist = Vector2.Distance(transform.position, target.GetTransform().position);
                if (dist < closestDistance && dist <= detectionRange)
                {
                    closestDistance = dist;
                    nearestEnemy = target;
                }
            }
        }
        currentTarget = nearestEnemy;
    }
    private ITargetable GetEnemyCastle(ITargetable selfUnit)
    {
        return currentTarget = (selfUnit.GetTeam() == Team.North)
           ? GameManager.main.south.GetComponent<ITargetable>()
           : GameManager.main.north.GetComponent<ITargetable>();
    }
    public ITargetable GetCurrentTarget() => currentTarget;
}
