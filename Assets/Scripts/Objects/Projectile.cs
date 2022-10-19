using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Travel Variables")]
    public float moveSpeed;
    public Vector2 directionToMove;

    [Header("Projectile Lifetime")]
    public float projectileLifetime;
    private float remainingLifetime;

    [Header("Object Rigidbody")]
    public Rigidbody2D myRigidbody;


    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }


    public void Launch(Vector2 initialVelocity)
    {
        myRigidbody.velocity = initialVelocity * moveSpeed;
    }


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        remainingLifetime = projectileLifetime;
    }


    // Update is called once per frame
    void Update()
    {
        remainingLifetime -= Time.deltaTime;

        if (remainingLifetime <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
