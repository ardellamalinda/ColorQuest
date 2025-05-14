using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class AverageDataDisplay : MonoBehaviour
{
	[SerializeField] private List<Answer> data = new List<Answer>();
	[SerializeField] private List<float> responseTimeList = new List<float>();

	[SerializeField] private TextMeshProUGUI stageText;
	[SerializeField] private TextMeshProUGUI jumlahSalahText;
	[SerializeField] private TextMeshProUGUI responseTimeText;

	public void SetData(List<Answer> data, Level level)
	{
		int jumlahSalah = 0;
		for (int i = 0; i < data.Count; i++)
		{
			if (data[i].level == level)
			{
				this.data.Add(data[i]);
				responseTimeList.Add(data[i].responseTime);
				if (!data[i].correct) 
				{ 
					jumlahSalah++; 
				}
			}
		}
		float average = responseTimeList.Average();
		stageText.text = "Stage : " + level.ToString();
		jumlahSalahText.text = "Jumlah Salah : " + jumlahSalah.ToString();
		responseTimeText.text = "Rata Rata Response Time : " + average.ToString("F1") + "s";
	}
}
