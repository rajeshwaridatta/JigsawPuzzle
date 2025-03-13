
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPopup : BasePopup
{
    public Button closeButton;
    public Button playButton;
    public TMP_Text LevelNumText;

    protected override void Awake()
    {
        base.Awake();
        popUpName = "levelpopup";
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => PopupManager.Instance.ClosePopup());
        }
        if (playButton != null)
        {
            playButton.onClick.AddListener(() => SceneController.Instance.LoadNextScene("GamePlayScene"));
        }
        
    }
    protected override void OnShow(object data)
    {
       
        LevelNumText.text =  LevelNumText.text = "Level: " + (DataManager.Instance.userData.currentLevelNum +1).ToString();
    }
}
