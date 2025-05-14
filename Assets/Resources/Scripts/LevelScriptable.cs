using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelScriptable : ScriptableObject
{
    public List<Color> rgbList;
    public List<Color> rgbSaturatedList;
    public List<int> dotCount;
    public List<Sprite> displaySprite;
    public List<string> displayString;
}
