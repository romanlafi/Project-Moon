using UnityEngine;

public class LogBehavior : Enemy
{
    private Transform target;
    private Animator animator;

    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            //WakeUp();
            transform.position = Vector3.MoveTowards(transform.position, target.position, stats.moveSpeed.GetValue() * Time.deltaTime);
            animator.SetFloat("moveX", transform.position.x);
            animator.SetFloat("moveY", transform.position.y);
        }
    }

    void WakeUp()
    {
        animator.SetBool("wakeUp", true);
    }
}

