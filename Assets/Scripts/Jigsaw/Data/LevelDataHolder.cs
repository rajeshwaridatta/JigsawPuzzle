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
    public int currentLevelNum;
    public int totalFirstTryCount;
    public List<int> LevelTryList;
    
    public UserData(int _currentLevelNum, int _totalFirstTryCount)
    {
        currentLevelNum = _currentLevelNum;
        totalFirstTryCount = _totalFirstTryCount;
        LevelTryList = new List<int>();
       
    }

}
