using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	[SerializeField] private Transform player;
	[SerializeField] private Text uiTitle;
	[SerializeField] private Text uiScore;
	[SerializeField] private Text uiFinalScore;
	[SerializeField] private Image uiGameOver;
	private bool gameOver;
	private bool firstStart;
	private bool playAgain;
	private int score;
	private bool isTouching;

	public static Manager Instance;

	public bool GameOver
	{
		get
		{
			return gameOver;
		}
	}
	public bool FirstStart
	{
		get
		{
			return firstStart;
		}
	}
	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
		}
	}

	void Awake () 
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		uiTitle = uiTitle.GetComponent<Text>();
		uiScore = uiScore.GetComponent<Text>();
		uiFinalScore = uiFinalScore.GetComponent<Text>();
		uiGameOver = uiGameOver.GetComponent<Image>();

		uiTitle.enabled = true;
		uiScore.enabled = false;
		uiFinalScore.enabled = false;
		uiGameOver.enabled = false;

		gameOver = true;
		firstStart = true;
		playAgain = false;
		score = 0;
	}
	
	void Update()
	{
		#if UNITY_EDITOR
		if(firstStart)
		{
			if (Input.GetAxis("Jump") > 0)
			{
				uiTitle.enabled = false;
				uiScore.enabled = true;
				gameOver = false;
				firstStart = false;
			}
		}

		if(playAgain)
		{
			if (Input.GetAxis("Jump") > 0)
			{
				score = 0;
				uiTitle.enabled = false;
				uiScore.enabled = true;
				gameOver = false;
				playAgain = false;
				SceneManager.LoadScene("main");
			}
		}

		#elif UNITY_ANDROID
		if(firstStart)
		{
			if(Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				if(touch.phase == TouchPhase.Began)
				{
					uiTitle.enabled = false;
					uiScore.enabled = true;
					gameOver = false;
					firstStart = false;
				}
			}
		}
		if(playAgain)
		{
			if(Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				if(touch.phase == TouchPhase.Began)
				{
					score = 0;
					uiTitle.enabled = false;
					uiScore.enabled = true;
					gameOver = false;
					playAgain = false;
					SceneManager.LoadScene("main");
				}
			}
		}
		#endif
	}

	void LateUpdate () 
	{
		// Camera movement
		transform.position = new Vector3(player.position.x + 1.5f, 0, -10);
	}

	public void GameEnded()
	{
		gameOver = true;
		// Bring up Game Over logo and final score
		uiGameOver.enabled = true;
		uiFinalScore.enabled = true;
		uiFinalScore.text = uiScore.text;
		uiScore.enabled = false;
		// Start new game on click/tap
		playAgain = true;
	}

	public void UpdateScore()
	{
		string s = string.Format("Score: {0}", score);
		uiScore.text = s;
	}

	public void Tap()
	{
		if(Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Began)
			{
				// user is touching screen. 
				isTouching = true;
			}
		}
	}
}
