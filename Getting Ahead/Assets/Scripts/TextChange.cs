using UnityEngine;

public class TextChange : MonoBehaviour
{
    public void Start()
    {
        TextOff();
    }
    public void TextOn()
   {
        gameObject.SetActive(true);
   }

    public void TextOff()
    {
        gameObject.SetActive(false);
    }
}
