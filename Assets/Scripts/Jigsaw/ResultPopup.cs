
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
public class ResultPopup : BasePopup
{

    public Button closeButton;
    public Button playButton;
    public TMP_Text levelNumber; 

    protected override void Awake()
    {
        base.Awake();
        popUpName = Constants.ResultPopupTagName;
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => PopupManager.Instance.ClosePopup());
        }
        if (playButton != null)
            playButton.onClick.AddListener(() => SceneController.Instance.LoadNextScene(Constants.MainMenuScene));
    }
    protected override void OnShow(object data)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Level: ").Append(DataManager.Instance.userData.currentLevelNum);
        levelNumber.text = sb.ToString();
    }
}
