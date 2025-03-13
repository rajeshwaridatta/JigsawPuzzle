using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : Singleton<DataManager>
{
    [Header("File storage config")]
    [SerializeField] private string fileName;
    private DataSavingUtil dataSaveHandler;
    [SerializeField] public UserData userData { get; private set; }
    public static event Action OnUserDataLoaded;


    private void OnEnable()
    {
        SceneController.OnSceneLoaded += LoadGame;
       

    }
    private void OnDisable()
    {
        SceneController.OnSceneLoaded -= LoadGame;

    }
    private void Start()
    {
        this.dataSaveHandler = new DataSavingUtil(Application.persistentDataPath, fileName);
       
        LoadGame("");
    }
    private void LoadGame(string sceneName)
    {
        this.userData = dataSaveHandler.Load();
       
        if (this.userData == null)
            NewGame();
        OnUserDataLoaded?.Invoke();
      
    }
    private void SaveGame()
    {
        dataSaveHandler.Save(userData);
    }
    private void NewGame()
    {
        this.userData = new UserData(0, 0);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    public void UpdateUserData(UserData data)
    {
        this.userData = data;
       
        SaveGame();
        OnUserDataLoaded?.Invoke();
    }
}
