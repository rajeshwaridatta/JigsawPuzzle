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
    public GameObject puzzlePiecePrefab; // The prefab for puzzle pieces
   
    public Image puzzleImage; // The image to divide into pieces (assigned via inspector)
    public Sprite[] puzzlePiecesSprites; // Array of cutout sprites for puzzle pieces
    public Transform bottompanel;
    public TopImageGrid topImageGrid;


    public PuzzlePiece[,] puzzlePieces; // Store references to the puzzle pieces

    void Start()
    {
        instance = this;
        puzzlePieces = new PuzzlePiece[topImageGrid.rows, topImageGrid.cols];



        puzzlePieces = new PuzzlePiece[topImageGrid.rows, topImageGrid.cols];
      
       
       
        CreatePuzzlePieces();
    }

    void CreatePuzzlePieces()
    {
        puzzlePieces = new PuzzlePiece[topImageGrid.rows, topImageGrid.cols];

        // Instantiate puzzle pieces in the lower panel (puzzle board)
        for (int i = 0; i < topImageGrid.rows; i++)
        {
            for (int j = 0; j < topImageGrid.cols; j++)
            {
                int index = i * topImageGrid.cols + j;

                // Instantiate puzzle pieces in the lower panel
                GameObject obj = Instantiate(puzzlePiecePrefab, bottompanel.transform);
                obj.GetComponent<Image>().sprite = puzzlePiecesSprites[index]; // Assign the correct sprite

                // Set random position for the piece in the lower panel (shuffling)
                obj.transform.localPosition = new Vector3(
                   UnityEngine. Random.Range(0f, bottompanel.GetComponent<RectTransform>().rect.width),
                   UnityEngine. Random.Range(0f, bottompanel.GetComponent<RectTransform>().rect.height),
                    0
                );
                Tuple<float, float> st = topImageGrid.GetPieceSize();
                obj.GetComponent<RectTransform>().sizeDelta = new Vector2(st.Item2, st.Item1);
                PuzzlePiece piece = obj.GetComponent<PuzzlePiece>();
                puzzlePieces[i, j] = piece;


                if (i > 0) piece.topNeighbor = puzzlePieces[i - 1, j];
                if (i < topImageGrid.rows - 1) piece.bottomNeighbor = puzzlePieces[i + 1, j];
                if (j > 0) piece.leftNeighbor = puzzlePieces[i, j - 1];
                if (j < topImageGrid.cols - 1) piece.rightNeighbor = puzzlePieces[i, j + 1];



                // Store reference to the piece
                

                // Assign the correct snap position from the top image grid
            
                piece.GetComponent<PuzzlePiece>().SetCorrectPosition(topImageGrid.CalculateSnapPositions(i,j), i,j, index, topImageGrid.GetPieceSize());
               piece.GetComponent<DragHandler>().pieceIndex = index;
            }
        }
    }

    

    // Assign the sprite to the puzzle piece
    void SetPieceImage(GameObject piece, int index)
    {
        Image pieceImage = piece.GetComponent<Image>();
        if (pieceImage != null && puzzlePiecesSprites.Length > index)
        {
            // Assign the sprite for the current puzzle piece (Image component now)
            pieceImage.sprite = puzzlePiecesSprites[index];
        }
    }

    // Set up the snap zone for each puzzle piece using a BoxCollider2D
    void SetSnapZone(GameObject piece, Vector2 snapPosition, Vector2 pieceSize)
    {
        BoxCollider2D collider = piece.AddComponent<BoxCollider2D>();
        collider.size = pieceSize; // Make the collider size match the puzzle piece
        collider.offset = snapPosition - new Vector2(pieceSize.x / 2, pieceSize.y / 2); // Center the collider at the snap position
    }

    // Function to check if all pieces are placed correctly
    public bool AreAllPiecesPlacedCorrectly()
    {
        foreach (PuzzlePiece piece in puzzlePieces)
        {
            // Get the DragHandler component (assuming it stores the correct snap position)
            DragHandler handler = piece.GetComponent<DragHandler>();
            if (!handler.IsCorrectlyPlaced())
            {
                return false; // If any piece is incorrectly placed, return false
            }
        }
        return true; // If all pieces are correctly placed, return true
    }
}
