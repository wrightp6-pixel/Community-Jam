using UnityEngine;

public class MoveGreen : MonoBehaviour
{
    public InputSystem_Actions controls;
    private Vector2 moveDistance;
    public float moveSpeed;
    private Rigidbody2D rb;
    public bool onGround = true;
    public float jumpDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get green's rigidbody2d component
        rb = GetComponent<Rigidbody2D>();

        // Activate the controls
        controls = new InputSystem_Actions();
        controls.GreenMap.Enable();
    }

    void FixedUpdate()
    {
        // Get move value from the input system and then move the green player
        // by setting their horizontal velocity
        moveDistance = controls.GreenMap.MoveGreen.ReadValue<Vector2>();

        rb.linearVelocityX = moveDistance.x * moveSpeed;

    }

    private void OnGreenJump()
    {
        // If the player presses the jump button, make green jump up
        // by increasing their vertical velocity
        Debug.Log("Jump");
        rb.linearVelocityY = jumpDistance;
    }
}
