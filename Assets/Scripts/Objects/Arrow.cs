using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRB;
    public float lifetime;
    public float magicCost;

    private float lifetimeCounter;


    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRB.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }


    private void Update()
    {
        lifetimeCounter -= Time.deltaTime;

        if (lifetimeCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        lifetimeCounter = lifetime;
    }
}
