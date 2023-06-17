using Unity.VisualScripting;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = other.gameObject.GetComponent<Rigidbody2D>();

            if (enemyRb != null)
            {
                Vector2 difference = enemyRb.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemyRb.AddForce(difference, ForceMode2D.Impulse);

                int damage = GameObject.FindWithTag("Player").GetComponent<CharacterStats>().baseAttack.GetValue();

                enemyRb.GetComponent<Enemy>().currentState = EnemyState.stagger;
                other.gameObject.GetComponent<Enemy>().Knock(enemyRb, knockTime, damage);
            }
        }

        if (other.gameObject.CompareTag("Breakable"))
        {
            other.gameObject.GetComponent<PotBehavior>().Hit();
        }
    }
}
