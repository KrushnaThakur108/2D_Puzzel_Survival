using TMPro;
using UnityEngine;

public class MazeHUD : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text piecesText;

    [Header("Puzzle")]
    [SerializeField] private int totalPiecesRequired = 4;

    // Update is called once per frame
    private void Update()
    {
        piecesText.text = $"Pieces: {GameData.Instance.GetCollectedCount()}/{totalPiecesRequired}";
    }
}
