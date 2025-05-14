using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameColorDotsManager : MonoBehaviour
{
	public float score;

	[SerializeField] private bool isPlaying;
	[SerializeField] private SameColorDot sameColorDotPrefab;
	[SerializeField] private List<Transform> places = new List<Transform>();
	[SerializeField] private List<SameColorDot> dotInScene = new List<SameColorDot>();

	[SerializeField] List<SameColorDot> dotTemp = new List<SameColorDot>();
	[SerializeField] Queue<SameColorDot> dotQueue = new Queue<SameColorDot>();
	[SerializeField] private int level;
	[SerializeField] private List<MatchDotLevelScriptable> MatchDotLevels = new List<MatchDotLevelScriptable>();
	[SerializeField] private SalahDisplay salahDisplay;
	[SerializeField] private SameDotsBenarDisplay exampleDisplay;

	[SerializeField] private SameColorDot dotSelected;
	[SerializeField] private bool isColorSelected;
	[SerializeField] private bool isSelectingColor;
	[SerializeField] private int pairLeft;
	//[SerializeField] private 


	public void SelectColor(SameColorDot dot)
	{
		if (isSelectingColor) return;
		
		if (!isColorSelected)
		{
			isColorSelected = true;
			dotSelected = dot;
			dot.Select();
			AudioManager.Instance.PlaySfx("Click");
		}
		else
		{
			if (dotSelected == dot) return;
			AudioManager.Instance.PlaySfx("Click");
			StartCoroutine(CheckColorDelay(dot));
		}
	}
	private IEnumerator CheckColorDelay(SameColorDot dot)
	{
		isSelectingColor = true;
		dot.Select();
		GameManager.Instance.ResetGuide();
		if (dotSelected.id == dot.id)
		{
			exampleDisplay.Display();
			dotSelected.Destroying();
			dot.Destroying();
			AudioManager.Instance.PlaySfx("Jawab Benar");
			GameManager.Instance.AnswerSameColorDots(true);
			GameManager.Instance.SetIsChoosing(false);
		}
		else
		{
			salahDisplay.Display();
			AudioManager.Instance.PlaySfx("Jawab Salah");
			GameManager.Instance.AnswerSameColorDots(false);
			GameManager.Instance.SetIsChoosing(false);
		}
		yield return new WaitForSeconds(1f);
		if (dotSelected.id == dot.id)
		{
			//Correct
			exampleDisplay.Hide();
			Destroy(dotSelected.gameObject);
			Destroy(dot.gameObject);
			isColorSelected = false;
			pairLeft--;
			GameManager.Instance.HideGuide();
			GameManager.Instance.AddGlobalStage();
		
		}
		else
		{
			//Incorrect
			salahDisplay.Hide();
			dotSelected.Deselect();
			dot.Deselect();
			isColorSelected = false;
			dotSelected = null;
	
			if (score > 0)
			{
				score -= 0.1f;
			}
		}
		if (pairLeft == 0)
		{
			if (level < 2)
			{
				AddLevel();
				GameManager.Instance.ChangeLevel();
				GameManager.Instance.NextStage();
			}
			else
			{
				GameManager.Instance.ViewGameResult();
			}

		}
		GameManager.Instance.SetIsChoosing(true);
		isSelectingColor = false;
	}
	public void AddLevel()
	{
		level++;
	}
	public void ResetLevel()
	{
		level = 0;
	}
	public void Play()
	{
		pairLeft = 6;
		dotTemp.Clear();
		dotInScene.Clear();
		for (int i = 0; i < places.Count; i++)
		{
			SameColorDot dot = Instantiate(sameColorDotPrefab, places[i].transform.position, Quaternion.identity);
			dot.SetData(this);
			dot.index = i;
			dotTemp.Add(dot);
			dotInScene.Add(dot);
		}
		for (int i = 0; i < dotTemp.Count;)
		{
			int r = Random.Range(0, dotTemp.Count);
			dotQueue.Enqueue(dotTemp[r]);
			dotTemp.RemoveAt(r);
		}
		Debug.Log("Dot Count : " + dotQueue.Count);
		int index = 0;
		while (dotQueue.Count > 0)
		{
			SameColorDot dot = dotQueue.Dequeue();
			dot.ChangeColor(MatchDotLevels[level].colors[index]);
			dot.SetId(MatchDotLevels[level].id[index]);
			index++;
			Debug.Log("Dequeued Dot Index : " + dot.index);
		}

	}
}
