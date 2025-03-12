using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    private readonly Stack<IPopup> popupStack = new Stack<IPopup>();

    public void OpenPopup(IPopup popup)
    {
        if (popup != null)
        {
            popupStack.Push(popup);
            popup.Show();
        }
    }

    public void ClosePopup()
    {
        if (popupStack.Count > 0)
        {
            var popup = popupStack.Pop();
            popup.Hide();
        }
    }

    private void Update()
    {
        if (popupStack.Count > 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePopup(); 
        }
    }
}
