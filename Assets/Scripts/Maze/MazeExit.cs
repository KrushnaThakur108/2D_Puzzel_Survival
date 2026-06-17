using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeExit : MonoBehaviour
{
    [Header("Requirements")]
    [SerializeField] private int piecesRequired = 4;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer exitRenderer;

    [SerializeField] private Color lockedColor = Color.red;
    [SerializeField] private Color unlockedColor = Color.green;

    private bool isUnlocked;

    // Update is called once per frame
    void Update()
    {
        bool shouldUnlock =
            GameData.Instance.HasRequiredPieces(piecesRequired);

        if (shouldUnlock != isUnlocked)
        {
            isUnlocked = shouldUnlock;

            if (exitRenderer != null)
            {
                exitRenderer.color =
                    isUnlocked ? unlockedColor : lockedColor;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUnlocked)
            return;

        if (!other.CompareTag("Player"))
            return;

        SceneManager.LoadScene("PuzzleScene");
    }

}
