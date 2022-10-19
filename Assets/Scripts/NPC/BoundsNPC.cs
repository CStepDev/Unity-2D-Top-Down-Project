using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsNPC : Signpost
{
    public float moveSpeed;
    public Collider2D npcBounds;
    public float minMoveTimeSeconds;
    public float maxMoveTimeSeconds;
    public float minWaitTimeSeconds;
    public float maxWaitTimeSeconds;

    private Transform npcTransform;
    private Rigidbody2D npcRigidbody;
    private Animator npcAnimator;
    private bool isMoving;
    private Vector3 direction;
    private float moveTimeSeconds;

    private float waitTimeSeconds;


    private void ChangeDirection()
    {
        int newDirection = Random.Range(0, 4);

        switch (newDirection)
        {
            case 0:
                direction = Vector3.right;
                break;
            case 1:
                direction = Vector3.up;
                break;
            case 2:
                direction = Vector3.left;
                break;
            case 3:
                direction = Vector3.down;
                break;
            default:
                break;
        }
    }


    private void ChooseDifferentDirection()
    {
        Vector3 previousDirection = direction;
        ChangeDirection();
        int loopCount = 0;


        while ((direction == previousDirection) && (loopCount < 100))
        {
            loopCount++;
            ChangeDirection();
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        ChooseDifferentDirection();
    }


    private void Move()
    {
        Vector3 newPosition = npcTransform.position + direction * moveSpeed * Time.deltaTime;

        if (npcBounds.bounds.Contains(newPosition))
        {
            npcRigidbody.MovePosition(npcTransform.position + direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            ChangeDirection();
        }

        
    }


    private void UpdateAnimator()
    {
        npcAnimator.SetFloat("moveX", direction.x);
        npcAnimator.SetFloat("moveY", direction.y);
    }


    // Start is called before the first frame update
    private void Start()
    {
        npcTransform = GetComponent<Transform>();
        npcRigidbody = GetComponent<Rigidbody2D>();
        npcAnimator = GetComponent<Animator>();

        moveTimeSeconds = Random.Range(minMoveTimeSeconds, maxMoveTimeSeconds);
        waitTimeSeconds = Random.Range(minWaitTimeSeconds, maxWaitTimeSeconds);

        ChangeDirection();
    }


    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (isMoving)
        {
            moveTimeSeconds -= Time.deltaTime;

            if (moveTimeSeconds <= 0)
            {
                moveTimeSeconds = Random.Range(minMoveTimeSeconds, maxMoveTimeSeconds);
                isMoving = false;
            }

            if (!isActive)
            {
                Move();
                UpdateAnimator();
            }
        }
        else
        {
            waitTimeSeconds -= Time.deltaTime;

            if (waitTimeSeconds <= 0)
            {
                ChooseDifferentDirection();
                isMoving = true;
                waitTimeSeconds = Random.Range(minWaitTimeSeconds, maxWaitTimeSeconds);
            }
        }

    }
}
