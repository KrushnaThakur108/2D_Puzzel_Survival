using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleData", menuName = "Scriptable Objects/PuzzleData")]
public class PuzzleData : ScriptableObject
{
    [Header("Puzzle Info")]
    public int puzzleID;

    public string puzzleName;

    [Header("Puzzle Layout")]
    public int rows = 2;
    public int columns = 2;

    [Header("Sprites")]
    public Sprite fullPuzzleImage;

    public Sprite[] pieceSprites;

    public int TotalPieces
    {
        get
        {
            return pieceSprites.Length;
        }
    }
}
