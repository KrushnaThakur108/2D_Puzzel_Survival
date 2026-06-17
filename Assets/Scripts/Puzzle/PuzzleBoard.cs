using UnityEngine;
using UnityEngine.UI;

public class PuzzleBoard : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform boardParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform trayParent;
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private GameObject completePanel;
    private int placedPieces = 0;
    private int totalPieces = 0;

    private void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {

        // Safety checks
        if (GameData.Instance == null)
        {
            Debug.LogError("GameData Instance is NULL!");
            return;
        }

        if (GameData.Instance.currentPuzzle == null)
        {
            Debug.LogError("Current Puzzle is NULL!");
            return;
        }

        PuzzleData puzzle = GameData.Instance.currentPuzzle;

        Debug.Log($"Generating Puzzle: {puzzle.puzzleName}");

        GridLayoutGroup grid = boardParent.GetComponent<GridLayoutGroup>();

        if (grid == null)
        {
            Debug.LogError("GridLayoutGroup missing on Board Parent!");
            return;
        }

        // Configure grid
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = puzzle.columns;

        // Remove old slots if any
        foreach (Transform child in boardParent)
        {
            Destroy(child.gameObject);
        }

        int totalSlots = puzzle.rows * puzzle.columns;
        totalPieces = totalSlots;

        Debug.Log($"Total Slots: {totalSlots}");

        for (int i = 0; i < totalSlots; i++)
        {
            GameObject slotObject =
                Instantiate(slotPrefab, boardParent);

            PuzzleSlot slot =
                slotObject.GetComponent<PuzzleSlot>();

            if (slot != null)
            {
                slot.Initialize(i);
            }
        }

        SpawnTrayPieces(puzzle);

        Debug.Log("Puzzle Board Generated Successfully");
    }

    private void SpawnTrayPieces(PuzzleData puzzle)
    {
        for (int i = 0; i < puzzle.pieceSprites.Length; i++)
        {
            //if (!GameData.Instance.HasPiece(i))
            //    continue;
            Debug.Log("SpawnTrayPieces");

            GameObject pieceObject =
                Instantiate(piecePrefab, trayParent);

            DraggablePiece piece =
                pieceObject.GetComponent<DraggablePiece>();

            if (piece != null)
            {
                piece.Initialize(
                    i,
                    puzzle.pieceSprites[i]
                );
            }
        }
    }

    public void PiecePlaced()
    {
        placedPieces++;

        Debug.Log($"Placed {placedPieces}/{totalPieces}");

        if (placedPieces >= totalPieces)
        {
            PuzzleCompleted();
        }
    }

    private void PuzzleCompleted()
    {
        Debug.Log("PUZZLE COMPLETED!");

        if (completePanel != null)
        {
            completePanel.SetActive(true);
        }
    }



}
