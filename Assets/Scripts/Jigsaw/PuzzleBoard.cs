using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class PuzzleBoard : MonoBehaviour
{
    public static PuzzleBoard instance;
    [SerializeField] private GameObject puzzlePiecePrefab;
    [SerializeField] private RectTransform bottomPanel;
    [SerializeField] public TopImageGrid topImageGrid;
    public PuzzlePiece[,] puzzlePieces;
    private Sprite[] puzzlePiecesSprites;


    private void OnEnable()
    {
        PuzzleGameManager.OnDataLoaded += CreatePuzzlePieces;
    }
    private void OnDisable()
    {
        PuzzleGameManager.OnDataLoaded -= CreatePuzzlePieces;

    }
    void Start()
    {
        instance = this;
        puzzlePieces = new PuzzlePiece[topImageGrid.rows, topImageGrid.cols];
        puzzlePieces = new PuzzlePiece[topImageGrid.rows, topImageGrid.cols];
    }
    public void SetUpPuzzle(LevelData data)
    {
        puzzlePiecesSprites = data.puzzlePiecesSprites;
    }
    void CreatePuzzlePieces()
    {
        puzzlePieces = new PuzzlePiece[topImageGrid.rows, topImageGrid.cols];
        for (int i = 0; i < topImageGrid.rows; i++)
        {
            for (int j = 0; j < topImageGrid.cols; j++)
            {
                int index = i * topImageGrid.cols + j;

               
                GameObject obj = Instantiate(puzzlePiecePrefab, bottomPanel.transform);
               

                Tuple<float, float> st = topImageGrid.GetPieceSize();
                obj.GetComponent<RectTransform>().sizeDelta = new Vector2(st.Item2, st.Item1);
                PuzzlePiece piece = obj.GetComponent<PuzzlePiece>();
                puzzlePieces[i, j] = piece;
                piece.SetPieceImage(puzzlePiecesSprites,index);

                float pieceWidth = obj.GetComponent<RectTransform>().sizeDelta.x;
                float pieceHeight = obj.GetComponent<RectTransform>().sizeDelta.y;

                float xPos = j * pieceWidth + pieceWidth / 2;
                float yPos = i * pieceHeight + pieceHeight / 2;
                float offsetX = UnityEngine.Random.Range(-pieceWidth * 0.2f, pieceWidth * 0.2f);
                float offsetY = UnityEngine.Random.Range(-pieceHeight * 0.2f, pieceHeight * 0.2f);

                piece.transform.localPosition = new Vector3(xPos + offsetX, yPos + offsetY, 0);
                piece.GetComponent<PuzzlePiece>().SetCorrectPosition(topImageGrid.CalculateSnapPositions(i,j), i,j, index, topImageGrid.GetPieceSize());
               
            }
        }
    }

   

   
    void SetSnapZone(GameObject piece, Vector2 snapPosition, Vector2 pieceSize)
    {
        BoxCollider2D collider = piece.AddComponent<BoxCollider2D>();
        collider.size = pieceSize;
        collider.offset = snapPosition - new Vector2(pieceSize.x / 2, pieceSize.y / 2); 
    }
}
