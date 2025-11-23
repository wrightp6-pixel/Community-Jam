using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float xMax;
    public float xMin;
    public float moveSpeed;
    private bool moveRight;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public GameObject red;
    public GameObject green;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (red.transform.position.x > xMin - 15 || green.transform.position.x > xMin - 15)
        {
            // Switch direction after hitting certain point
            if (transform.position.x >= xMax)
            {
                moveRight = false;
                spriteRenderer.flipX = true;
            }

            if (transform.position.x <= xMin)
            {
                moveRight = true;
                spriteRenderer.flipX = false;
            }

            if (moveRight)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }

            // Destroy enemy if the player goes far enough away without already killing it
            if (red.transform.position.x > xMax + 15 || green.transform.position.x > xMax + 15)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("Laser"))
            {
                Destroy(gameObject);
            }
    }
}
