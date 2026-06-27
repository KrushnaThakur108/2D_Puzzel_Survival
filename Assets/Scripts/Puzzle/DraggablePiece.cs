using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//public class DraggablePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
//{
//    [HideInInspector]
//    public int pieceID;

//    private Image image;

//    private Transform originalParent;
//    private Canvas canvas;

//    private CanvasGroup canvasGroup;

//    private void Awake()
//    {
//        image = GetComponent<Image>();
//        canvas = GetComponentInParent<Canvas>();

//        canvasGroup = GetComponent<CanvasGroup>();

//        if (canvasGroup == null)
//        {
//            canvasGroup = gameObject.AddComponent<CanvasGroup>();
//        }
//    }

//    public void Initialize(int id, Sprite sprite)
//    {
//        pieceID = id;

//        if (sprite != null)
//            image.sprite = sprite;

//        gameObject.name = $"Piece_{id}";
//    }


//    public void OnBeginDrag(PointerEventData eventData)
//    {
//        Debug.Log("Begin Drag");

//        originalParent = transform.parent;

//        Debug.Log("Original Parent: " + (originalParent ? originalParent.name : "NULL"));
//        Debug.Log("Canvas: " + (canvas ? canvas.name : "NULL"));

//        if (originalParent != null)
//        {
//            PuzzleSlot slot = originalParent.GetComponent<PuzzleSlot>();

//            if (slot != null)
//            {
//                Debug.Log("Removing piece from slot " + slot.slotID);
//                slot.RemovePiece();
//            }
//        }

//        if (canvas != null)
//        {
//            transform.SetParent(canvas.transform);
//        }

//        canvasGroup.blocksRaycasts = false;
//    }


//    public void OnDrag(PointerEventData eventData)
//    {
//        transform.position = eventData.position;
//    }

//    public void OnEndDrag(PointerEventData eventData)
//    {
//        PuzzleSlot targetSlot = null;

//        foreach (GameObject obj in eventData.hovered)
//        {
//            targetSlot = obj.GetComponent<PuzzleSlot>();

//            if (targetSlot != null)
//                break;
//        }

//        if (targetSlot != null)
//        {
//            bool placed = targetSlot.TryPlacePiece(this);

//            if (placed)
//            {
//                canvasGroup.blocksRaycasts = true;
//                return;
//            }

//        }



//        transform.SetParent(originalParent);
//        transform.localPosition = Vector3.zero;
//        canvasGroup.blocksRaycasts = true;
//    }

//}


//Updated code for DraggablePiece to handle placement into slots
public class DraggablePiece :
    MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    [HideInInspector]
    public int pieceID;

    private Image image;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Transform trayParent;

    private Vector2 traySize;

    public PuzzleSlot CurrentSlot { get; private set; }

    private void Awake()
    {
        image = GetComponent<Image>();

        canvas = GetComponentInParent<Canvas>();

        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void Initialize(int id, Sprite sprite)
    {
        pieceID = id;

        image.sprite = sprite;

        trayParent = transform.parent;

        gameObject.name = $"Piece_{id}";

        RectTransform rect = GetComponent<RectTransform>();
        traySize = rect.sizeDelta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CurrentSlot != null)
        {
            CurrentSlot.RemovePiece();
            CurrentSlot = null;
        }

        transform.SetParent(canvas.transform,true);

        canvasGroup.blocksRaycasts = false;

        RectTransform rect = GetComponent<RectTransform>();

        rect.sizeDelta = traySize;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CurrentSlot == null)
        {
            transform.SetParent(trayParent, false);
        }

        canvasGroup.blocksRaycasts = true;
    }

    public void PlaceIntoSlot(PuzzleSlot slot)
    {
        if (!slot.IsEmpty())
            return;

        CurrentSlot = slot;

        slot.SetPiece(this);

        transform.SetParent(slot.transform, false);

        transform.localScale = Vector3.one;

        RectTransform rect = GetComponent<RectTransform>();
        RectTransform slotRect = slot.GetComponent<RectTransform>();

        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);

        rect.sizeDelta = slotRect.rect.size;   // <-- match slot size

        rect.anchoredPosition = Vector2.zero;

        FindFirstObjectByType<PuzzleBoard>()
            ?.CheckPuzzleCompleted();
    }
}