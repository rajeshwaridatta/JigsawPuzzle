using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapZone : MonoBehaviour
{
    public PuzzlePiece correctPiece;

    public bool IsCorrectPiece(PuzzlePiece piece)
    {
        return piece == correctPiece;
    }
}
