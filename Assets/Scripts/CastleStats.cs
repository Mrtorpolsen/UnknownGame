using UnityEngine;

public class CastleStats : MonoBehaviour, IUnit
{
    [Header("Reference")]
    [SerializeField] public GameObject castle;
    [SerializeField] FloatingHealthBar healthBar;

    [Header("Attributes")]
    [SerializeField] public int maxHealth = 500;
    [SerializeField] public int currentHealth;
    [SerializeField] public int attackDamage = 0;
    [SerializeField] public float attackSpeed = 0f;
    [SerializeField] public float attackRange = 0f;
    [SerializeField] public float hitRadius = 0.26f;

    [SerializeField] public Team team;
    public GameObject GetGameObject() => gameObject;
    public Team GetTeam() => team;
    public float GetAttackRange() => attackRange;
    public int GetAttackDamage() => attackDamage;
    public float GetAttackSpeed() => attackSpeed;
    public bool GetIsAlive() => currentHealth > 0;
    public float GetHitRadius() => hitRadius;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Awake()
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

    void Die()
    {
        Destroy(castle);
        GameManager.main.SetGameOver(true, team);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    FighterStats otherFighter = collision.gameObject.GetComponent<FighterStats>();

    //    if (otherFighter != null && otherFighter.team != this.team)
    //    {
    //        TakeDamage(otherFighter.attackDamage);
    //        otherFighter.Die();
    //    }
    //}

    public Transform GetTransform()
    {
        return (this != null) ? transform : null;
    }

    void ITargetable.Die()
    {
        Die();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }

}
