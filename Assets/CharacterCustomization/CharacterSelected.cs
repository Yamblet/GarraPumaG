using UnityEngine;
using System.Collections;

public class CharacterSelected : MonoBehaviour {

	private CharacterAsset ca;
	private GameObject actualChosenCharacter;
	public static Animator animMec;
	private bool isRunning = false;
	
	void Awake()
	{
		ca = GameObject.Find("_CharacterAssetManager").GetComponent<CharacterAsset>();
	}

	// Use this for initialization
	void Start ()
	{;

		if(ca.chosenCharacter == null)
		{
			ca.chosenCharacter = ca.characterMesh[0];
			Debug.Log("Ejecutado en funcion start de CharacterSelected: " + ca.chosenCharacter.gameObject.name);
		}
		InstantiateCharacter();

		animMec = actualChosenCharacter.GetComponent<Animator>();
	}

	void Update()
	{


		if(AnimacionesCamara.actual.animacion == false)
		{
			isRunning = true;
			animMec.SetBool("IsRunning",isRunning);
		}
	}
	
	void InstantiateCharacter()
	{
		actualChosenCharacter = Instantiate(ca.chosenCharacter, new Vector3(transform.position.x,transform.position.y - .5f ,transform.position.z), Quaternion.identity) as GameObject;
		
		//position gameobject to choose.
		actualChosenCharacter.transform.parent = transform;
		actualChosenCharacter.transform.rotation = transform.rotation;
		//animation instantiation
	}

}
