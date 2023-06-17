using UnityEngine;
using System.Collections;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public string enemyName;
    protected CharacterStats stats;

    public EnemyState currentState;

    void Awake ()
    {
        stats = GetComponent<CharacterStats>(); 
    }

    public void Knock(Rigidbody2D enemyRb, float knockTime, int damage)
    {
        StartCoroutine(KnockCo(enemyRb, knockTime));
        stats.TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D enemyRb, float knockTime)
    {
        if (enemyRb != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemyRb.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            enemyRb.velocity = Vector2.zero;
        }
    }

    public void ChangeState (EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
