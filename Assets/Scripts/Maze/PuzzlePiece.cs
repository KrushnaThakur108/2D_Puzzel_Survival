using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [Header("Piece Data")]
    public int pieceID;

    private Vector3 startPos;

    [SerializeField]
    private float floatAmount = 0.1f;

    [SerializeField]
    private float floatSpeed = 2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        startPos = transform.position;

        // If already collected, hide it
        if (GameData.Instance.HasPiece(pieceID))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
    transform.position =
     startPos +
     Vector3.up *
     Mathf.Sin(Time.time * floatSpeed) *
     floatAmount;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
      
        GameData.Instance.CollectPiece(pieceID);

        Debug.Log($"Collected Piece {pieceID}");

        Destroy(gameObject);
    
    }
}
