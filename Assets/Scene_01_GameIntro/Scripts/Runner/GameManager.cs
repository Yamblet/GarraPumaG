using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	private float timer = 0; // variable tiempo
	public int score = 0; // marcador
	private int highscore = 0;//Contador de marcador alto
	public int tapCounter; // Contador de taps cuando se activa el duelo de taps.
	
	public float timeTurboZone = 0;
	public float timeReduceVel = 0;
		
	public Text scoreText;
	public Text timeText;
	public Text highscoreLbl;
	
	public bool isTurbo; // TURBO ACTIVATED?
	public bool hasCrashedEnemy; // HAS CRASHED??

	public GameObject pauseMenu;
	public GameObject DueloTapMenu;

	public bool isDueloTapMenuActive;

	public Button pauseButton;

	public GameObject gameOverScreen;
	public GameObject pausaButton;


	void Start ()
	{
		DueloTapMenu.SetActive(false);
		isDueloTapMenuActive = false;

		Application.targetFrameRate = -1;
		highscore = PlayerPrefs.GetInt("High Score Lvl 1");
//		Debug.Log("Highscore: " + highscore);
		score = 0; 
		timer = 3;
	}
	
	void Update ()
	{
		scoreText.text = score.ToString(); //DISPLAYS SCORE.

		if(timeTurboZone > 0 && isTurbo) //MANAGE TIMERTURBO AND GOES 1 UNIT LESS TIL ZERO BASED ON DELTA TIME.
		{
			timeTurboZone -= Time.deltaTime;

			if(timeTurboZone <= 0)//DEACTIVATES TURBO.
			{
				isTurbo = false;
				timeTurboZone = 0;
			}
		}

		if(timeReduceVel > 0 && hasCrashedEnemy)//MANAGE TIMER SLOW BY CRASHING WITH ENEMIES AND GOES 1 UNIT LESS TIL ZERO BASED ON DELTA TIME.
		{
			timeReduceVel -= Time.deltaTime;
			if(timeReduceVel <= 0) //DEACTIVATES SLOW VELOCITY FOR CRASHING.
			{
				hasCrashedEnemy = false;
				timeReduceVel = 0;
			}
		}

		///DUELO TAP CONTROLLING
		///
		if(isDueloTapMenuActive == true)
		{
			DueloTapMenu.SetActive(true);
		}
		else
		{
			DueloTapMenu.SetActive(false);
		}

		////
		/// DUELO TAPPING
		/// 
		/// 
		if(isDueloTapMenuActive == true)
		{
			timer -= TimeController.deltaTime; //STARTS TIME WHEN THE MAIN ANIMATION ENDS.
			timeText.text = "Time: " +  timer.ToString("f0") + " secs";		

			if((timer > 0) && (tapCounter >= 6))
			{
				Time.timeScale = 1;
				isDueloTapMenuActive = false;
				timer = 3;
			}
			 
			if(timer < 0)
			{
				Time.timeScale = 0;
				timer = 3;
				isDueloTapMenuActive = false;

				gameOverScreen.SetActive(true);
				pausaButton.SetActive(false);
			}
		}
	}

	public void MenuPause()
	{
		pausaButton.SetActive(false);
		Time.timeScale = 0.0f;
		pauseMenu.SetActive(true);
	}

	//Menu options.
	public void BackToGame()
	{
		pausaButton.SetActive(true);
		Time.timeScale = 1f;
		pauseMenu.SetActive(false);
	}

	public void BackToMenu()
	{
		Time.timeScale = 1f;
		Application.LoadLevel(0);
	}

	public void ResetScene()
	{
		Time.timeScale = 1f;
		Application.LoadLevel(Application.loadedLevel);
	}


	// SETTING HIGH SCORES.
	public void SetHighScore()
	{
		if(score > highscore)
		{
			highscore = score;
			PlayerPrefs.SetInt("High Score Lvl 1", highscore);

			Debug.Log("High Score is " + highscore );
		}
	}
}
