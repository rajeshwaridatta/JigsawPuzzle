
using UnityEngine;
using UnityEngine.UI;

public class LevelPopup : BasePopup
{
    public Button closeButton;
    public Button playButton;

    protected override void Awake()
    {
        base.Awake();
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => PopupManager.Instance.ClosePopup());
        }
    }
    protected override void OnShow(object data)
    {
        //if (data is LevelPopupData popupData)
        //{
        //    levelText.text = "Level " + popupData.LevelNumber;
        //    descriptionText.text = popupData.Description;
        //}
    }
}
