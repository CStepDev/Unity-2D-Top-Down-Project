using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    IDLE,
    WALK,
    ATTACK,
    STAGGER
}


public class Enemy : MonoBehaviour
{
    [Header("State Machine")]
    public EnemyState currentState;

    [Header("Enemy Stats")]
    public string enemyName;
    public IntValue enemyMaxHealth;
    public int enemyHealth;
    public float enemySpeed;

    [Header("Origin Position")]
    public Vector2 homePosition;

    [Header("Death Signals")]
    public Signal roomSignal;

    [Header("Death Effects")]
    public LootTable enemyLoot;
    public GameObject deathEffect;

    private float deathEffectDelay = 1.0f;
    private IEnumerator coroutine; // Stores KnockCo for execution


    private IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockTime)
    {
        if ((myRigidBody != null) && (currentState != EnemyState.STAGGER))
        {
            yield return new WaitForSeconds(knockTime);
            ChangeState(EnemyState.IDLE);
            myRigidBody.velocity = Vector2.zero;
        }
    }


    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject newDeathEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(newDeathEffect, deathEffectDelay);
        }
    }


    private void MakeLoot()
    {
        if (enemyLoot != null)
        {
            Pickup current = enemyLoot.LootPowerup();

            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }


    private void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            DeathEffect();
            MakeLoot();

            // This is for rooms not intended to be locked when enemies aren't in them, no idea why this was
            // left out by Taft
            if (roomSignal != null)
            {
                roomSignal.Raise();
            }
            
            this.gameObject.SetActive(false);
        }
    }


    public void Knock(Rigidbody2D myRigidBody, float knockTime)
    {
        if (currentState != EnemyState.STAGGER)
        {
            coroutine = KnockCo(myRigidBody, knockTime);
            StartCoroutine(coroutine);
        }
    }


    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }


    private void OnEnable()
    {
        transform.position = homePosition;
        enemyHealth = enemyMaxHealth.initialValue;
        ChangeState(EnemyState.IDLE);
    }


    private void Awake()
    {
        enemyHealth = enemyMaxHealth.initialValue;
    }
}
