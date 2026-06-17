using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Play Game");
        SceneManager.LoadScene("MazeScene");
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game");
    }
}
