using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchDotLevel", menuName = "ScriptableObjects/MatchDotLevel", order = 1)]
public class MatchDotLevelScriptable : ScriptableObject

{
	public int colorCount;
	public List<Color> colors = new List<Color>();
	public List<int> id = new List<int>();
}

