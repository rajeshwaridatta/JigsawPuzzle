using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class UIManager : Singleton<UIManager>
{
    public Button LevelBtn;
    public LevelPopup levelPopup;
    public ResultPopup resultPopup;
    public TMP_Text firstTryText;
    public TMP_Text LevelNumText;
    private void OnEnable()
    {
        PuzzleEvents.OnPuzzleCompleted += ShowGameOver;
        DataManager.OnUserDataLoaded += UpdateText;
    }
    private void OnDisable()
    {
        PuzzleEvents.OnPuzzleCompleted -= ShowGameOver;
        DataManager.OnUserDataLoaded -= UpdateText;

    }
    private void Start()
    {
       // LevelBtn.onClick.AddListener(()=> PopupManager.Instance.OpenPopup(levelPopup));
       
    }
    private void ShowGameOver()
    {
        PopupManager.Instance.OpenPopup(resultPopup);
    }
    public void ShowLevelPopup()
    {
        PopupManager.Instance.OpenPopup(levelPopup);
    }
    private void UpdateText()
    {
        firstTryText.text = DataManager.Instance.userData.totalFirstTryCount.ToString();
        LevelNumText.text = "Level: " + DataManager.Instance.userData.userLevelData.currentLevel.ToString();
    }
}
