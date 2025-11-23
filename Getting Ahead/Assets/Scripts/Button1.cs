using UnityEngine;

public class Button1 : MonoBehaviour
{
    public Gate1 gate;
    private int numPlayers;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            numPlayers += 1;
            gate.OpenGate();
            anim.SetBool("isDown", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // If no one is pressing the button, close the gate
        if (other.gameObject.CompareTag("Player"))
        {
            numPlayers -= 1;
            if (numPlayers == 0)
            {
                gate.CloseGate();
                anim.SetBool("isDown", false);
            }
        }
    }
}
