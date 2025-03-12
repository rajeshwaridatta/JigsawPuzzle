using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "LevelData")]
public class LevelDataHolder : ScriptableObject
{
    
    public List<LevelData> levelData = new List<LevelData>();


}
[System.Serializable]
public class LevelData
{
    public int levelNumber;
    public int numberOfRows;
    public int numberOfCols;
    public Sprite completeSprite;
    public Sprite[] puzzlePiecesSprites;
    public Dictionary<Sprite, int> spriteToIndexMap;
}
[System.Serializable]
public class UserData
{
    public UserLevelData userLevelData;
    public int totalFirstTryCount;
    public UserData(UserLevelData _userLevelData, int _totalFirstTryCount)
    {
        userLevelData = _userLevelData;
        totalFirstTryCount = _totalFirstTryCount;
    }

}
[System.Serializable]
public class UserLevelData
{
    public int currentLevel;
    public bool passedInFirstTry;
   
}