using UnityEngine;
using System.Collections;

public class SimulacionEstadio : MonoBehaviour {

//	public GameObject player;
//	public GameObject estadio;
//	public Transform porteria;
//	public Transform ladoIzq;
//	public Transform ladoDer;
//	public Transform pisoEstadio;
//	public float velocidadCrecimiento=1;
//	private float distanciaEstadioPlayer;
//	private float alturaEstadio;
//	private float escalaXporteria;
//	private float anguloLado;
//	// Use this for initialization
//	void Start () {
//		escalaXporteria=0;
//		distanciaEstadioPlayer=estadio.transform.position.z-player.transform.position.z;
//		alturaEstadio=0;
//		anguloLado=0;
//	}
//	
//	// Update is called once per frame
//	void Update () {
////Moves the stadium with the player but also slowly moving towards the player and discretly making it taller
//		estadio.transform.position=new Vector3(estadio.transform.position.x,estadio.transform.position.y+alturaEstadio,
//		                                       	  player.transform.position.z+distanciaEstadioPlayer);
//		estadio.transform.eulerAngles=new Vector3(player.transform.eulerAngles.x,player.transform.eulerAngles.y,
//		                                          estadio.transform.eulerAngles.z);
////Moves the floor of the stadium with the stadium z axis
//		pisoEstadio.position=new Vector3(pisoEstadio.transform.position.x,pisoEstadio.transform.position.y,
//		                                 		  estadio.transform.position.z);
////Slowly makes the goal bigger on its x axis 
//		porteria.localScale=new Vector3(porteria.localScale.x,porteria.localScale.y,
//		                                          porteria.localScale.z+escalaXporteria);
////Rotates the side walls 
//		ladoIzq.eulerAngles=new Vector3(ladoIzq.eulerAngles.x,ladoIzq.eulerAngles.y-anguloLado,ladoIzq.eulerAngles.z);
//		ladoDer.eulerAngles=new Vector3(ladoDer.eulerAngles.x,ladoDer.eulerAngles.y+anguloLado,ladoDer.eulerAngles.z);
////Change values for all the stadium pieces 
//		escalaXporteria+=0.000012f*velocidadCrecimiento;
//		distanciaEstadioPlayer-=0.02f*velocidadCrecimiento;
//		alturaEstadio+=0.0000028f*velocidadCrecimiento;
//		anguloLado+=0.00003f*velocidadCrecimiento;
//	}
}
