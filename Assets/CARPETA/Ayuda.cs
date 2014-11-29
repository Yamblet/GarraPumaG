using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ayuda : MonoBehaviour {

	private int index = 0;
	public Sprite[] tutorialImages;
	public Image tutorial;
	public GameObject tutorialPanel;
	public GameObject mainMenu;

	public GameObject botonTutorial1;
	public GameObject botonTutorial2;

	public GameObject moverTutorial1;
	public GameObject moverTutorial2;
	public GameObject moverTutorial3;


	public GameObject saltarTutorial1;
	public GameObject saltarTutorial2;
	public GameObject saltarTutorial3;


	void Start ()
	{
		index = 0;
		botonTutorial1.SetActive(false);
		botonTutorial2.SetActive(false);

		saltarTutorial1.SetActive(false);
		saltarTutorial2.SetActive(false);
		saltarTutorial3.SetActive(false);

		moverTutorial1.SetActive(false);
		moverTutorial2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(index)
		{
		case 0:
			tutorial.sprite = tutorialImages[0];

			botonTutorial1.SetActive(true);
			botonTutorial2.SetActive(true);

			saltarTutorial1.SetActive(false);
			saltarTutorial2.SetActive(false);
			saltarTutorial3.SetActive(false);

			moverTutorial1.SetActive(false);
			moverTutorial2.SetActive(false);
			break;
		case 1:
			tutorial.sprite = tutorialImages[1];
			botonTutorial1.SetActive(false);
			botonTutorial2.SetActive(false);

			saltarTutorial1.SetActive(false);
			saltarTutorial2.SetActive(false);
			saltarTutorial3.SetActive(false);

			moverTutorial1.SetActive(true);
			moverTutorial2.SetActive(true);
			break;
		case 2:
			tutorial.sprite = tutorialImages[2];
			botonTutorial1.SetActive(false);
			botonTutorial2.SetActive(false);

			saltarTutorial1.SetActive(false);
			saltarTutorial2.SetActive(false);
			saltarTutorial3.SetActive(false);

			moverTutorial1.SetActive(true);
			moverTutorial2.SetActive(true);
			break;
		case 3:
			tutorial.sprite = tutorialImages[3];
			botonTutorial1.SetActive(false);
			botonTutorial2.SetActive(false);

			saltarTutorial1.SetActive(false);
			saltarTutorial2.SetActive(false);
			saltarTutorial3.SetActive(false);

			moverTutorial1.SetActive(true);
			moverTutorial2.SetActive(true);
			break;
		case 4:
			tutorial.sprite = tutorialImages[4];
			botonTutorial1.SetActive(false);
			botonTutorial2.SetActive(false);

			saltarTutorial1.SetActive(true);
			saltarTutorial2.SetActive(true);
			saltarTutorial3.SetActive(true);

			moverTutorial1.SetActive(false);
			moverTutorial2.SetActive(false);
			break;
		case 5:
			tutorial.sprite = tutorialImages[5];
			botonTutorial1.SetActive(false);
			botonTutorial2.SetActive(false);

			saltarTutorial1.SetActive(false);
			saltarTutorial2.SetActive(false);
			saltarTutorial3.SetActive(false);

			moverTutorial1.SetActive(false);
			moverTutorial2.SetActive(false);
			break;
		default:
			if(index > 5)
			{
				mainMenu.SetActive(true);
				tutorialPanel.SetActive(false);
				index = 0;
			}
			if(index < 0)
			{
				mainMenu.SetActive(true);
				tutorialPanel.SetActive(false);
				index = 0;
			}
			break;
		}
	}

	public void SlideRight()
	{
		index++;
	}

	public void SlideLeft()
	{
		index--;
	}
}
