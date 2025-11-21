using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class MoveRed : MonoBehaviour
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
        // Get red's rigidbody2d component
        rb = GetComponent<Rigidbody2D>();

        // Activate the controls
        controls = new InputSystem_Actions();
        controls.RedMap.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Get move value from the input system and then move the red player
        moveDistance = controls.RedMap.MoveRed.ReadValue<Vector2>();

        rb.linearVelocity = moveDistance * moveSpeed * Time.fixedDeltaTime;
 
    }

    private void OnRedJump()
    {
        Debug.Log("Jump");
        rb.linearVelocityY = jumpDistance;
        
    }
}
