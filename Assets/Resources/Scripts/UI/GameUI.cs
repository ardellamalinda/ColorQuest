using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : UIManager
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DotsManager dotsManager;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI rankText;
	[SerializeField] private TextMeshProUGUI correctText;
	[SerializeField] private GameObject mainMenuButton;
	[SerializeField] private GameObject showSummaryButton;
	[SerializeField] private Slider progressBar;

	[SerializeField] private float targetProgress;
	private float fillspeed = 1f;

	[SerializeField] private SummaryDisplay summaryDisplay;

	[SerializeField] private SalahDisplay salahDisplay;
	[SerializeField] private ExampleDisplay exampleDisplay;

	private void Awake()
	{
		gameManager.OnShowResult += ShowResult;
		gameManager.OnAnswer += ShowCorrect;
		gameManager.OnNext += Next;
		gameManager.OnGlobalStageAdded += DisplayProgressBar;
	}
	private void DisplayProgressBar(int val)
	{
		float progress = (float)((float)val / 33f);
		targetProgress = progress;
	}
	
	private void Update()
	{
		progressBar.value = Mathf.Lerp(progressBar.value, targetProgress, fillspeed * Time.deltaTime);
	}
	public void OpenSummary()
	{
		summaryDisplay.Show();
	}
	public void CloseSummary()
	{
		summaryDisplay.Hide();
	}
	private void Start()
	{
		ToggleMainMenuButton(false);
		ToggleSummaryButton(false);
	}
	public void ToggleMainMenuButton(bool val)
	{
		mainMenuButton.SetActive(val);
	}
	public void ToggleSummaryButton(bool val)
	{
		showSummaryButton.SetActive(val);
	}
	public void Next()
	{
		ResetCorrect();
		AudioManager.Instance.PlaySfx("Mulai Game");
	}
	public void GoToMainMenu()
	{
		ToggleSummaryButton(false);
		ToggleMainMenuButton(false);
		resultText.text = "";
		rankText.text = "";
		GameManager.Instance.GoToMenu();
	}
	public void ShowCorrect(bool val,int currentStage)
	{
		levelText.text = "";
		stageText.text = "";
		if (val)
		{
			exampleDisplay.Display(dotsManager.GetCurrentLevel().displaySprite[currentStage], dotsManager.GetCurrentLevel().displayString[currentStage]);
		}
		else {
			salahDisplay.Display();
		}
	}
	public void ResetCorrect()
	{
		salahDisplay.Hide();
		exampleDisplay.Hide();
		correctText.text = "";
	}
	public void ShowResult(float result)
	{
		ResetCorrect();
		ToggleMainMenuButton(true);
		ToggleSummaryButton(true);
		levelText.text = "";
		stageText.text = "";
		string rank;
		if (result > 2.8f)
		{
			rank = "A";
		}
		else if (result > 2.4f)
		{
			rank = "B";
		}
		else if (result > 1.8f)
		{
			rank = "C";
		}
		else if (result > 1f)
		{
			rank = "D";
		}
		else {
			rank = "E";
		}
		rankText.text = rank.ToString();
		float finalScore = (result / 3f) * 100f;
		resultText.text = "Kamu Dapat " +rank +" , dengan skor "+ finalScore.ToString("F0");
		//fuzzy logic
		if (result > 2.4f)
		{
			resultText.text += "\n(Tinggi, Mata aman)"; 
		}
		else if (result > 1.5f)
		{
			resultText.text += "\n(Sedang, Zona Abu Abu)";
		}
		else {
			resultText.text += "\n(Rendah, Buta Warna)";
		}
	}
}
