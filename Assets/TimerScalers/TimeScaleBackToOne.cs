using UnityEngine;
using System.Collections;

public class TimeScaleBackToOne : MonoBehaviour {

	
	// Update is called once per frame
	public void EndOfTheComicSequence()
	{
		gameObject.SetActive(false);
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevel);
	}
}
