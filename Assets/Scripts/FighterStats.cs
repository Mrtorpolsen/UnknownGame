using UnityEngine;

public class FighterStats : UnitStats, IUnit
{
    [Header("Reference")]
    [SerializeField] public GameObject unit;
    [SerializeField] FloatingHealthBar healthBar;
    
    [Header("Attributes")]
    [SerializeField] public int cost = 50;
    [SerializeField] public int maxHealth = 200;
    [SerializeField] public int currentHealth;
    [SerializeField] public int attackDamage = 20;
    [SerializeField] public float attackSpeed = 1f;
    [SerializeField] public float attackRange = 0.25f;
    [SerializeField] public float hitRadius = 0.135f;
    [SerializeField] public float movementSpeed = 2f;


    public override Team Team { get; set; }
    public override int Cost => cost;
    public GameObject GetGameObject() => gameObject;
    public Team GetTeam() => Team;
    public float GetAttackRange() => attackRange;
    public int GetAttackDamage() => attackDamage;
    public float GetAttackSpeed() => attackSpeed;
    public bool GetIsAlive() => currentHealth > 0;
    public float GetHitRadius() => hitRadius;
    public float GetMovementSpeed() => movementSpeed;

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

}
