using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class TopImageGrid : MonoBehaviour
{
    [SerializeField] public int rows {  get; private set; }
    [SerializeField] public int cols { get; private set; }

    [SerializeField] private RectTransform topImageRectTransform;
    [SerializeField] private GameObject maskHolder;
    [SerializeField] private Image maskedImage;
    private bool levelWin = false;

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

        StartCoroutine(Check(piece));
       
    }
    private IEnumerator Check(PuzzlePiece piece)
    {
        yield return new WaitForSeconds(0.3f);
        maskHolder.transform.GetChild(piece.index).gameObject.SetActive(false);
        bool gameWin = maskHolder.transform.Cast<Transform>().All(child => !child.gameObject.activeSelf);
        if (gameWin)
        {
            GameWin();
        }
    }
    public void GameWin()
    {
        levelWin = true;
        PuzzleGameManager.Instance.userData.currentLevelNum = PuzzleGameManager.Instance.currentLevel.levelNumber;


        int index = PuzzleGameManager.Instance.userData.currentLevelNum == 0 ? 0 : PuzzleGameManager.Instance.userData.currentLevelNum - 1;

        if (index > 0 && index < PuzzleGameManager.Instance.userData.LevelTryList.Count)
        {
            if (PuzzleGameManager.Instance.userData.LevelTryList[index] >= 1)
            {
                PuzzleGameManager.Instance.userData.LevelTryList[index]++;
            }
        }
        else
        {
            PuzzleGameManager.Instance.userData.LevelTryList.Add(1);
            PuzzleGameManager.Instance.userData.totalFirstTryCount++;
        }

        UserData currentData = PuzzleGameManager.Instance.userData;
        DataManager.Instance.UpdateUserData(currentData);
        PuzzleEvents.InvokePuzzleCompleted();
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
    public RectTransform GetCorrectRect(PuzzlePiece piece)
    {
        return maskHolder.transform.GetChild(piece.index).GetComponent<RectTransform>();
    }
    private void OnApplicationQuit()
    {
        if(!levelWin)
        {
            PuzzleGameManager.Instance.userData.currentLevelNum = PuzzleGameManager.Instance.currentLevel.levelNumber-1;
        int index = PuzzleGameManager.Instance.userData.currentLevelNum == 0 ? 0 : PuzzleGameManager.Instance.userData.currentLevelNum - 1;

            if (index > 0 && index < PuzzleGameManager.Instance.userData.LevelTryList.Count)
            {
                if (PuzzleGameManager.Instance.userData.LevelTryList[index] >= 1)
                {
                    PuzzleGameManager.Instance.userData.LevelTryList[index]++;
                }
            }
            else
            {
                PuzzleGameManager.Instance.userData.LevelTryList.Add(0); 

            }
            UserData currentData = PuzzleGameManager.Instance.userData;
            DataManager.Instance.UpdateUserData(currentData);
        }
       
    }

}
