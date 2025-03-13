using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField]
    public Vector2 correctSnapPosition;//{ get; private set; }
    [SerializeField] private Material outlineMat;
    private Image image;
    private int row, col;
    public int index { get;private set; }
    private Vector2 size;
    private RectTransform rect;
   
    void Awake()
    {
        image = GetComponent<Image>();
        this.GetComponent<Image>().material = null;
        rect = GetComponent<RectTransform>();
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

    public void SetVFXMat()
    {
        image.material = outlineMat;
    }
    public void PlayScaleAnim()
    {
        Vector2 originalscale = transform.localScale;
        Vector2 scaleTo = originalscale * 1.25f;
        transform.DOScale(scaleTo, 0.05f).SetEase(Ease.InOutSine).OnComplete(() => {

           
        });
    }
   
   public bool IsOverlapping()
    {
        Rect r1= GetWorldRect( PuzzleGameManager.Instance.grid.GetCorrectRect(this));
        Rect r2 = GetWorldRect(rect);
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
