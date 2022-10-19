using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Log
{
    private IEnumerator attackCoroutine; // Stores AttackCo for execution


    public IEnumerator AttackCo()
    {
        currentState = EnemyState.ATTACK;
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.WALK;
        anim.SetBool("isAttacking", false);
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

                ChangeState(EnemyState.WALK);
            }
        }
        else if ((Vector3.Distance(targetPosition.position, this.transform.position) <= chaseRadius) &&
                 (Vector3.Distance(targetPosition.position, this.transform.position) <= attackRadius))
        {

            if (((currentState == EnemyState.IDLE) || (currentState == EnemyState.WALK)) &&
                (currentState != EnemyState.STAGGER))
            {
                attackCoroutine = AttackCo();
                StartCoroutine(attackCoroutine);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.IDLE;
        logRb = this.GetComponent<Rigidbody2D>();
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
