// Author  : CS Dev
// Purpose : This script controls the "knockback" an enemy will receive when
//           hit by the sword the player character uses, taking public inputs
//           for the force applied and how long it's applied for

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float thrust; // Distance the enemy will move
    [SerializeField] private float knockTime; // Time the enemy will move
    [SerializeField] private string otherTag;
    //public int damageValue; // The damage value used


    private void OnTriggerEnter2D(Collider2D other)
    {
        //if ((other.gameObject.CompareTag("Breakable")) && (!this.gameObject.CompareTag("Enemy")))
        //{
        //    other.GetComponent<Pot>().BreakPot();
        //}

        if ((other.gameObject.CompareTag(otherTag)) && (other.isTrigger))
        {
            Rigidbody2D enemyRb = other.GetComponentInParent<Rigidbody2D>();

            if (enemyRb != null)
            {
                
                if ((other.gameObject.CompareTag("Enemy")) &&
                    (other.isTrigger) &&
                    (enemyRb.GetComponent<Enemy>().currentState != EnemyState.STAGGER))
                {
                    enemyRb.GetComponent<Enemy>().Knock(enemyRb, knockTime);
                    enemyRb.GetComponent<Enemy>().ChangeState(EnemyState.STAGGER);

                    // The tutorial said not to "do these differently", but force shouldn't be added to staggered targets
                    Vector3 difference = enemyRb.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    enemyRb.DOMove(enemyRb.transform.position + difference, knockTime);
                    //enemyRb.AddForce(difference, ForceMode2D.Impulse);
                }
            
                if (enemyRb.GetComponentInParent<PlayerMovement>().currentState != PlayerState.STAGGER)
                {
                    enemyRb.GetComponentInParent<PlayerMovement>().Knock(knockTime);
                    enemyRb.GetComponent<PlayerMovement>().currentState = PlayerState.STAGGER;

                    Vector2 difference = enemyRb.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    enemyRb.AddForce(difference, ForceMode2D.Impulse);
                }
            }
        }
    }
}
