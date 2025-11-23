using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject red;
    public GameObject green;
    public float xPos;
    public float yPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Store the X and Y position of whichever charcters is furthest along or highest up, then move the caemera there
        if (red.transform.position.x > green.transform.position.x)
        {
            xPos = red.transform.position.x;
        }
        else
        {
            xPos = green.transform.position.x;

        }
        
        if (red.transform.position.y > green.transform.position.y)
        {
            yPos = red.transform.position.y;
        }
        else
        {
            yPos = green.transform.position.y;
        }
        
        if (transform.position.x != xPos)
        {
            transform.Translate(Vector3.right * (xPos - transform.position.x));
        }

        if(transform.position.y != yPos)
        {
            transform.Translate(Vector3.up * (yPos - transform.position.y));
        }
    }
}
