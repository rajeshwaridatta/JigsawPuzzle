using JetBrains.Annotations;
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
        List<Vector2> gridPositions = new List<Vector2>();

        for (int i = 0; i < topImageGrid.rows; i++)
        {
            for (int j = 0; j < topImageGrid.cols; j++)
            {

                Tuple<float, float> st = topImageGrid.GetPieceSize();
                 new Vector2(st.Item2, st.Item1);

                float pieceWidth = new Vector2(st.Item2, st.Item1).x;
                float pieceHeight = new Vector2(st.Item2, st.Item1).y;
                float offsetX = UnityEngine.Random.Range(-pieceWidth * 0.2f, pieceWidth * 0.2f);
                float offsetY = UnityEngine.Random.Range(-pieceHeight * 0.2f, pieceHeight * 0.2f);
                float xPos = j * pieceWidth + pieceWidth / 2;
                float yPos = i * pieceHeight + pieceHeight / 2;
                gridPositions.Add(new Vector2(xPos+offsetX, yPos+offsetY));
            }
        }
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
                int rand = UnityEngine.Random.Range(0, gridPositions.Count - 1);
                piece.transform.localPosition = gridPositions[rand];
                gridPositions.RemoveAt(rand);
                ShuffleList(gridPositions);
                piece.GetComponent<PuzzlePiece>().SetCorrectPosition(topImageGrid.CalculateSnapPositions(i,j), i,j, index, topImageGrid.GetPieceSize());
               
            }
        }
    }
    void ShuffleList(List<Vector2> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine. Random.Range(0, n + 1);
            Vector2 value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }




    void SetSnapZone(GameObject piece, Vector2 snapPosition, Vector2 pieceSize)
    {
        BoxCollider2D collider = piece.AddComponent<BoxCollider2D>();
        collider.size = pieceSize;
        collider.offset = snapPosition - new Vector2(pieceSize.x / 2, pieceSize.y / 2); 
    }
}
