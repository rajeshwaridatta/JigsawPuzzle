using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PopupData", menuName = "UI/PopupData")]
public class PopUpData : ScriptableObject
{
    public List<Popup> gamePopupList;
}
[System.Serializable]
public class Popup
{
    public string popupName;
    public GameObject popupPrefab;
}