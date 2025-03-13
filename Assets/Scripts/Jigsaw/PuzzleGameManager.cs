using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuzzleGameManager : MonoBehaviour
{
    public static PuzzleGameManager Instance;
    public LevelDataHolder levelDataHolder;
    public LevelData currentLevel { get; private set; }
    public PuzzleBoard board;
    public TopImageGrid grid;
    public UserData userData;


    public static event Action OnDataLoaded;

    private void Awake()
    {
        Instance = this;
        userData = DataManager.Instance.userData;
        int levelToPlay = levelDataHolder.levelData.Count > userData.currentLevelNum + 1 ? userData.currentLevelNum + 1 : userData.currentLevelNum;
        Debug.Log("Level to play "+ levelToPlay);
        LoadLevelData(levelToPlay);

    }

    private void LoadLevelData(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex > levelDataHolder.levelData.Count) return;
        currentLevel = levelDataHolder.levelData[levelIndex-1];
        grid.SetLevelImageAssets(currentLevel);
        board.SetUpPuzzle(currentLevel);
        OnDataLoaded.Invoke();


    }



}
