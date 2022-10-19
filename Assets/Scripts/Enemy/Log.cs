using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    protected Rigidbody2D logRb;

    [Header("Current Target Position")]
    protected Transform targetPosition;

    [Header("State Change Distances")]
    public float chaseRadius;
    public float attackRadius;

    [Header("Animator")]
    public Animator anim;


    public void UpdateAnimMoveDirection(Vector3 direction)
    {
        direction = direction.normalized;
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }


    public virtual void CheckDistance()
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
                anim.SetBool("isAwake", true);
            }
        }
        else
        {
            anim.SetBool("isAwake", false);
        }
    }


	// Use this for initialization
	void Start ()
    {
        currentState = EnemyState.IDLE;
        logRb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        //originPosition = this.transform;
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
        anim.SetBool("isAwake", true);
	}
	

	// Update is called once per frame
	void FixedUpdate ()
    {
        CheckDistance();
	}
}
