using UnityEngine;
using System.Collections;

public class DestroyCoins : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Player")
//			Debug.Log("Destroyed");
			Destroy(gameObject);
	}
}
