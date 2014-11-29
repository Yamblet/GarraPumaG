using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CrazyTapping : MonoBehaviour {

	Animator animTapper;
	GameManager gameManager;
	void Start () 
	{
		animTapper = gameObject.GetComponent<Animator>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();//consigo el componente del objecto -GameManager- para asi poder usar su variable score.
	}
	
	public void hasBeenTapped()
	{
		animTapper.SetBool("IsTapped", true);
		gameManager.tapCounter  += 1;

	}
}
