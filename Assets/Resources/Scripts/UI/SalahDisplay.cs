using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class SalahDisplay : UIManager
{
	[SerializeField] private TextMeshProUGUI salahText;
	[SerializeField] private Image mascotImage;
	[SerializeField] private Image thumbsImage;
	[SerializeField] private List<string> variasiText = new List<string>();
	public void Display()
	{
		Show();
		salahText.text = "Kamu Salah";
		LeanTween.scale(salahText.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.25f).setEaseInOutCubic();
		LeanTween.scale(salahText.gameObject, new Vector3(1, 1, 1), 0.5f).setDelay(0.2f).setEaseInOutCubic();
		LeanTween.rotate(thumbsImage.gameObject, new Vector3(0, 0, -30), 0.25f).setEaseInOutCubic();
		LeanTween.rotate(thumbsImage.gameObject, new Vector3(0, 0, 0), 0.25f).setDelay(0.2f).setEaseInOutCubic();
		StartCoroutine(SalahDelay());
	}
	private IEnumerator SalahDelay()
	{
		yield return new WaitForSeconds(1f);
		LeanTween.scale(salahText.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.25f).setEaseInOutCubic();
		LeanTween.scale(salahText.gameObject, new Vector3(1, 1, 1), 0.5f).setDelay(0.2f).setEaseInOutCubic();
		LeanTween.rotate(thumbsImage.gameObject, new Vector3(0, 0, -30), 0.25f).setEaseInOutCubic();
		LeanTween.rotate(thumbsImage.gameObject, new Vector3(0, 0, 0), 0.25f).setDelay(0.2f).setEaseInOutCubic();
		salahText.text = variasiText[Random.Range(0, variasiText.Count)];
	}
}
