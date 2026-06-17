using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggablePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public int pieceID;

    private Image image;

    private Transform originalParent;
    private Canvas canvas;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        image = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();

        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void Initialize(int id, Sprite sprite)
    {
        pieceID = id;

        if (sprite != null)
            image.sprite = sprite;

        gameObject.name = $"Piece_{id}";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;

        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PuzzleSlot targetSlot = null;

        foreach (GameObject obj in eventData.hovered)
        {
            targetSlot = obj.GetComponent<PuzzleSlot>();

            if (targetSlot != null)
                break;
        }

        if (targetSlot != null)
        {
            bool placed = targetSlot.TryPlacePiece(this);

            if (placed)
                return;
        }

        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }
 
}
