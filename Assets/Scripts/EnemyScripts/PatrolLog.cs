using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : LOgBehavior
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

   public override void CheckDistance()
    {
        if (Vector3.Distance(target.position,
                transform.position) <= ChaseRadius
            && Vector3.Distance(target.position,
                transform.position) > AttackRadius)
        {
            if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk && CurrentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                rigidbody.MovePosition(temp);


              // ChangeState(EnemyState.walk);
                animator.SetBool("WakeUp", true);
            }

        }
        else if (Vector3.Distance(target.position,transform.position) > ChaseRadius)
        {
            if (Vector3.Distance(transform.position, path[currentPoint].position)>roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, MoveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                rigidbody.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
         

        }

        
    }

    private void ChangeGoal()
    {
        if (currentPoint == path.Length -1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
