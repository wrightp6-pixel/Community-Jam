using UnityEngine;

public class MoveGreen : MonoBehaviour
{
    
    public InputSystem_Actions controls;
    private Vector2 moveDistance;
    public float moveSpeed;

    
    void Start()
    {
        // Activate the controls
        controls = new InputSystem_Actions();
        controls.GreenMap.Enable();
    }

    void Update()
    {
        // Get move value from the input system and then move the green player
        moveDistance = controls.GreenMap.MoveGreen.ReadValue<Vector2>();
        transform.Translate(moveDistance * moveSpeed * Time.deltaTime);
    }
}
