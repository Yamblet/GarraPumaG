using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StadiumRoom : MonoBehaviour {

	private int stadiumModelIndex = 0;
	
	private StadiumAsset sa;
	private GameObject model;

	private Touch touch;//Variable Touch

	private Vector2 posicionInicialTouch2; //posicion inicial de touch al pegar dedo a la pantalla
	private Vector2 posicionFinalTouch2;	//posicion final de touch al despegar dedo de la pantalla

	public Image[] spritesSelectionDt;

	public Sprite unselected;
	public Sprite selected;
	
	void Start ()
	{
		sa = GameObject.Find("_StadiumAssetManager").GetComponent<StadiumAsset>();
		InstantiateCharacterModel();
	}

	void Update()
	{
		int touchCount = Input.touchCount;//variable que tendra el contador de los touches
		TouchesControl(touchCount);//Funcion que maneja Touches
	}
	
	private void InstantiateCharacterModel()
	{
		switch(stadiumModelIndex)
		{
		case 0:
			sa.chosenStadium = sa.stadiumMesh[0];

			spritesSelectionDt[0].sprite = selected;
			spritesSelectionDt[1].sprite = unselected;
			spritesSelectionDt[2].sprite = unselected;
			spritesSelectionDt[3].sprite = unselected;
			spritesSelectionDt[4].sprite = unselected;
			break;
		case 1:
			sa.chosenStadium = sa.stadiumMesh[1];

			spritesSelectionDt[0].sprite = unselected;
			spritesSelectionDt[1].sprite = selected;
			spritesSelectionDt[2].sprite = unselected;
			spritesSelectionDt[3].sprite = unselected;
			spritesSelectionDt[4].sprite = unselected;
			break;
		case 2 :
			sa.chosenStadium = sa.stadiumMesh[2];

			spritesSelectionDt[0].sprite = unselected;
			spritesSelectionDt[1].sprite = unselected;
			spritesSelectionDt[2].sprite = selected;
			spritesSelectionDt[3].sprite = unselected;
			spritesSelectionDt[4].sprite = unselected;
			break;
		case 3 :
			sa.chosenStadium = sa.stadiumMesh[3];

			spritesSelectionDt[0].sprite = unselected;
			spritesSelectionDt[1].sprite = unselected;
			spritesSelectionDt[2].sprite = unselected;
			spritesSelectionDt[3].sprite = selected;
			spritesSelectionDt[4].sprite = unselected;
			break;
		case 4:
			sa.chosenStadium = sa.stadiumMesh[4];

			spritesSelectionDt[0].sprite = unselected;
			spritesSelectionDt[1].sprite = unselected;
			spritesSelectionDt[2].sprite = unselected;
			spritesSelectionDt[3].sprite = unselected;
			spritesSelectionDt[4].sprite = selected;
			break;
		default:
			if(stadiumModelIndex < 0)
			{
				stadiumModelIndex = 4;
				sa.chosenStadium = sa.stadiumMesh[4];

				spritesSelectionDt[0].sprite = unselected;
				spritesSelectionDt[1].sprite = unselected;
				spritesSelectionDt[2].sprite = unselected;
				spritesSelectionDt[3].sprite = unselected;
				spritesSelectionDt[4].sprite = selected;
			}
			
			if(stadiumModelIndex > 4)
			{
				stadiumModelIndex = 0;
				sa.chosenStadium = sa.stadiumMesh[0];

				spritesSelectionDt[0].sprite = selected;
				spritesSelectionDt[1].sprite = unselected;
				spritesSelectionDt[2].sprite = unselected;
				spritesSelectionDt[3].sprite = unselected;
				spritesSelectionDt[4].sprite = unselected;
			}
			break;
		}
		
		if(transform.childCount != 0)
		{
			for(int i = 0; i < transform.childCount; i++)
				Destroy(transform.GetChild(i).gameObject);
		}

		model = Instantiate(sa.stadiumMesh[stadiumModelIndex] , transform.position, Quaternion.identity) as GameObject;
//		Debug.Log ("Has escogido a: " + sa.chosenStadium.name);
		
		//position gameobject to choose.
		model.transform.parent = transform;
		model.transform.rotation = transform.rotation;
		//animation instantiation
//		animMec = model.GetComponent<Animator>();
		
	}	

	void TouchesControl( int touchCount)
	{
		for(int i = 0;i < touchCount; i++)
		{
			touch = Input.GetTouch(i);
			
			if(touch.phase == TouchPhase.Began) 
			{
				posicionInicialTouch2 = touch.position;
				posicionFinalTouch2 = touch.position;
//				Debug.Log("Initial Pos: " + posicionInicialTouch2);

			}
			
			if(touch.phase == TouchPhase.Moved)
			{
				posicionFinalTouch2 = touch.position;
//				Debug.Log("Final Pos: " + posicionFinalTouch2);
//				Debug.Log("X: " + (posicionFinalTouch2.x - posicionInicialTouch2.x));
//				Debug.Log("Y: " + (posicionFinalTouch2.y - posicionInicialTouch2.y));
			}
			
			if(touch.phase == TouchPhase.Ended)
			{
				if(((posicionFinalTouch2.y - posicionInicialTouch2.y) <40) && ((posicionFinalTouch2.x - posicionInicialTouch2.x) > 0))
				{
					stadiumModelIndex--;
					InstantiateCharacterModel();
				}

				if(((posicionFinalTouch2.y - posicionInicialTouch2.y) < 40) && ((posicionFinalTouch2.x - posicionInicialTouch2.x) < 0))
				{
					stadiumModelIndex++;
					InstantiateCharacterModel();
				}
			}
		}
	}

	//	public void OnClickRight()
	//	{
	//		stadiumModelIndex++;
	//		InstantiateCharacterModel();
	//	}
	//	
	//	public void OnClickLeft()
	//	{
	//		stadiumModelIndex--;
	//		InstantiateCharacterModel();
	//	}
}
