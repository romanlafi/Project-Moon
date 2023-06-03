using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotBehavior : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
