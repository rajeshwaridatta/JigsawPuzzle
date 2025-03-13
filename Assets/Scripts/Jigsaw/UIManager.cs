using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class UIManager : Singleton<UIManager>
{
    
   
    private void OnEnable()
    {
        PuzzleEvents.OnPuzzleCompleted += ShowGameOver;
        DataManager.OnUserDataLoaded += UpdateText;
        SceneController.OnSceneLoaded += UpdateText;
    }
    private void OnDisable()
    {
        PuzzleEvents.OnPuzzleCompleted -= ShowGameOver;
        DataManager.OnUserDataLoaded -= UpdateText;
        SceneController.OnSceneLoaded += UpdateText;

    }
    private void Start()
    {
       
        UpdateText("MenuScene");


    }
    private void ShowGameOver()
    {
        GameObject g = GameObject.FindGameObjectWithTag("resultpopup");
      
        PopupManager.Instance.OpenPopup(GameObject.FindGameObjectWithTag("resultpopup").GetComponent<IPopup>());
    }
    public void ShowLevelPopup()
    {
        PopupManager.Instance.OpenPopup(GameObject.FindGameObjectWithTag("levelpopup").GetComponent<IPopup>());
    }
    private void UpdateText(string sceneName)
    {
       
        
      
    }
}
