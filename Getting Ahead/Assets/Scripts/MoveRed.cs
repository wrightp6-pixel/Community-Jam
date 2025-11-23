using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

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
    public Animator anim;
    private float lastPosition;
    public bool canTeleport;
    public MoveGreen moveGreen;
    public GameObject greenPlayer;
    public Manager manager;
    private float distanceX;
    private float distanceY;
    public GameObject laserPiece;
    private float count;
    public SpriteRenderer spriteRenderer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get red's rigidbody2d component
        rb = GetComponent<Rigidbody2D>();

        // Activate the controls
        controls = new InputSystem_Actions();
        controls.RedMap.Enable();

        // Start the player with the ability to jump and not with the ability to teleport
        onGround = true;
        canTeleport = false;

    }

    private void Update()
    {
        if (manager.laserOn)
        {
            distanceX = greenPlayer.transform.position.x - transform.position.x;
            distanceY = greenPlayer.transform.position.y - transform.position.y;

            if(Mathf.Abs(distanceX) > 5)
            {
                count = 20;
            }
            else if (Mathf.Abs(distanceX) > 8)
            {
                count = 25;
            }
            else if (Mathf.Abs(distanceX) > 12)
            {
                count = 30;
            }
            else
            {
                count = 15;
            }

                for (float i = 1; i < count + 1; i++)
                {
                    Instantiate(laserPiece, new Vector3(transform.position.x + (distanceX * (i / count)), transform.position.y + (distanceY * (i / count)), -0.5f), transform.rotation);
                    //Instantiate(laserPiece, new Vector3(distanceX, transform.position.y + (distanceY * (i / 10)), -0.5f), transform.rotation);
                }
            manager.LaserFalse();
        }
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
        if( rb.linearVelocityX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.linearVelocityX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnRedJump()
    {
        // Store the player's jump input for a short time
        jumpInput = true;
        StartCoroutine(WaitStopJump());

    }

    private void OnRedTele()
    {
        if (canTeleport)
        {
            moveGreen.TeleportToRed();
        }
    }

    private void OnRedLaser()
    {
        if (manager.laserOn == false)
        {
            manager.LaserTrue();
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
        else if (other.gameObject.CompareTag("Flag1"))
        {
            controls.Disable();
            StartCoroutine(WaitCut2());
            
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

    IEnumerator WaitCut2()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("Cutscene1");

    }

    public void TeleportToGreen()
    {
        transform.Translate(new Vector3(greenPlayer.transform.position.x - transform.position.x, greenPlayer.transform.position.y + 1.2f - transform.position.y, 0));
    }
}
