using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    [Header("Progress")]
    public int currentLevel = 1;
    public int currentPuzzleID = 0;


    [Header("Current Puzzle")]
    public PuzzleData currentPuzzle;

    private HashSet<int> collectedPieces = new HashSet<int>();

    private void Start()
    {
        Debug.Log(currentPuzzle.puzzleName);
    }

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("GameData Persistent");
    }

    // Collect a puzzle piece
    public void CollectPiece(int pieceID)
    {
        collectedPieces.Add(pieceID);
    }

    // Check if piece exists
    public bool HasPiece(int pieceID)
    {
        return collectedPieces.Contains(pieceID);
    }

    // Get all collected pieces
    public HashSet<int> GetCollectedPieces()
    {
        return collectedPieces;
    }

    // Reset current puzzle progress
    public void ClearCollectedPieces()
    {
        collectedPieces.Clear();
    }

    //Your GameData now exposes the total collected count.
    public int GetCollectedCount()
    {
        return collectedPieces.Count;
    }

    public bool HasRequiredPieces(int requiredCount)
    {
        return collectedPieces.Count >= requiredCount;
    }

}
