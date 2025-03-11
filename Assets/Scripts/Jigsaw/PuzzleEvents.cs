using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PuzzleEvents 
{
    public static event Action OnPuzzleCompleted;

    public static void InvokePuzzleCompleted()
    {
        OnPuzzleCompleted?.Invoke();
    }
}
