using UnityEngine;
using System.Collections;

public class coinsBehaviour : MonoBehaviour {

	public float fuerzaRotatoria = 5f;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(Vector3.up * Time.deltaTime * fuerzaRotatoria);
	}
}
