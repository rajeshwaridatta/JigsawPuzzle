using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Device;
using System.Text;

public class MenuScreen : MonoBehaviour
{
    public Button LevelBtn;

    public TMP_Text firstTryText;
    public TMP_Text LevelNumText;
    private StringBuilder sb;
    private void Start()
    {
        UpdateText();
        LevelBtn.onClick.AddListener(() => UIManager.Instance. ShowLevelPopup());
    }
    private void OnEnable()=> DataManager.OnUserDataLoaded += UpdateText;

    private void OnDisable()=> DataManager.OnUserDataLoaded -= UpdateText;
    
    private void UpdateText()
    {
        sb = new StringBuilder(); 
        sb.Clear();
        sb.Append(DataManager.Instance.userData?.totalFirstTryCount);
        firstTryText.text = sb.ToString();
        sb.Clear();
        sb.Append("Level: ").Append(DataManager.Instance.userData.currentLevelNum + 1);
        LevelNumText.text = sb.ToString();

       

    }

}
