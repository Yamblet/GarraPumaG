using UnityEngine;
using System.Collections;

public class AnimacionesCamara : MonoBehaviour
{
	
		public GameObject player;
		public static AnimacionesCamara actual;
		public bool animacion;
		private Vector3 ultimo;
		public Animator anim;
//		private float posicionInicioY;
//		private bool isJumping;

		void Awake()
		{
		anim = Camera.main.GetComponent<Animator>();
		}
	
		void Start ()
		{	
//				isJumping = false;
//				posicionInicioY = player.transform.position.y;
				animacion = true;
				actual = this;
				ultimo = transform.position;
				anim.SetBool ("begin", animacion);
				InvokeRepeating ("Movimiento", 7f, 0.3f);
		}
	
		void Movimiento ()
		{
				actual = this;
				animacion = false;
				anim.SetBool ("begin", animacion);
				float speed = (transform.position - ultimo).magnitude;
				anim.SetFloat ("speed", speed);
				ultimo = transform.position;
		}

		void DeActivateCamera()
		{

		}
}
