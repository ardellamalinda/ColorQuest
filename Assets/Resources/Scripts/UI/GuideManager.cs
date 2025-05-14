using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuideManager : UIManager
{
	[SerializeField] private Image imageDisplay;
	[SerializeField] private TextMeshProUGUI textDisplay;
	[SerializeField] private TextMeshProUGUI indexDisplay;

	[TextArea]
	[SerializeField] private string[] textList;
	[SerializeField] private Sprite[] imageList;

	[SerializeField] private int currentIndex;
	public override void Show()
	{
		base.Show();
		currentIndex = 0;
		DisplayGuide();
	}
	public void Next()
	{
		if (currentIndex < imageList.Length - 1)
		{
			currentIndex++;
			AudioManager.Instance.PlaySfx("Click");
			DisplayGuide();
		}
	}
	public void Prev()
	{
		if (currentIndex > 0)
		{
			currentIndex--;
			DisplayGuide();
			AudioManager.Instance.PlaySfx("Click");
		}
	}
	private void DisplayGuide()
	{
		imageDisplay.sprite = imageList[currentIndex];
		textDisplay.text = textList[currentIndex];
		indexDisplay.text = "(" + (currentIndex + 1) + " / " + imageList.Length + " )";
	}
	public void Close()
	{
		AudioManager.Instance.PlaySfx("Click");
		Hide();
	}
}
