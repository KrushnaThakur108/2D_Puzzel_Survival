using UnityEngine;
using UnityEngine.UI;

public class PuzzleSlot : MonoBehaviour
{
    [HideInInspector]
    public int slotID;

    private Image image;

    public bool IsFilled { get; private set; }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Initialize(int id)
    {
        slotID = id;
        gameObject.name = $"Slot_{id}";
    }

    public bool TryPlacePiece(DraggablePiece piece)
    {
        Debug.Log($"Trying Piece {piece.pieceID} into Slot {slotID}");

        if (IsFilled)
            return false;

        if (piece.pieceID != slotID)
            return false;

        IsFilled = true;

        piece.transform.SetParent(transform, false);

        RectTransform pieceRect = piece.GetComponent<RectTransform>();

        pieceRect.anchorMin = new Vector2(0.5f, 0.5f);
        pieceRect.anchorMax = new Vector2(0.5f, 0.5f);
        pieceRect.pivot = new Vector2(0.5f, 0.5f);
        pieceRect.sizeDelta = new Vector2(150, 150);


        pieceRect.anchoredPosition = Vector2.zero;

        FindFirstObjectByType<PuzzleBoard>()?.PiecePlaced();


        return true;
    }
}

