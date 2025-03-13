
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResultPopup : BasePopup
{

    public Button closeButton;
    public Button playButton;
    public TMP_Text levelNumber; 

    protected override void Awake()
    {
        base.Awake();
        popUpName = "resultpopup";
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => PopupManager.Instance.ClosePopup());
        }
        if (playButton != null)
            playButton.onClick.AddListener(() => SceneController.Instance.LoadNextScene("MenuScene"));
    }
    protected override void OnShow(object data)
    {
        levelNumber.text =  "Level: " + (DataManager.Instance.userData.currentLevelNum).ToString();
    }
}
