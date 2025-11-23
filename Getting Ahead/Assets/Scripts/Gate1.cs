using UnityEngine;

public class Gate1 : MonoBehaviour
{
    private bool gateUp;
    public float moveSpeed;
    public float yMax;
    public float yMin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gateUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Move gate up or down
        if (gateUp && transform.position.y < yMax)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        else if (gateUp == false &&  transform.position.y > yMin)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }

    public void OpenGate()
    {
        gateUp = true;
    }

    public void CloseGate()
    {
        gateUp = false;
    }
}
