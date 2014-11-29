using UnityEngine;
using System.Collections;

public class StadiumAsset : MonoBehaviour {

	public GameObject[] stadiumMesh;

	public GameObject chosenStadium;
	
	private static StadiumAsset instance;
	
	void Awake()
	{
		// allow only one instance of the Main Menu
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		
		DontDestroyOnLoad(gameObject);
		instance = this;
	}
	
	void Update () 
	{
		//		Debug.Log ("You have chosen this character: " + chosenCharacter.gameObject.name);
	}
}

