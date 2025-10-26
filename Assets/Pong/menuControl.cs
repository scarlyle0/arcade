using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayPong()
    {
        SceneManager.LoadScene("Pong");
        Debug.Log("Pong Started");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
