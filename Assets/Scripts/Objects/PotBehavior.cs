using System.Collections;
using UnityEngine;

public class PotBehavior : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Hit()
    {
        animator.SetBool("hit", true);
        StartCoroutine(HitCo());
    }

    IEnumerator HitCo()
    {
        yield return new WaitForSeconds(.33f);
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.relativeVelocity.magnitude >= 2.5f)
            {
                Hit();
            }
        } 
    }
}
