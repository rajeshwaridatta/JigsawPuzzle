using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TopImageGrid : MonoBehaviour
{
    [SerializeField] public int rows {  get; private set; }
    [SerializeField] public int cols { get; private set; }

    [SerializeField] private RectTransform topImageRectTransform;
    [SerializeField] private GameObject maskHolder;
    [SerializeField] private Image maskedImage;

    private Vector3[,] worldGridPositions;
   
    private void OnEnable()
    {
        DragHandler.OnPiecePlacedCorrectly += CheckGridState;
    }
    private void OnDisable()
    {
        DragHandler.OnPiecePlacedCorrectly -= CheckGridState;

    }

    private void CheckGridState(PuzzlePiece piece)
    {
        maskHolder.transform.GetChild(piece.index).gameObject.SetActive(false);
        bool gameWin = maskHolder.transform.Cast<Transform>().All(child => !child.gameObject.activeSelf);
        if (gameWin)
        {
            PuzzleEvents.InvokePuzzleCompleted();
            PuzzleGameManager.Instance.userData.userLevelData.currentLevel = PuzzleGameManager.Instance.currentLevel.levelNumber;
            
            if (!PuzzleGameManager.Instance.userData.userLevelData.passedInFirstTry)
            {
                PuzzleGameManager.Instance.userData.userLevelData.passedInFirstTry = true;
                PuzzleGameManager.Instance.userData.totalFirstTryCount++;
            }
            DataManager.Instance.UpdateUserData(PuzzleGameManager.Instance.userData.userLevelData, PuzzleGameManager.Instance.userData.totalFirstTryCount);
        }
            
       
    }

    public Vector2 CalculateSnapPositions(int row, int col)
    {
        return (worldGridPositions[row, col]);
    }
    public void AddSnapPositions()
    {
        worldGridPositions = new Vector3[rows, cols];
        Vector3 imageLocalPos = topImageRectTransform.localPosition;
        Vector3 worldPos = topImageRectTransform.TransformPoint(imageLocalPos);

        Vector3 topLeft = topImageRectTransform.TransformPoint(new Vector3(-topImageRectTransform.rect.width / 2, topImageRectTransform.rect.height / 2, 0));
        Vector3 correctionOffset = new Vector3(-GetPieceSize().Item2 / 2, GetPieceSize().Item1 / 2, 0);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Vector3 localGridPosition1 = new Vector3(j * GetPieceSize().Item2 +330, -i * GetPieceSize().Item1-290, 0);
                worldGridPositions[i, j] = topLeft + localGridPosition1 + correctionOffset;
            }
        }
    }
    public Tuple<float, float> GetPieceSize()
    {
        return Tuple.Create((float)topImageRectTransform.rect.height/rows, (float)topImageRectTransform.rect.width  / cols) ;
    }
    public void SetLevelImageAssets(LevelData currentLevel)
    {
        Sprite s = currentLevel.completeSprite;
        topImageRectTransform.GetComponent<Image>().sprite =s ;
        int i = 0;
        foreach(Transform child in maskHolder.transform)
        {
            child.GetComponent<Image>().sprite = currentLevel.puzzlePiecesSprites[i];
            i++;
        }
        maskedImage.sprite = s;
        rows = currentLevel.numberOfRows;
        cols = currentLevel.numberOfCols;
        AddSnapPositions();


    }

}
