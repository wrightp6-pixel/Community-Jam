using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class MoveRed : MonoBehaviour
{
    public InputSystem_Actions controls;
    private Vector2 moveDistance;
    public float moveSpeed;
    private Rigidbody2D rb;
    public bool onGround;
    public float jumpDistance;
    public bool jumpInput;
    public float jumpBuffer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get red's rigidbody2d component
        rb = GetComponent<Rigidbody2D>();

        // Activate the controls
        controls = new InputSystem_Actions();
        controls.RedMap.Enable();

        // Start the player with the ability to jump
        onGround = true;
    }

    void FixedUpdate()
    {
        // Get move value from the input system and then move the red player
        moveDistance = controls.RedMap.MoveRed.ReadValue<Vector2>();
        rb.linearVelocityX = moveDistance.x * moveSpeed;

        // If the player presses the jump button and is grounded, make red jump up
        // by increasing their vertical velocity and prevent them from jumping again
        if (onGround && jumpInput)
        {
            rb.linearVelocityY = jumpDistance;
            // Make it so jump only happens once at a time
            jumpInput = false;
            onGround = false;
        }

    }

    private void OnRedJump()
    {
        // Store the player's jump input for a short time
        jumpInput = true;
        StartCoroutine(WaitStopJump());

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Set onGround to true if the player touches the ground (or a hat)
        if (other.gameObject.CompareTag("Ground"))
        {
            StopAllCoroutines();
            onGround = true;
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
}
