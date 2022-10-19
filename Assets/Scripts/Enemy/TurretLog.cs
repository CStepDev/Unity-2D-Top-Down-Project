using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLog : Log
{
    public GameObject logProjectile;
    public float fireRate;
    private float fireDelay;
    private bool canFire = true;


    private void Update()
    {
        fireDelay -= Time.deltaTime;

        if (fireDelay <= 0f)
        {
            canFire = true;
            fireDelay = fireRate;
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
                if (canFire)
                {
                    Vector3 tempVector = targetPosition.transform.position - transform.position;

                    GameObject current = Instantiate(logProjectile, transform.position, Quaternion.identity);
                    current.GetComponent<Projectile>().Launch(tempVector);
                    canFire = false;
                }

                ChangeState(EnemyState.WALK);
                anim.SetBool("isAwake", true);
            }
        }
        else
        {
            anim.SetBool("isAwake", false);
        }
    }
}
