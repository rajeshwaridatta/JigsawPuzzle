using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Device;

public class MenuScreen : MonoBehaviour
{
    public Button LevelBtn;

    public TMP_Text firstTryText;
    public TMP_Text LevelNumText;
    private void Start()
    {
        UpdateText("");
        LevelBtn.onClick.AddListener(() => UIManager.Instance. ShowLevelPopup());
    }
    private void OnEnable()
    {
        
        DataManager.OnUserDataLoaded += UpdateText;
      
    }
    private void OnDisable()
    {
       
        DataManager.OnUserDataLoaded -= UpdateText;
       

    }
    private void UpdateText(string sceneName)
    {

        firstTryText.text = DataManager.Instance.userData?.totalFirstTryCount.ToString();
        LevelNumText.text = "Level: " + (DataManager.Instance.userData?.currentLevelNum + 1).ToString();

    }

}
