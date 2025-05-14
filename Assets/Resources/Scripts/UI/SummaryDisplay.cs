using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummaryDisplay : UIManager
{

	[SerializeField] private Transform parent;
	[SerializeField] private DetailDataDisplay detailDataDisplayPrefab;
	[SerializeField] private AverageDataDisplay averageDataDisplayPrefab;
	[SerializeField] private TextMeshProUGUI showText;

	[SerializeField] private bool isShowingAverage;

	public override void Show()
	{
		base.Show();
		isShowingAverage = true;
		ShowAverage();
	}
	public void ToggleShow()
	{
		if (isShowingAverage)
		{
			isShowingAverage = false;
			ShowDetail();
			showText.text = "Tampilkan Rata-Rata";
		}
		else {
			isShowingAverage = true;
			ShowAverage();
			showText.text = "Tampilkan Detail";
		}
	}
	private void ShowAverage()
	{
		foreach (Transform t in parent)
		{
			Destroy(t.gameObject);
		}
		List<Answer> answers = GameManager.Instance.GetAnswers();
		AverageDataDisplay Protan = Instantiate(averageDataDisplayPrefab, parent);
		Protan.SetData(answers, Level.Protan);
		AverageDataDisplay Deutan = Instantiate(averageDataDisplayPrefab, parent);
		Deutan.SetData(answers, Level.Deutan);
		AverageDataDisplay Tritan = Instantiate(averageDataDisplayPrefab, parent);
		Tritan.SetData(answers, Level.Tritan);
	}
	private void ShowDetail()
	{
		foreach (Transform t in parent)
		{
			Destroy(t.gameObject);
		}
		List<Answer> answers = GameManager.Instance.GetAnswers();
		for (int i = 0; i < answers.Count; i++)
		{
			DetailDataDisplay p = Instantiate(detailDataDisplayPrefab, parent);
			p.SetData(answers[i]);
		}
	}

}
