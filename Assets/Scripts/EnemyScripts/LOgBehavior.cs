using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOgBehavior : Enemy
{
    public Rigidbody2D rigidbody;
    public Transform target;
    public float ChaseRadius;
    public float AttackRadius;
    public Transform homeposition;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        CurrentState = EnemyState.idle;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        animator.SetBool("WakeUp", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        if (Vector3.Distance(target.position,transform.position)<= ChaseRadius && Vector3.Distance(target.position,transform.position)>AttackRadius)
        {
            if (CurrentState ==EnemyState.idle || CurrentState == EnemyState.walk && CurrentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                rigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                animator.SetBool("WakeUp", true);
            }
        }
        
        else if (Vector3.Distance(target.position, transform.position) > ChaseRadius)
        {
            animator.SetBool("WakeUp", false);
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        animator.SetFloat("MoveX", setVector.x);
        animator.SetFloat("MoveY", setVector.y);
    }

    public void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x)> Mathf.Abs(direction.y))
        {
            if (direction.x>0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if(direction.x<0)
            {
                SetAnimFloat(Vector2.left);

            }
        }
        else if(Mathf.Abs(direction.x)< Mathf.Abs(direction.y))
        {

            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);

            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);

            }
        }
    }

    public void ChangeState(EnemyState NewState)
    {
        if (CurrentState!= NewState)
        {
            CurrentState = NewState;
        }
    }

}
