using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameManager : MonoBehaviour
{
    public static PuzzleGameManager Instance;
    public PuzzleBoard puzzleBoard;

   

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
  

    public void CheckWinCondition()
    {
        bool allCorrect = puzzleBoard.AreAllPiecesPlacedCorrectly();
        
        if (allCorrect)
        {
            Debug.Log("Puzzle Completed!");
        }
    }
}
