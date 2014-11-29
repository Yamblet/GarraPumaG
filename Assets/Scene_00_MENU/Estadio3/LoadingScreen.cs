using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LoadingScreen : MonoBehaviour 
{
	public string levelToLoad;

	public GameObject text;
	public GameObject progressBar;

//	public GameObject text1;
//	public GUIText highScoreTxt;

	private int loadProgress = 0;

	void Awake()
	{
		Time.timeScale = 1;
	}
	// Use this for initialization
	void Start () 
	{

		text.SetActive(false);
		progressBar.SetActive(false);
//		text1.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () 
	{
//		highScoreTxt.text = "Highscore: " + (ScoreManager.managerHighScore.highScore).ToString();
	}
	

	IEnumerator DisplayLoadingScreen(string level)
	{
		text.SetActive(true);
		progressBar.SetActive(true);

//		text1.SetActive(false);


		progressBar.transform.localScale = new Vector3(loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

		text.guiText.text = "Loading Progress " + loadProgress + " %";     

		AsyncOperation async = Application.LoadLevelAsync(level);
		while(!async.isDone && async.progress <= 100)
		{
			loadProgress = (int)(async.progress * 100);
			text.guiText.text = "Loading Progress " + loadProgress + "%";
			progressBar.transform.localScale = new Vector3(async.progress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
			yield return null;
		}
	}

	public void ChooseCharacter()
	{
		Application.LoadLevel(levelToLoad);
	}
}
