using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameColorDot : MonoBehaviour
{
    public int index;
    public int id;
    public SpriteRenderer sprite;

    private SameColorDotsManager manager;

    public void SetData(SameColorDotsManager manager)
    {
        this.manager = manager;

    }
    public void SetId(int id)
    {
        this.id = id;
    }
	private void Start()
	{
        LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.25f).setEaseInOutCubic();
        LeanTween.scale(gameObject, new Vector3(2, 2, 2), 0.5f).setDelay(0.2f).setEaseInOutCubic();
    }

	public void ChangeColor(Color color)
    {
        sprite.color = color;
    }
    public Color GetColor()
    {
        return sprite.color;
    }
	private void OnMouseDown()
	{
        manager.SelectColor(this);
	}
	public void Select()
    {
        LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.25f).setEaseInOutCubic();
    }
    public void Deselect()
    {
        LeanTween.scale(gameObject, new Vector3(2f, 2f, 2f), 0.25f).setEaseInOutCubic();
    }
    public void Destroying()
    {
       // LeanTween.scale(gameObject, new Vector3(2f, 2f, 2f), 0.25f).setEaseInOutCubic();
        LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.25f).setEaseInOutCubic();
        StartCoroutine(DestroyDelay());
    }
    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(0.25f);
        LeanTween.scale(gameObject, new Vector3(0.05f, 0.05f, 0.05f), 1f).setEaseInOutCubic();
    }
}
