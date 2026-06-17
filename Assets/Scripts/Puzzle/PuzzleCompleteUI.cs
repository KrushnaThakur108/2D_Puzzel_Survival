using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleCompleteUI : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadScene("MazeScene");
    }
}
