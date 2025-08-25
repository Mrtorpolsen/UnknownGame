using System;
using UnityEngine;

public class RangerStats : UnitStats, IUnit
{
    [Header("Reference")]
    [SerializeField] public GameObject unit;
    [SerializeField] FloatingHealthBar healthBar;
    [SerializeField] public GameObject arrowPrefab;

    [Header("Attributes")]
    [SerializeField] public int cost = 75;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int attackDamage = 15;
    [SerializeField] public int currentHealth;
    [SerializeField] public float attackRange = 1.5f;
    [SerializeField] public float attackSpeed = 0.5f;
    [SerializeField] public float hitRadius = 0.135f;

    private Combat combat;

    public override Team Team { get; set; }
    public override int Cost => cost;
    public GameObject GetGameObject() => gameObject;
    public Team GetTeam() => Team;
    public float GetAttackRange() => attackRange;
    public int GetAttackDamage() => attackDamage;
    public float GetAttackSpeed() => attackSpeed;
    public bool GetIsAlive() => currentHealth > 0;
    public float GetHitRadius() => hitRadius;
    public Transform GetTransform()
    {
        return (this != null) ? transform : null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (unit == null) return;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
    void Awake()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        combat = GetComponent<Combat>();
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(unit);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void Shoot(ITargetable target)
    {
        GameObject arrowObj = Instantiate(arrowPrefab, unit.transform.position, Quaternion.identity);
        Arrow arrowScript = arrowObj.GetComponent<Arrow>();
        arrowObj.layer = target.GetTeam() == Team.North ? LayerMask.NameToLayer("SouthTeamProjectile") : LayerMask.NameToLayer("NorthTeamProjectile");
        arrowScript.SetTarget(target);

        arrowScript.Init(this, attackDamage);
        arrowScript.OnHit += HandleArrowHit;
    }

    private void HandleArrowHit(ITargetable target, int damage)
    {
        Debug.Log("arrowhit");

        combat.ApplyProjectileDamage(target, damage);
    }
}
