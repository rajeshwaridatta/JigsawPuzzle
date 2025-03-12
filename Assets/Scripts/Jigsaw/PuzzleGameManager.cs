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
        LoadLevelData(userData.userLevelData.currentLevel);

    }
    private void Start()
    {
        
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
