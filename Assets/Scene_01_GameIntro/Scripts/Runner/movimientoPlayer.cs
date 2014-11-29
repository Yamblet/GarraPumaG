using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class movimientoPlayer : MonoBehaviour {

	public Animator animBall;
	public Animator animCollider;

	private bool isBallMoving;
	
	public float acceletometerVelocity;

	public Transform particlePos;
	public GameObject particleInst;
	public GameObject particleTurbo;
	public GameObject particleCoin;

	private GameObject clone;

	public Collision playerCollision;
	//BOUNDARIES FOR THE PLAYER MOVEMENT
	public float minX;
	public float maxX;

	//MANAGEMENT OF TOUCHES
	private Touch touch;//Variable Touch
	private Vector2 posicionInicialTouch2; //posicion inicial de touch al pegar dedo a la pantalla
	private Vector2 posicionFinalTouch2;	//posicion final de touch al despegar dedo de la pantalla

	private GameManager gameManager;

	public float fuerza = 10f; //Velocidad de movimiento hacia enfrente.
	public float impulseJump = 9f; //fuerza de salto 
	public float turboVelocity = 18;
	public float limitVel = 13;

	public static bool isGrounded;// si toca el suelo se activa esta variable booleana
	private bool isMoving; // se movera el personaje cuando esta variable sea true

	public AudioSource coinSound; //sonido de moneda

	public float initialTime= 0;

	public static int lives; //numero de vidas disponibles.

	[HideInInspector]
	public bool isDribling;//Public variable to enter dribling mode
	public bool isForcing; //Public variable to enter forcing mode

	//UI MANAGEMENT
	public Image barraVida;
	public Sprite[] lifeBar;

	void Awake()
	{
		Time.timeScale = 1;
	}

	void Start () 
	{
		isBallMoving = false;

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();//consigo el componente del objecto -GameManager- para asi poder usar su variable score.
//		transform.position = new Vector3(0,.2f,0); //Posicion con la que empieza el player desde que corre el juego.
		animCollider = GameObject.Find("CharacterPos").GetComponent<Animator>();
		animCollider.SetBool("isJumping", false);

		isDribling=false;  //Esta driblando?
		isGrounded = true; //Esta en el suelo??
		isMoving = true; // esta moviendo??
		gameManager.isTurbo = false;// esta activado el turbo??
		gameManager.hasCrashedEnemy = false;

		lives = 2; //Total de vidas.

		initialTime = Time.time;
	}


	void Update() 
	{
//		if(isGrounded == false)
//		{
//			animCollider.SetBool("isJumping", true);
//		}
//		else
//		{
//			animCollider.SetBool("isJumping", false);
//		}
//		Debug.Log(isGrounded);
//		Debug.Log("Vidas" + lives);
		if(lives == 2)
		{
			barraVida.sprite = lifeBar[lives];
		}

		int touchCount = Input.touchCount;//variable que tendra el contador de los touches
		TouchesControl(touchCount);//Funcion que maneja Touches

		OutOfBounds(); //funcion que indica las fronteras de movimiento del jugador

		if(AnimacionesCamara.actual.animacion == false && isMoving == true)//Pulls the value from the animacionesCamara script and if this is false the player can move.
		{
			isBallMoving = true;
			animBall.SetBool("isMoving", isBallMoving);

			while(fuerza <= limitVel && fuerza >= 0)
			{
				fuerza += 6 *  Time.fixedDeltaTime;// aumenta velocidad gradualmente
			}

			if(gameManager.hasCrashedEnemy)
			{
				fuerza = 3;
			}

			transform.Translate(new Vector3(Input.acceleration.x * Time.deltaTime * acceletometerVelocity ,0,1 * fuerza * Time.deltaTime));//Movimiento controlado atraves del acelerometro

			if(fuerza >= limitVel) // if velocity = 10f;
			{
				fuerza = limitVel;//locks the velocity to 10 when this reaches this value.
				if(gameManager.isTurbo)//if turbo == true 
				{
					fuerza = turboVelocity; //supervelocity
				}
				else if(gameManager.isTurbo == false)
				{
					fuerza = limitVel;
				}
			}
		}	
	}
	
	void  OutOfBounds() //Controla las fronteras de movimiento del jugador.
	{
		if (transform.position.x < minX) //xBorder to left
		{
			transform.position = new Vector3(minX, transform.position.y, transform.position.z);
		}
		if (transform.position.x >= maxX) //xBorder to right
		{
			transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
		}
	}

	//CONTROL DE TOUCHES

	void TouchesControl( int touchCount)
	{
		for(int i = 0;i < touchCount; i++)
		{
			touch = Input.GetTouch(i);
			
			if(touch.phase == TouchPhase.Began) 
			{
				posicionInicialTouch2 = touch.position;
				posicionFinalTouch2 = touch.position;
			}

			if(touch.phase == TouchPhase.Moved)
			{
				posicionFinalTouch2 = touch.position;
			}

			if(touch.phase == TouchPhase.Ended)
			{
				if(((posicionFinalTouch2.y - posicionInicialTouch2.y) > 30) && ((posicionFinalTouch2.x - posicionInicialTouch2.x) < 200f) && isGrounded == true && AnimacionesCamara.actual.animacion == false)
				{
					isGrounded = false;
					CharacterSelected.animMec.SetBool("isGrounded", false);
					AnimacionesCamara.actual.anim.SetBool("salto",true);
					rigidbody.AddForce(Vector3.up * impulseJump, ForceMode.Impulse); // Jumping force Impulse
				}
			}
		}
	}


	void OnTriggerEnter(Collider other) 
	{	
		if(other.gameObject.tag == "Goal")
		{
			Application.LoadLevel(Application.loadedLevel);
			gameManager.SetHighScore();
		}//collider on trigger if player enters turbozone goes super fast!
	}
	
	//aumenta score cada vez que colisiona con una moneda.
	void OnCollisionEnter(Collision collision) 
	{
		if(collision.gameObject.tag == "Coin") //BEHAVIOUR COIN WHEN THIS COLLIDES
		{
			clone = Instantiate(particleCoin, particlePos.position, Quaternion.identity) as GameObject;//Instanciar particula y destruir
			Destroy(clone, particleCoin.particleSystem.duration);

			coinSound.Play();
			gameManager.score += 1;// each time coin collides sums up the score 1.
		}

		if(collision.gameObject.tag == "floor") //BEHAVIOUR FOR MAKING THE JUMP TRUE OR FALSE
		{
//			Debug.Log(isGrounded);
			isGrounded = true; // THE PLAYER IS ON THE FLOOR.
			CharacterSelected.animMec.SetBool("isGrounded", true);
			AnimacionesCamara.actual.anim.SetBool("salto",false);
		}

		if(collision.gameObject.tag == "TurboZone")//collider on trigger if player enters turbozone goes super fast!
		{

			clone = Instantiate(particleTurbo, particlePos.position, Quaternion.identity) as GameObject;//Instanciar particula y destruir
			Destroy(clone, particleTurbo.particleSystem.duration);

			gameManager.timeTurboZone += .5f; //sum up 5 to time limit in turbo zone
			gameManager.timeReduceVel = 0f;// resets slow time if was active.
			
			gameManager.hasCrashedEnemy = false; //
			gameManager.isTurbo = true;

			Destroy(collision.gameObject);
		}

		if(gameObject.tag == "Player" && collision.gameObject.tag == "Enemy") //BEHAVIOUR WHEN PLAYER COLLIDES WITH THE ENEMY
		{
			clone = Instantiate(particleInst, particlePos.position, Quaternion.identity) as GameObject;//Instanciar particula y destruir
			Destroy(clone, particleInst.particleSystem.duration); // destruye particul despues del tiempo que dure.

			gameManager.hasCrashedEnemy = true; // The enemy has crashed.
			gameManager.timeReduceVel = 2.0f;//Sums up 2 to time of slow by crashing.

			gameManager.timeTurboZone = 0.0f; //Reset turbo time limit if active
			gameManager.isTurbo = false;

			collision.gameObject.collider.enabled = false; //Si enemigo ya choco con jguador ya no puede mas
			Destroy(collision.transform.root.gameObject, 1); // destruye el padre del objeto hijo enemigo

			RecuperaBalon.hasCrashedEnemyToActivateDuel = true;

			lives-= 1; // one live less
		
			if(lives == 1)
			{
				barraVida.sprite = lifeBar[lives];
			}
			else if(lives == 0)
			{
				barraVida.sprite = lifeBar[lives];
			}


			if(lives < 0)
			{
				isMoving = false;
			}
		}

		if(collision.gameObject.tag == "DueloTap") //BEHAVIOUR WHEN PLAYER COLLIDES WITH THE ENEMY THAT ACTIVATES THE TAP DUEL
		{
			Time.timeScale = .003f;
			gameManager.isDueloTapMenuActive = true;
		}


		if(collision.gameObject.tag == "EnemyDrible") //if the player collides with a Drible enemy, DriblingMode begins
		{
			isDribling=true;
		}
	}

	//Set for isDribling
	public void SetIsDribling(bool isDribling)
	{
		this.isDribling=isDribling;
	}
	//Set for isForcing
	public void SetIsForcing(bool isForcing)
	{
		this.isForcing=isForcing;
	}
	//Set for isMoving
	public void SetIsMoving(bool isMoving)
	{
		this.isMoving=isMoving;
	}
	public bool GetIsGrounded()
	{
		return isGrounded;
	}
}
