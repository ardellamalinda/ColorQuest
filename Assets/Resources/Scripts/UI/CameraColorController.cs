using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColorController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Color menuColor;
    [SerializeField] private Color colorProtan;
    [SerializeField] private Color colorDeutan;
    [SerializeField] private Color colorTritan;
    [SerializeField] private float duration;

    public void ChangeColor(Level level)
    {
        if (level == Level.Protan)
        {
            LeanTween.value(gameObject, UpdateColor, menuColor, colorProtan, duration)
                   .setEase(LeanTweenType.easeInOutQuad);
        } else if (level == Level.Deutan)
        {
            LeanTween.value(gameObject, UpdateColor, colorProtan, colorDeutan, duration)
                   .setEase(LeanTweenType.easeInOutQuad);
        } else if (level == Level.Tritan)
        {
            LeanTween.value(gameObject, UpdateColor, colorDeutan, colorTritan, duration)
                   .setEase(LeanTweenType.easeInOutQuad);
        }
    }
    public void ChangeColorToMenu()
    {
        LeanTween.value(gameObject, UpdateColor, colorTritan, menuColor, duration)
               .setEase(LeanTweenType.easeInOutQuad);
    }
    public void UpdateColor(Color color)
    {
        cam.backgroundColor = color;
    }
}
