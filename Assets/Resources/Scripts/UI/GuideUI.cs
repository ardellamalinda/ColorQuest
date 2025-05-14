using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideUI : UIManager
{
    [SerializeField] private TextMeshProUGUI guideText;


	
	public void Show(string theText)
	{
		base.Show();
		guideText.text = theText;
		guideText.transform.localScale = new Vector3(1, 1, 1);
		LeanTween.scale(guideText.gameObject, new Vector3(1.25f, 1.25f, 1.25f), 1).setLoopPingPong();
	}
	public override void Hide()
	{
		base.Hide();
		LeanTween.cancel(guideText.gameObject);
	}
}
