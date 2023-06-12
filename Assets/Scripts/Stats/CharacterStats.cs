using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth { get; private set; }

    public Stat baseAttack;
    public Stat moveSpeed;

    public HealthBar healthBar;

    void Start ()
    {
        currentHealth = maxHealth;

        if (healthBar != null) healthBar.SetMaxHealth(maxHealth);

    }

    public void TakeDamage (int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        if (healthBar != null) healthBar.SetHealth(currentHealth);

        Debug.Log(transform.name + " takes " + damage + " damage");

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    public virtual void Die () 
    {
        transform.gameObject.SetActive(false);
        Debug.Log(transform.name + " died");
    }
}
