using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Answer
{
	public bool correct;
	public Level level;
	public Stage stageLevel;
	public int stage;
	public int globalStage;
	public float responseTime;

	public Answer(bool correct, Level level, Stage stageLevel,int stage, int globalStage,float responseTime)
	{
		this.correct = correct;
		this.level = level;
		this.stageLevel = stageLevel;
		this.stage = stage;
		this.globalStage = globalStage;
		this.responseTime = responseTime;
	}
}
public class GameManager : MonoBehaviour
{
	public event Action OnDotClicked;
	public event Action<bool, int> OnAnswer;
	public event Action OnNext;
	public event Action OnLevelCleared;
	public event Action<int> OnGlobalStageAdded;
	public event Action<float> OnShowResult;


	public static GameManager Instance;
	public Level currentLevel;
	public Stage currentStageLevel;
	public int currentStage;
	public int currentGlobalStage;
	public int currentDifficulty;
	[SerializeField] private DotsManager dotsManager;
	[SerializeField] private SameColorDotsManager sameColorDotsManager;
	[SerializeField] private GuideManager guideManager;
	[SerializeField] private MainMenuUI mainMenuUI;
	[SerializeField] private GameUI gameUI;
	[SerializeField] private GuideUI guideUI;
	[SerializeField] private CameraColorController camColorController;
	[SerializeField] private ExampleDisplay exampleDisplay;
	[SerializeField] private SalahDisplay salahDisplay;
	[SerializeField] private float protanPoint;
	[SerializeField] private float deutanPoint;
	[SerializeField] private float tritanPoint;
	[SerializeField] private bool isAnswering;
	private Stack<Color> rgbStored = new Stack<Color>();

	[SerializeField] private List<Answer> answers = new List<Answer>();

	[SerializeField] private float showGuideMaxCooldown;
	[SerializeField] private float showGuidecurrentCooldown;
	[SerializeField] private bool isStart;
	[SerializeField] private bool isChoosing;
	[SerializeField] private bool guideShowing;

	[SerializeField] private float responseTime;

	[SerializeField] private ParticleSystem confettiParticle;

	private void Awake()
	{
		Instance = this;
	}
	private void OnDestroy()
	{
		OnDotClicked = null;
		OnShowResult = null;
		OnNext = null;
		OnLevelCleared = null;
		OnGlobalStageAdded = null;
	}
	private void Update()
	{
		if (isStart && isChoosing)
		{
			if (showGuidecurrentCooldown > 0)
			{
				showGuidecurrentCooldown -= Time.deltaTime;
			}
			else if (showGuidecurrentCooldown < 0)
			{
				Debug.Log("Showing Guide");
				AudioManager.Instance.PlaySfx("Guide Show");
				if (currentStageLevel == Stage.Differentcolor)
				{
					guideUI.Show("Tekan Lingkaran yang warnanya\n berbeda dari yang lain!");
				}
				else {
					guideUI.Show("Tekan Lingkaran yang\n warnanya sama!");
				}
				guideShowing = true;
				showGuidecurrentCooldown = 0;
			}
			responseTime += Time.deltaTime;
		}
	}
	public void ShowGuide()
	{
		showGuidecurrentCooldown = 0.1f;
	}
	public void HideGuide()
	{
		if (guideShowing)
		{
			guideUI.Hide();
			ResetGuide();
		}
	}
	public void ResetGuide()
	{
		showGuidecurrentCooldown = 5;
	}
	public void NextStage()
	{
		Debug.Log("NextStage");
		GameManager.Instance.StartStage(Stage.Differentcolor);
	}

	private void Start()
	{
		Application.targetFrameRate = 60;
		LeanTween.reset();
		gameUI.Hide();
		mainMenuUI.Show();
		AudioManager.Instance.PlayMusic("Music 1");
	}
	public void OpenGuide()
	{
		guideManager.Show();
	}
	public void CloseGuide()
	{
		guideManager.Hide();
	}

