using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExampleDisplay : UIManager
{
	[SerializeField] private Image image;
	[SerializeField] private Image mascotImage;
	[SerializeField] private Image thumbsImage;
	[SerializeField] private TextMeshProUGUI displayText;
	public void Display(Sprite sprite,string displayString)
	{
		Show();
		LeanTween.scale(image.gameObject, new Vector3(1.5f,1.5f,1.5f), 0.25f).setEaseInOutCubic();
		//LeanTween.scale(mascotImage.gameObject, new Vector3(0.75f, 0.75f, 0.75f), 0.25f).setEaseInOutCubic();
		LeanTween.rotate(thumbsImage.gameObject, new Vector3(0,0,30), 0.25f).setEaseInOutCubic();
		LeanTween.rotate(mascotImage.gameObject, new Vector3(0,0,30), 0.25f).setEaseInOutCubic();
		LeanTween.scale(image.gameObject, new Vector3(1,1,1), 0.5f).setDelay(0.2f).setEaseInOutCubic();
		LeanTween.scale(displayText.gameObject, new Vector3(0.75f, 0.75f, 0.75f), 0.25f).setEaseInOutCubic();
		LeanTween.rotate(thumbsImage.gameObject, new Vector3(0, 0, 0), 0.25f).setDelay(0.2f).setEaseInOutCubic();
		LeanTween.rotate(mascotImage.gameObject, new Vector3(0, 0, 0), 0.25f).setDelay(0.2f).setEaseInOutCubic();
		LeanTween.scale(displayText.gameObject, new Vector3(1,1,1), 0.5f).setDelay(0.2f).setEaseInOutCubic();
		//LeanTween.scale(mascotImage.gameObject, new Vector3(1,1,1), 0.5f).setDelay(0.2f).setEaseInOutCubic();
		image.sprite = sprite;
		displayText.text = displayString;
	}
}
