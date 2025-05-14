using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
	[SerializeField] public bool isDifferent;
	[SerializeField] private SpriteRenderer sprite;
	[Range(0, 255)]
	[SerializeField] private float redValue;
	[Range(0, 255)]
	[SerializeField] private float greenValue;
	[Range(0, 255)]
	[SerializeField] private float blueValue;

	public void SetDifferent(bool val)
	{
		isDifferent = val;
	}
	
	public Vector2 GetPos()
	{
		return transform.position;
	}
	public void ChangeRed(float val)
	{
		redValue = val;
	}
	public void ChangeBlue(float val)
	{
		blueValue = val;
	}
	public void ChangeGreen(float val)
	{
		greenValue = val;
	}
	public void ChangeColor(Color color)
	{
		sprite.color = color;
	}
	public void CheckOverlapping()
	{
		List<Dot> dots =  DotsManager.Instance.GetDots();
		for (int i = 0; i < dots.Count; i++)
		{
			if (dots[i] != this)
			{
				if (Vector2.Distance(transform.position, dots[i].transform.position) < 2.5f)
				{
					Debug.Log("Overlapping");
					DotsManager.Instance.MoveDots(this);
					break;
				}
			}
		}
	}
	private void Start()
	{
		LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.25f).setEaseInOutCubic();
		LeanTween.scale(gameObject, new Vector3(2, 2, 2), 0.5f).setDelay(0.2f).setEaseInOutCubic();
	}
	private void OnMouseDown()
	{
		GameManager.Instance.ClickDot(this);
	}
	public Vector3 GetRGB()
	{
		return new Vector3(redValue, greenValue, blueValue);
	}

	private byte FloatToByteColor(float val)
	{
		return (byte)((val * 255) * 255);
	}
}
