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

    }
    private void OnDisable()
    {
        PuzzleEvents.OnPuzzleCompleted -= ShowGameOver;
    }
   
    private void ShowGameOver()
    {

        PopupManager.Instance.OpenPopup(GameObject.FindGameObjectWithTag(Constants.ResultPopupTagName).GetComponent<IPopup>());
    }
    public void ShowLevelPopup()
    {
        PopupManager.Instance.OpenPopup(GameObject.FindGameObjectWithTag(Constants.LevelPopupTagName).GetComponent<IPopup>());
    }
   
}
