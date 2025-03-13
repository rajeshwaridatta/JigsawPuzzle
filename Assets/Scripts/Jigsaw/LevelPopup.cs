
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.SocialPlatforms.Impl;

public class LevelPopup : BasePopup
{
    public Button closeButton;
    public Button playButton;
    public TMP_Text LevelNumText;

    protected override void Awake()
    {
        base.Awake();
        popUpName = Constants.LevelPopupTagName;
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => PopupManager.Instance.ClosePopup());
        }
        if (playButton != null)
        {
            playButton.onClick.AddListener(() => SceneController.Instance.LoadNextScene(Constants.GameScene));
        }
        
    }
    protected override void OnShow(object data)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Level: ").Append(DataManager.Instance.userData.currentLevelNum + 1);
        LevelNumText.text = sb.ToString();
    }
}
