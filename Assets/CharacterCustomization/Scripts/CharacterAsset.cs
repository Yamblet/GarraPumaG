using UnityEngine;
using System.Collections;

public class CharacterAsset : MonoBehaviour {

	public GameObject[] characterMesh;
	
	public GameObject chosenCharacter;

	private static CharacterAsset instance;

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
