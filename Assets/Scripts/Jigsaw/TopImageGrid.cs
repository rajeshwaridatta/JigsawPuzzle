using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopImageGrid : MonoBehaviour
{
    public int rows = 3; // Number of rows for the grid
    public int cols = 3; // Number of columns for the grid
    public RectTransform topImageRectTransform; // The RectTransform of the top image
    public Vector2 pieceSize; // Size of each puzzle piece (calculated automatically)
    public Texture2D jigsawTexture;
    private Vector2 imageSize;

    private Vector2Int dimensions;
    private float width;
    private float height;
    Vector3[,] worldGridPositions;
    public GameObject maskHolder;

    void Start()
    {
        worldGridPositions = new Vector3[rows, cols];
        imageSize = topImageRectTransform.rect.size;
        pieceSize = new Vector2(imageSize.x / cols, imageSize.y / rows);

       
        AddSnapPositions();

    }
    public Vector2 CalculateSnapPositions(int row, int col)
    {
        return (worldGridPositions[row, col]);
    }
    public void AddSnapPositions()
    {
        Vector3 imageLocalPos = topImageRectTransform.localPosition;
        Vector3 worldPos = topImageRectTransform.TransformPoint(imageLocalPos);

        Vector3 topLeft = topImageRectTransform.TransformPoint(new Vector3(-topImageRectTransform.rect.width / 2, topImageRectTransform.rect.height / 2, 0));
        Vector3 correctionOffset = new Vector3(-GetPieceSize().Item2 / 2, GetPieceSize().Item1 / 2, 0);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
              
               // Vector3 localGridPosition = topLeft + new Vector3(j * GetPieceSize().Item2, -i * GetPieceSize().Item1, 0);
               // worldGridPositions[i, j] = topImageRectTransform.TransformPoint(localGridPosition)  ;
               //// worldGridPositions[i, j] -= topImageRectTransform.parent.position;
               // Debug.Log($"Grid[{i}, {j}] Local: {localGridPosition}, World: {worldGridPositions[i,j]}");



                Vector3 localGridPosition1 = new Vector3(j * GetPieceSize().Item2 +330, -i * GetPieceSize().Item1-290, 0);
                worldGridPositions[i, j] = topLeft + localGridPosition1 + correctionOffset;

                //Debug.Log($"Grid[{row}, {col}] World Position: {worldGridPosition}");

            }
        }
        
    }
    public Tuple<float, float> GetPieceSize()
    {
        height = 1f / dimensions.y;
        float aspect = (float)jigsawTexture.width / jigsawTexture.height;
        width = aspect / dimensions.x;


       // return Tuple.Create(height, width);
        return Tuple.Create((float)topImageRectTransform.rect.height/rows, (float)topImageRectTransform.rect.width  / cols) ;
    }
    
}
