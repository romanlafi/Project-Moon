using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {

            Rigidbody2D otherRb = other.gameObject.GetComponent<Rigidbody2D>();

            if (otherRb != null)
            {
                Vector2 difference = otherRb.transform.position - transform.position;
                difference = difference.normalized * thrust;
                otherRb.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("Enemy"))
                {
                    int damage = GameObject.FindWithTag("Player").GetComponent<CharacterStats>().baseAttack.GetValue();

                    otherRb.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.gameObject.GetComponent<Enemy>().Knock(otherRb, knockTime, damage);
                }

                if (other.gameObject.CompareTag("Player"))
                {
                    if (this.gameObject.GetComponent<Enemy>().currentState != EnemyState.stagger)
                    {
                        otherRb.GetComponent<PlayerController>().currentState = PlayerState.stagger;
                        other.gameObject.GetComponent<PlayerController>().Knock(knockTime);
                        other.gameObject.GetComponent<CharacterStats>().TakeDamage(this.gameObject.GetComponent<CharacterStats>().baseAttack.GetValue());
                    }
                  
                }
            }
        }

        if (other.gameObject.CompareTag("Breakable"))
        {
            other.gameObject.GetComponent<PotBehavior>().Hit();
        }
    }
}
