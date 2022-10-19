using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    WALK,
    STAGGER,
    ATTACK,
    INTERACT
}


public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState; // Holds current state information for state machine use


    public VectorValue playerStartPosition; // Determines where the player will spawn in a loaded scene
    public float speed; // The speed at which the player moves


    // TODO Break off health system to its own component
    //public IntValue playerHealth; // The current health of the player
    //public Signal playerHealthSignal; // Used to trace current health
    //public Signal playerHitSignal; // Alerts appropriate objects if the player has been hit

    // TODO Break off the player inventory
    public Inventory playerInventory; // Player's current inventory
    public SpriteRenderer receivedItemSprite; // The sprite for the received item

    // TODO Player hit should be part of the health systeM?
    

    // TODO Player magic should be part of the magic system
    public Signal playerCastMagic; // Raises a signal if the player casts magic


    // TODO break this off with the player ability system
    [Header("Projectile Variables")]
    public GameObject projectile; // Reference to a projectile the player can fire
    public Item bow;

    private Rigidbody2D playerRigidBody2D; // Reference to the rigidbody attached to the gameobject
    private Vector2 movement; // Contains the frame movement
    private Animator playerAnimator; // The animator used by the player to cycle appropriate animations
    private IEnumerator attackCoroutine; // Stores AttackCo for execution
    private IEnumerator knockCoroutine; // Stores KnockCo for execution


    // TODO Break off invun frame stuff into its own script
    [Header("Invulnerability Frame Variables")]
    public Color flashColor;
    public Color defaultPlayerColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer playerSpriteRenderer;
    private IEnumerator flashCoroutine; // Stores FlashCo for execution


    private IEnumerator AttackCo(float time)
    {
        playerAnimator.SetBool("isAttacking", true);
        currentState = PlayerState.ATTACK;
        yield return null;
        playerAnimator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(time);
        if(currentState != PlayerState.INTERACT)
        {
            currentState = PlayerState.WALK;
        }   
    }


    // TODO this should be part of the ability itself
    private Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(playerAnimator.GetFloat("moveY"), playerAnimator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }


    // TODO this should be part of the ability itself
    private void MakeArrow()
    {
        if (playerInventory.currentMagic > 0)
        {
            Vector2 temp = new Vector2(playerAnimator.GetFloat("moveX"), playerAnimator.GetFloat("moveY"));
            Arrow newArrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            newArrow.Setup(temp, ChooseArrowDirection());

            // Raise the signal and reduce the magic one the arrow has been instantiated and set up
            playerCastMagic.Raise();
            playerInventory.ReduceMagic(newArrow.magicCost);
        }
    }


    private IEnumerator RangedAttackCo(float time)
    {
        //playerAnimator.SetBool("isAttacking", true);
        currentState = PlayerState.ATTACK;
        yield return null;
        //playerAnimator.SetBool("isAttacking", false);
        MakeArrow();
        yield return new WaitForSeconds(time);
        if (currentState != PlayerState.INTERACT)
        {
            currentState = PlayerState.WALK;
        }
    }


    // TODO move player flashing to own script
    public IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;

        while (temp < numberOfFlashes)
        {
            playerSpriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            playerSpriteRenderer.color = defaultPlayerColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }

        triggerCollider.enabled = true;
    }


    // TODO Move the knock to its own script
    public IEnumerator KnockCo(float knockTime)
    {
        // TODO HEALTH
        //playerHitSignal.Raise();

        if ((playerRigidBody2D != null) && (currentState != PlayerState.STAGGER))
        {
            flashCoroutine = FlashCo();
            StartCoroutine(flashCoroutine);
            yield return new WaitForSeconds(knockTime);
            currentState = PlayerState.WALK;
            playerRigidBody2D.velocity = Vector2.zero;
        }
    }


    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.INTERACT)
            {
                playerAnimator.SetBool("isReceivingItem", true);
                currentState = PlayerState.INTERACT;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                playerAnimator.SetBool("isReceivingItem", false);
                currentState = PlayerState.WALK;
                receivedItemSprite.sprite = null;
                playerInventory.AddItem(playerInventory.currentItem);
                playerInventory.currentItem = null;
            }
        }
    }


    //public void TakeDamage(int damage)
    //{
    //    playerHealth.runTimeValue -= damage;
    //    playerHealthSignal.Raise();
    //}


    void Attacking()
    {
        if ((Input.GetButtonDown("Attack")) && 
            (currentState != PlayerState.ATTACK) && (currentState != PlayerState.STAGGER))
        {
            attackCoroutine = AttackCo(0.3f);
            StartCoroutine(attackCoroutine);
        }
        else if ((Input.GetButtonDown("RangedAttack")) &&
                 (currentState != PlayerState.ATTACK) && (currentState != PlayerState.STAGGER))
        {
            if (playerInventory.CheckForItem(bow))
            {
                attackCoroutine = RangedAttackCo(0.3f);
                StartCoroutine(attackCoroutine);
            }
        }
    }


    // TODO Move the knock to its own script
    public void Knock(float knockTime)
    {
        knockCoroutine = KnockCo(knockTime);
        StartCoroutine(knockCoroutine);
        //// TODO HEALTH
        //TakeDamage(damage);

        //if ((currentState != PlayerState.STAGGER) &&
        //    (playerHealth.runTimeValue > 0))
        //{
        //    knockCoroutine = KnockCo(knockTime);
        //    StartCoroutine(knockCoroutine);
        //}
        //else
        //{
        //    this.gameObject.SetActive(false);
        //}
    }


    void UpdateMovementVector()
    {
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
    }


    void AddMovement()
    {
        playerRigidBody2D.MovePosition((new Vector2(transform.position.x,
                                                    transform.position.y)) +
                                                    movement *
                                                    speed *
                                                    Time.deltaTime);
    }


    void UpdateAnimator()
    {
        playerAnimator.SetFloat("moveX", movement.x);
        playerAnimator.SetFloat("moveY", movement.y);
        playerAnimator.SetBool("isMoving", true);
    }


	// Grabs references to attached components and initializes movement vector
	void Start ()
    {
        currentState = PlayerState.WALK;
        playerRigidBody2D = this.GetComponent<Rigidbody2D>();

        // Animator reference and setup of an initial value to prevent all direction attacks
        playerAnimator = this.GetComponent<Animator>();
        playerAnimator.SetFloat("moveY", -1);

        // Move the player character before they appear in the scene to an appropriate location
        transform.position = playerStartPosition.runTimeValue;

        movement = Vector2.zero;
        attackCoroutine = null;
        knockCoroutine = null;
	}
	

	void Update ()
    {
        // Exit update if the player is in an interaction state
        if (currentState == PlayerState.INTERACT)
        {
            return;
        }

        UpdateMovementVector();
        Attacking();

        if ((movement != Vector2.zero) && (currentState == PlayerState.WALK))
        {
            AddMovement();
            UpdateAnimator();
        }
        else
        {
            playerAnimator.SetBool("isMoving", false);
            movement = Vector2.zero;

            if ((playerRigidBody2D.velocity != Vector2.zero) && (currentState != PlayerState.STAGGER))
            {
                playerRigidBody2D.velocity = Vector2.zero;
            } 
        }
	}
}
