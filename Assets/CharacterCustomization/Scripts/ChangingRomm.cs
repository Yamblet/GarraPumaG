using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangingRomm : MonoBehaviour {

	private int charModelIndex = 0;

	private CharacterAsset ca;
	private StadiumAsset sa;

	private GameObject model;

	private string stadiumSelected;

	public GameObject ObjLoadingImage;

	private bool hasChangeScene = false;
	
	void Start ()
	{
		sa = GameObject.Find("_StadiumAssetManager").GetComponent<StadiumAsset>();
		ca = GameObject.Find("_CharacterAssetManager").GetComponent<CharacterAsset>();

		stadiumSelected = sa.chosenStadium.name;

		InstantiateCharacterModel();
		hasChangeScene = false;
	}

	void Update()
	{
//		Debug.Log(hasChangeScene);
		if(hasChangeScene == true)
		{
			ObjLoadingImage.SetActive(true);

			Application.LoadLevel(stadiumSelected);
		}
	}

	private void InstantiateCharacterModel()
	{
		switch(charModelIndex)
		{
		case 0:
			ca.chosenCharacter = ca.characterMesh[0];
			break;
		case 1:
			ca.chosenCharacter = ca.characterMesh[1];
			break;
		case 2 :
			ca.chosenCharacter = ca.characterMesh[2];
			break;
		case 3 :
			ca.chosenCharacter = ca.characterMesh[3];
			break;	
			
		default:
			if(charModelIndex < 0)
			{
			charModelIndex = 3;
			ca.chosenCharacter = ca.characterMesh[3];
			}

			if(charModelIndex > 3)
			{
				charModelIndex = 0;
				ca.chosenCharacter = ca.characterMesh[0];
			}
			break;
		}

		if(transform.childCount != 0)
		{
			for(int i = 0; i < transform.childCount; i++)
				Destroy(transform.GetChild(i).gameObject);
		}
		
		model = Instantiate(ca.characterMesh[charModelIndex] , transform.position, Quaternion.identity) as GameObject;
		model.transform.parent = transform;
		model.transform.rotation = transform.rotation;
	}	

	void OnClickRight()
	{
		charModelIndex++;
		InstantiateCharacterModel();
	}
	
	void OnClickLeft()
	{
		charModelIndex--;
		InstantiateCharacterModel();
	}

	void GoToGame()
	{
		hasChangeScene = true;
	}

	void ReturnMenu()
	{
		Application.LoadLevel(0);
	}
}
