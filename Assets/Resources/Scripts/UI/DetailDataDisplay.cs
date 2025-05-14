using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DetailDataDisplay : MonoBehaviour
{
    [SerializeField] private Answer data;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private TextMeshProUGUI isCorrectText;
    [SerializeField] private TextMeshProUGUI responseTimeText;
    [SerializeField] private Image bg;
    [SerializeField] private Color differentColorModeColor;
    [SerializeField] private Color sameColorModeColor;

    public void SetData(Answer data)
    {
        this.data = data;
        levelText.text = "Level " +data.globalStage.ToString();
        stageText.text = "Stage " +data.level.ToString();
        if (data.correct)
        {
            isCorrectText.text = "Menjawab Benar";
        }
        else {
            isCorrectText.text = "Menjawab Salah ";
        }
        responseTimeText.text = "Response Time :" + data.responseTime.ToString("F1")+"s";
        if (data.stageLevel == Stage.Differentcolor)
        {
            bg.color = differentColorModeColor;
        }
        else {
            bg.color = sameColorModeColor;
        }
    }

}
