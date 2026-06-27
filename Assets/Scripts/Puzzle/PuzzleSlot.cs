using UnityEngine;
using UnityEngine.EventSystems;

//using UnityEngine.UI;

//public class PuzzleSlot : MonoBehaviour
//{
//    [HideInInspector]
//    public int slotID;

//    private Image image;
//    private DraggablePiece currentPiece;

//    //public bool IsFilled { get; private set; }

//    private void Awake()
//    {
//        image = GetComponent<Image>();
//    }

//    public void Initialize(int id)
//    {
//        slotID = id;
//        gameObject.name = $"Slot_{id}";
//    }

//    public bool TryPlacePiece(DraggablePiece piece)
//    {
//        Debug.Log($"Trying Piece {piece.pieceID} into Slot {slotID}");



//        if (currentPiece != null)
//        {
//            Debug.Log("Slot already occupied");
//            return false;
//        }

//        currentPiece = piece;
//        //piece.transform.SetParent(transform);
//        piece.transform.localPosition = Vector3.zero;

//        //IsFilled = true;

//        piece.transform.SetParent(transform, false);

//        RectTransform pieceRect = piece.GetComponent<RectTransform>();

//        pieceRect.anchorMin = new Vector2(0.5f, 0.5f);
//        pieceRect.anchorMax = new Vector2(0.5f, 0.5f);
//        pieceRect.pivot = new Vector2(0.5f, 0.5f);
//        pieceRect.sizeDelta = new Vector2(150, 150);


//        pieceRect.anchoredPosition = Vector2.zero;

//        //FindFirstObjectByType<PuzzleBoard>()?.PiecePlaced();

//        // check for the completion of puzzle with correct placements
//        FindFirstObjectByType<PuzzleBoard>()?.CheckPuzzleCompleted();


//        return true;
//    }

//    public void RemovePiece()
//    {

//        Debug.Log($"Removing {currentPiece?.name} from Slot {slotID}");

//        currentPiece = null;
//    }

//    public bool IsCorrect()
//    {
//        if (currentPiece == null)
//            return false;

//        return currentPiece.pieceID == slotID;
//    }

//}


// Updated code for PuzzleSlot to implement IDropHandler interface

public class PuzzleSlot : MonoBehaviour, IDropHandler
{
    [HideInInspector]
    public int slotID;

    public DraggablePiece CurrentPiece { get; private set; }

    public void Initialize(int id)
    {
        slotID = id;
        gameObject.name = $"Slot_{id}";
    }

    public bool IsEmpty()
    {
        return CurrentPiece == null;
    }

    public bool IsCorrect()
    {
        return CurrentPiece != null &&
               CurrentPiece.pieceID == slotID;
    }

    public void SetPiece(DraggablePiece piece)
    {
        CurrentPiece = piece;
    }

    public void RemovePiece()
    {
        CurrentPiece = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggablePiece piece =
            eventData.pointerDrag.GetComponent<DraggablePiece>();

        if (piece == null)
            return;

        piece.PlaceIntoSlot(this);
    }
}
