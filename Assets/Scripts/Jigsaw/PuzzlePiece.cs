using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    private Image image;
    private int row, col;
    public int index { get;private set; }
    private Vector2 size;
    private RectTransform correctPlace;
   
    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetCorrectPosition(Vector2 pos, int _row,int _col,int _index, Tuple<float, float> sizeVal)
    {
        
        row = _row;
        col = _col;
        index = _index;
        size = new Vector2(sizeVal.Item2, sizeVal.Item1);
    }

   public void SetPieceImage( Sprite[] puzzlePiecesSprites, int index)
    {
      image.sprite = puzzlePiecesSprites[index];
    }
   
   public bool IsOverlapping()
    {
        Rect r1= GetWorldRect( PuzzleGameManager.Instance.grid.GetCorrectCorrespondingRect(this));
        Rect r2 = GetWorldRect(this.GetComponent<RectTransform>());
        return r1.Overlaps(r2);
    }

    Rect GetWorldRect(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        return new Rect(corners[0].x, corners[0].y,
                        corners[2].x - corners[0].x,
                        corners[2].y - corners[0].y);
    }

}