	public void QuitGame()
	{
		Application.Quit();
	}
	public void StartStage(Stage stage)
	{
		if (stage == Stage.Differentcolor)
		{
			currentStageLevel = stage;
			camColorController.ChangeColor(Level.Protan);
			dotsManager.StartDots();
			ShowGuide();
		}
		else if (stage == Stage.SameColor)
		{
			currentStageLevel = stage;
			dotsManager.ClearDots();
			sameColorDotsManager.Play();
			ShowGuide();
		}
	}
	public void StartGame()
	{
		AudioManager.Instance.StopMusic();
		AudioManager.Instance.PlayMusic("Music 2");
		gameUI.Show();
		currentLevel = Level.Protan;
		sameColorDotsManager.ResetLevel();
		dotsManager.StartPlay();
		currentStage = 1;
		currentDifficulty = 1;
		protanPoint = 0.5f;
		deutanPoint = 0.5f;
		tritanPoint = 0.5f;
		currentGlobalStage = 1;
		StartStage(Stage.Differentcolor);
		dotsManager.SetStage(currentDifficulty - 1);
		isChoosing = true;
		OnDotClicked?.Invoke();
		mainMenuUI.Hide();
		OnGlobalStageAdded.Invoke(currentGlobalStage);
		ClearAnswer();
		isStart = true;
		guideShowing = true;
		guideUI.Show();
	}
	private void ClearAnswer()
	{
		answers.Clear();
	}
	public void SetIsChoosing(bool val) 
	{
		isChoosing = val;
	}
	public void AnswerSameColorDots(bool correct)
	{
		answers.Add(new Answer(correct, currentLevel, currentStageLevel, currentStage,currentGlobalStage, responseTime));
		this.responseTime = 0;
	}
	private void AddAnswer(bool correct, Level level,Stage stageLevel, int stage, int globalStage,float responseTime)
	{
		answers.Add(new Answer(correct, level,stageLevel, stage, globalStage, responseTime));
		this.responseTime = 0;
	}
	public void GoToMenu()
	{
		gameUI.Hide();
		mainMenuUI.Show();
		AudioManager.Instance.StopMusic();
		AudioManager.Instance.PlayMusic("Music 1");
		camColorController.ChangeColorToMenu();
	}
	public void ClickDot(Dot dot)
	{
		if (isAnswering)
			return;
		if (guideShowing)
		{
			guideUI.Hide();
		}
		if (currentStage > 4)
		{
			isAnswering = true;
			isChoosing = false;
			if (dot.isDifferent)
			{
				OnAnswer?.Invoke(true, currentStage - 1);
				AddAnswer(true, currentLevel, currentStageLevel, currentStage, currentGlobalStage, responseTime);
				dotsManager.ClearDots();
				if (currentLevel == Level.Protan)
				{
					StartCoroutine(NextLevelDelay(Level.Deutan));
				}
				else if (currentLevel == Level.Deutan)
				{
					StartCoroutine(NextLevelDelay(Level.Tritan));
				}
				else if (currentLevel == Level.Tritan)
				{
					StartCoroutine(LastStageDelay());
					//StartCoroutine(ShowResultDelay());
				}
				AudioManager.Instance.PlaySfx("Jawab Benar");

			}
			else
			{
				OnAnswer?.Invoke(false, currentStage - 1);
				AddAnswer(false, currentLevel, currentStageLevel, currentStage, currentGlobalStage, responseTime);
				dotsManager.ClearWrongColor();
				if (currentLevel == Level.Protan)
				{
					StartCoroutine(NextLevelDelay(Level.Deutan));
				}
				else if (currentLevel == Level.Deutan)
				{
					StartCoroutine(NextLevelDelay(Level.Tritan));
				}
				else if (currentLevel == Level.Tritan)
				{
					StartCoroutine(LastStageDelay());
					//StartCoroutine(ShowResultDelay());
				}
				AudioManager.Instance.PlaySfx("Jawab Salah");

			}
			Debug.Log("Level Clear!");
		}
		else
		{
			isChoosing = false;
			if (dot.isDifferent)
			{
				OnAnswer?.Invoke(true, currentStage - 1);
				Debug.Log("Jawaban Benar");
				Next(true, dot);
				AudioManager.Instance.PlaySfx("Jawab Benar");
			}
			else
			{
				OnAnswer?.Invoke(false, currentStage - 1);
				Debug.Log("Jawaban Salah");
				if (currentLevel == Level.Protan)
				{
					protanPoint -= 0.1f;
				}
				else if (currentLevel == Level.Deutan)
				{
					deutanPoint -= 0.1f;
				}
				else if (currentLevel == Level.Tritan)
				{
					tritanPoint -= 0.1f;
				}
				Next(false, dot);
				AudioManager.Instance.PlaySfx("Jawab Salah");

			}
		}
		OnDotClicked?.Invoke();
	}
	public void ViewGameResult()
	{

		ShowResult(protanPoint + deutanPoint + tritanPoint + sameColorDotsManager.score);
	}
	public List<Answer> GetAnswers()
	{
		return answers;
	}
	public IEnumerator ShowResultDelay()
	{
		isStart = false;
		dotsManager.ClearDots();
		yield return new WaitForSeconds(2);
		exampleDisplay.Hide();
		ShowResult(protanPoint + deutanPoint + tritanPoint + sameColorDotsManager.score);
		dotsManager.ClearDots();
		Debug.Log("Clear! Show Result");
		isAnswering = false;
		OnNext?.Invoke();
	}
	public void Next(bool isCorrect, Dot dot)
	{
		isAnswering = true;
		AddAnswer(isCorrect, currentLevel, currentStageLevel, currentStage, currentGlobalStage, responseTime);
		if (isCorrect)
		{
			dotsManager.ClearDots();
			StartCoroutine(NextCorrectDelay(dot));
		}
		else
		{
			dotsManager.ClearWrongColor();
			StartCoroutine(NextIncorrectDelay());
		}
	}
	public IEnumerator NextCorrectDelay(Dot dot)
	{
		yield return new WaitForSeconds(2f);
		exampleDisplay.Hide();
		dotsManager.DecreaseSaturationLevel();
		currentStage++;
		currentDifficulty++;
		Vector3 rgb = dot.GetRGB();
		rgbStored.Push(dotsManager.GetRGB());
		currentGlobalStage++;
		OnGlobalStageAdded.Invoke(currentGlobalStage);
		dotsManager.SetStage(currentDifficulty - 1);
		dotsManager.NextStage();
		OnNext?.Invoke();
		isAnswering = false;
		isChoosing = true;
		showGuidecurrentCooldown = showGuideMaxCooldown;
	}
	public void AddGlobalStage()
	{
		currentGlobalStage++;
		OnGlobalStageAdded.Invoke(currentGlobalStage);
	}
	public IEnumerator NextIncorrectDelay()
	{

		yield return new WaitForSeconds(3f);
		dotsManager.IncreaseSaturationLevel();
		currentStage++;
		if (currentDifficulty > 1)
		{
			currentDifficulty--;
		}
		dotsManager.SetStage(currentDifficulty - 1);
		dotsManager.NextStage();
		OnNext?.Invoke();
		currentGlobalStage++;
		OnGlobalStageAdded.Invoke(currentGlobalStage);
		isAnswering = false;
		isChoosing = true;
		showGuidecurrentCooldown = showGuideMaxCooldown;

	}
	public void ChangeLevel()
	{
		if (currentLevel == Level.Protan)
		{
			currentLevel = Level.Deutan;
			dotsManager.SetLevel(currentLevel);
		}
		else if (currentLevel == Level.Deutan)
		{
			currentLevel = Level.Tritan;
			dotsManager.SetLevel(currentLevel);
		}
		else if (currentLevel == Level.Tritan)
		{
			StartCoroutine(LastStageDelay());
			//StartCoroutine(ShowResultDelay());
		}
	}
	public IEnumerator NextLevelDelay(Level level)
	{

		camColorController.ChangeColor(level);
		yield return new WaitForSeconds(2f);
		currentGlobalStage++;
		OnGlobalStageAdded.Invoke(currentGlobalStage);
		currentStage = 1;
		currentDifficulty = 1;
		dotsManager.SetLevel(currentLevel);
		dotsManager.SetStage(currentDifficulty - 1);
		dotsManager.NextStage();
		OnNext?.Invoke();
		isAnswering = false;
		isChoosing = true;
		exampleDisplay.Hide();
		showGuidecurrentCooldown = showGuideMaxCooldown;
		if (currentStageLevel == Stage.Differentcolor)
		{
			StartStage(Stage.SameColor);
		}
	}
	public IEnumerator LastStageDelay()
	{
		yield return new WaitForSeconds(2f);
		//currentGlobalStage++;
		//OnGlobalStageAdded.Invoke(currentGlobalStage);
		//currentStage = 1;
		//currentDifficulty = 1;
		dotsManager.SetLevel(currentLevel);
		dotsManager.SetStage(currentDifficulty - 1);
		dotsManager.NextStage();
		OnNext?.Invoke();
		isAnswering = false;
		isChoosing = true;
		exampleDisplay.Hide();
		showGuidecurrentCooldown = showGuideMaxCooldown;
		if (currentStageLevel == Stage.Differentcolor)
		{
			StartStage(Stage.SameColor);
		}
	}
	public int GetGlobalStage()
	{
		return currentGlobalStage;
	}
	public void ShowResult(float result)
	{
		isStart = false;
		confettiParticle.Play();
		OnShowResult?.Invoke(result);
	}
}
