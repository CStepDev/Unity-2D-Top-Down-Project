using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLog : Log
{
    public Collider2D boundary;

    public override void CheckDistance()
    {
        if ((Vector3.Distance(targetPosition.position, this.transform.position) <= chaseRadius) &&
            (Vector3.Distance(targetPosition.position, this.transform.position) > attackRadius) &&
            (boundary.bounds.Contains(targetPosition.transform.position)))
        {
            if (((currentState == EnemyState.IDLE) || (currentState == EnemyState.WALK)) &&
                 (currentState != EnemyState.STAGGER))
            {
                Vector3 moveLogTowards = Vector3.MoveTowards(this.transform.position,
                                             targetPosition.position,
                                             enemySpeed * Time.deltaTime);

                UpdateAnimMoveDirection(moveLogTowards - transform.position);
                logRb.MovePosition(moveLogTowards);

                ChangeState(EnemyState.WALK);
                anim.SetBool("isAwake", true);
            }
        }
        else if ((Vector3.Distance(targetPosition.position, transform.position) > chaseRadius) ||
                 (!boundary.bounds.Contains(targetPosition.transform.position)))
        {
            anim.SetBool("isAwake", false);
        }
    }
}
