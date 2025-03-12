using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : Singleton<DataManager>
{
    [Header("File storage config")]
    [SerializeField] private string fileName;
    private DataSavingUtil dataSaveHandler;
    public UserData userData { get; private set; }
    public static event Action OnUserDataLoaded;
    private void Start()
    {
        this.dataSaveHandler = new DataSavingUtil(Application.persistentDataPath, fileName);
       
        LoadGame();
    }
    private void LoadGame()
    {
        this.userData = dataSaveHandler.Load();
       
        if (this.userData == null)
            NewGame();
        OnUserDataLoaded.Invoke();
    }
    private void SaveGame()
    {
        dataSaveHandler.Save(userData);
    }
    private void NewGame()
    {
        UserLevelData newdata = new UserLevelData();
        newdata.currentLevel = 1;
        newdata.passedInFirstTry = false;
        this.userData = new UserData(newdata, 0);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    public void UpdateUserData(UserLevelData levelData, int firstTryCount)
    {
        this.userData.userLevelData = levelData;
        this.userData.totalFirstTryCount = firstTryCount;
        SaveGame();
    }
}
