using System.Collections.Generic;
using UnityEngine;

public enum Level
{
	Protan, Deutan, Tritan
}
public enum Stage
{ 
	Differentcolor,SameColor
}

public class DotsManager : MonoBehaviour
{
	public static DotsManager Instance;
	[SerializeField] private int dotsCount;
	[SerializeField] private Dot dotsPrefab;
	[SerializeField] private float saturationLevel = 50f; 
	[SerializeField] private Transform parent; // nge spawn dotnya di child ini
	[SerializeField] private List<Transform> cornerPosition; //buat nge fixing pas di android
	[SerializeField] private List<Dot> dotsSpawned; 
	[SerializeField] private LevelScriptable currentLevelPlay;
	[SerializeField] private List<LevelScriptable> levelList;
	[SerializeField] Color selectedColor;
	[SerializeField] Color saturatedColor;


	private void Awake()
	{
		Instance = this;
	}
	public LevelScriptable GetCurrentLevel()
	{
		return currentLevelPlay;
	}
	public void StartDots()
	{
		ChangeMainColor();
		SpawnDots();
		ChangeDotsColor();
		saturatedColor = currentLevelPlay.rgbSaturatedList[0];
		Change1Saturation();
	}
	public void ClearWrongColor()
	{
		for (int i = 0; i < dotsSpawned.Count; i++)
		{
			if (!dotsSpawned[i].isDifferent)
			{
				dotsSpawned[i].gameObject.SetActive(false);
			}
			
		}
	}
	public void SetStage(int index)
	{
		selectedColor = currentLevelPlay.rgbList[index];
		saturatedColor = currentLevelPlay.rgbSaturatedList[index];
		dotsCount = currentLevelPlay.dotCount[index];
	}
	public void StartPlay()
	{
		currentLevelPlay = levelList[0];
	}
	public void SetLevel(Level level)
	{
		if (level == Level.Protan)
		{
			currentLevelPlay = levelList[0];
		}
		else if (level == Level.Deutan)
		{
			currentLevelPlay = levelList[1];
		}
		else if (level == Level.Tritan)
		{
			currentLevelPlay = levelList[2];
		}
	}
	public void DecreaseSaturationLevel()
	{
		saturationLevel *= 0.8f;
	}
	public Color GetRGB()
	{
		return selectedColor;
	}
	public void IncreaseSaturationLevel()
	{
		saturationLevel *= 1.2f;
	}
	public void ChangeColor(Color val)
	{
		selectedColor = val;
	}
	private void Update()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			ClearDots();
			SpawnDots();
			Change1Saturation();
		}
	}
	public void NextStage()
	{
		ClearDots();
		SpawnDots();
		Change1Saturation();
	}
	public void Change1Saturation()
	{
		for (int i = 0; i < dotsSpawned.Count; i++)
		{
			dotsSpawned[i].SetDifferent(false);
		}
		int rIndex = Random.Range(0, dotsSpawned.Count);

		dotsSpawned[rIndex].ChangeColor(saturatedColor);
		dotsSpawned[rIndex].SetDifferent(true);
		Debug.Log("CurStage : 1");

	}
	public void ChangeMainColor()
	{
		if (GameManager.Instance.currentLevel == Level.Protan)
		{
			selectedColor = currentLevelPlay.rgbList[0];
		}
		else if (GameManager.Instance.currentLevel == Level.Deutan)
		{
			selectedColor = currentLevelPlay.rgbList[0];

			//selectedColor = Color.yellow;

		}
		else if (GameManager.Instance.currentLevel == Level.Tritan)
		{
			selectedColor = currentLevelPlay.rgbList[0];

			//selectedColor = Color.blue;

		}
	}
	public void ChangeDotsColor()
	{
		for (int i = 0; i < dotsSpawned.Count; i++)
		{
			dotsSpawned[i].ChangeColor(selectedColor);
		}
	}
	public List<Dot> GetDots()
	{
		return dotsSpawned;
	}
	public void ClearDots()
	{
		foreach (Dot d in dotsSpawned)
		{
			Destroy(d.gameObject);
		}
		dotsSpawned.Clear();
	}

	private void SpawnDots()
	{
		for (int i = 0; i < dotsCount; i++)
		{
			Dot dot = Instantiate(dotsPrefab, parent);
			dot.transform.position = GetNewPosition();
			dotsSpawned.Add(dot);
			dot.ChangeColor(selectedColor);
			dot.CheckOverlapping();
		}
	}
	public void MoveDots(Dot dot)
	{
		dot.transform.position = GetNewPosition();
		dot.CheckOverlapping();
	}
	Vector2 GetNewPosition()
	{
		Vector2 newpos = new Vector2(Random.Range(cornerPosition[0].position.x, cornerPosition[1].position.x), Random.Range(cornerPosition[2].position.y, cornerPosition[0].position.y));
		return newpos;
	}
}
