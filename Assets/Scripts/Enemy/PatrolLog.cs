using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;


    private void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
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


    public override void CheckDistance()
    {
        if ((Vector3.Distance(targetPosition.position, this.transform.position) <= chaseRadius) &&
            (Vector3.Distance(targetPosition.position, this.transform.position) > attackRadius))
        {
            if (((currentState == EnemyState.IDLE) || (currentState == EnemyState.WALK)) &&
                 (currentState != EnemyState.STAGGER))
            {
                Vector3 moveLogTowards = Vector3.MoveTowards(this.transform.position,
                                             targetPosition.position,
                                             enemySpeed * Time.deltaTime);

                UpdateAnimMoveDirection(moveLogTowards - transform.position);
                logRb.MovePosition(moveLogTowards);

                //ChangeState(EnemyState.WALK);
                anim.SetBool("isAwake", true);
            }
        }
        else if (Vector3.Distance(targetPosition.position, transform.position) > chaseRadius)
        {
            if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            {
                Vector3 moveLogTowards = Vector3.MoveTowards(this.transform.position,
                 path[currentPoint].position,
                 enemySpeed * Time.deltaTime);

                UpdateAnimMoveDirection(moveLogTowards - transform.position);
                logRb.MovePosition(moveLogTowards);
            }
            else
            {
                ChangeGoal();
            }
        }
    }
}
