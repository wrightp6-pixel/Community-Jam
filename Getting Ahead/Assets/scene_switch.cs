using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // Call this from the button's OnClick
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Level1");
    }
}