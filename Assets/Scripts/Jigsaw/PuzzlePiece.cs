using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField]
    public Vector2 correctSnapPosition;//{ get; private set; }
    private bool placedCorrectly = false;
    private Image image;
    private int row, col;
    public int index { get;private set; }
    private Vector2 size;
   
    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetCorrectPosition(Vector2 pos, int _row,int _col,int _index, Tuple<float, float> sizeVal)
    {
        correctSnapPosition = pos;
        row = _row;
        col = _col;
        index = _index;
        size = new Vector2(sizeVal.Item2, sizeVal.Item1);
    }

   public void SetPieceImage( Sprite[] puzzlePiecesSprites, int index)
    {
      image.sprite = puzzlePiecesSprites[index];
    }


    public bool IsCorrectlyPlaced()
    {
        return placedCorrectly;
    }
    public void SetPlacement()
    {
        placedCorrectly = true;
    }

}
