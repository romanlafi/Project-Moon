using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth { get; private set; }

    public Stat baseAttack;
    public Stat moveSpeed;

    void Awake ()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage (int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage");

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    public virtual void Die () 
    {
        Debug.Log(transform.name + "died");
    }
}
