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


    public PuzzlePiece leftNeighbor;
    public PuzzlePiece rightNeighbor;
    public PuzzlePiece topNeighbor;
    public PuzzlePiece bottomNeighbor;

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

    public void SetImage(Sprite fullImage, Rect pieceRect)
    {
        Texture2D texture = fullImage.texture;
        Sprite pieceSprite = Sprite.Create(texture, pieceRect, new Vector2(0.5f, 0.5f));
        image.sprite = pieceSprite;
    }

 
    public bool IsCorrectlyPlaced()
    {
        bool leftCheck = leftNeighbor == null || Mathf.Abs(transform.localPosition.x - leftNeighbor.transform.localPosition.x) < size.x * 0.2f;
        bool rightCheck = rightNeighbor == null || Mathf.Abs(transform.localPosition.x - rightNeighbor.transform.localPosition.x) < size.x * 0.2f;
        bool topCheck = topNeighbor == null || Mathf.Abs(transform.localPosition.y - topNeighbor.transform.localPosition.y) < size.y * 0.2f;
        bool bottomCheck = bottomNeighbor == null || Mathf.Abs(transform.localPosition.y - bottomNeighbor.transform.localPosition.y) < size.y * 0.2f;

        return leftCheck && rightCheck && topCheck && bottomCheck;
    }
}
