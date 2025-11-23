using System.Collections;
using UnityEngine;

public class MoveGreen : MonoBehaviour
{
    public GreenActions controls;
    private Vector2 moveDistance;
    public float moveSpeed;
    private Rigidbody2D rb;
    private bool onGround;
    public float jumpDistance;
    public bool jumpInput;
    public float jumpBuffer;
    private float lastPosition;
    public Animator anim;
    private bool canTeleport;
    public GameObject redPlayer;
    public MoveRed moveRed;
    public SpriteRenderer spriteRenderer;
    public Manager manager;
    private bool alive;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get red's rigidbody2d component
        rb = GetComponent<Rigidbody2D>();

        // Activate the controls
        controls = new GreenActions();
        controls.GreenMap.Enable();

        // Start the player with the ability to jump and not with the ability to teleport
        onGround = true;
        canTeleport = false;
        alive = true;
    }

    void FixedUpdate()
    {
        // Get move value from the input system and then move the green player
        moveDistance = controls.GreenMap.MoveGreen.ReadValue<Vector2>();
        rb.linearVelocityX = moveDistance.x * moveSpeed;

        // If the player presses the jump button and is grounded, make green jump up
        // by increasing their vertical velocity and prevent them from jumping again
        if (onGround && jumpInput)
        {
            rb.linearVelocityY = jumpDistance;
            // Make it so jump only happens once at a time
            jumpInput = false;
            onGround = false;
        }

        // Change animation state to idle when idle and moving when moving
        if (lastPosition != transform.position.x)
        {
            anim.SetBool("isMoving", true);
        }

        if (lastPosition == transform.position.x)
        {
            anim.SetBool("isMoving", false);
        }

        lastPosition = transform.position.x;

        // Change sprite to face direction of movement
        if (rb.linearVelocityX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.linearVelocityX < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (manager.health <= 0)
        {
            controls.Disable();
            alive = false;
        }

    }

    private void OnGreenJump()
    {
        if (alive)
        {
            // Store the player's jump input for a short time
            jumpInput = true;
            StartCoroutine(WaitStopJump());
        }
    }

    private void OnGreenTele()
    {
        if (canTeleport && alive)
        {
            moveRed.TeleportToGreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Set onGround to true if the player touches the ground (or a hat)
        if (other.gameObject.CompareTag("Ground"))
        {
            StopAllCoroutines();
            onGround = true;
        }
        else if (other.gameObject.CompareTag("Teleporter"))
        {
            canTeleport = true;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            manager.ChangeHealth();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Change onGround to false if the player leaves the ground
        if (other.gameObject.CompareTag("Ground") && transform.position.y > other.transform.position.y)
        {
            //Set onGround to false after a delay
            StartCoroutine(WaitOffGround());
        }
        else if (other.gameObject.CompareTag("Teleporter"))
        {
            canTeleport = false;
        }
    }

    IEnumerator WaitOffGround()
    {
        // Set onGround to false after a delay to create "Coyote Time" effect
        yield return new WaitForSeconds(jumpBuffer);
        onGround = false;
    }

    IEnumerator WaitStopJump()
    {
        // Set jumpInput to false after a delay so that there is a short time when jumpInput
        // is stored, but it is stopped after a delay (creating jump buffer)
        yield return new WaitForSeconds(jumpBuffer);
        jumpInput = false;
    }

    public void TeleportToRed()
    {
        transform.Translate(new Vector3(redPlayer.transform.position.x - transform.position.x, redPlayer.transform.position.y + 1.2f - transform.position.y, 0));
    }
}
