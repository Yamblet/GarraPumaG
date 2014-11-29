using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecuperaBalon : MonoBehaviour {
	
	public GameObject objetoQueTieneElScriptDeVidas;
	private movimientoPlayer scriptVida;
	private GameManager gameManager;
//Variables para simular luz
	public Image luz;
	public float duracionSeg;
	private float incremento;
	private float light_f;
	private bool isLighting;
	private int numLight;
//Variables para el duelo
	public Image dueloBueno;
	public Image imagenDuelo;
	public Image  flechaDuelo;
	public Sprite[] imagenesDB;
	private int contBueno;
	private int contTiempoBueno;

	public GameObject barraVida;
	public GameObject GarritasCounter;

	//Entre menor sea mas resistencia opone la barra del jugador
	private const int DELAY_BUENO=4;

	public Image dueloMalo;
	public GameObject imagePaAtras;

	public Sprite imagenSecondParaAtras;

	public Sprite[] imagenesDM;
	private int contMalo;
	private int contTiempoMalo;
	//Entre menor sea mas rapido sube la barra enemiga
	private const int DELAY_MALO=7;

	public GameObject botonDuelo;
	public Image balonDuelo;
	public GameObject efectoLoco;

	private bool isDueling;
	public static bool hasCrashedEnemyToActivateDuel;

	public GameObject player;	

	void Awake()
	{
		imagePaAtras.SetActive(false);
	}

	void Start () 
	{
//Obtiene el script que controla la vida
		scriptVida=objetoQueTieneElScriptDeVidas.GetComponent<movimientoPlayer>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
//Condiciones iniciales para la luz
		luz.rectTransform.sizeDelta=new Vector2(0,0);
		luz.color=new Color(1,1,1,0);
		isLighting=false;
		light_f=0;
		numLight=0;
//Calculo del incremento en segundos del canal alpha a 60 cuadros por segundo
		if(duracionSeg>0)
			incremento=0.01666f/duracionSeg;
		else
			isLighting=false;
			luz.enabled=false;
//Condiciones iniciales para el duelo

		flechaDuelo.enabled = false;
		imagenDuelo.enabled = false;
		dueloBueno.enabled=false;
		dueloMalo.enabled=false;
		botonDuelo.SetActive(false);
		balonDuelo.enabled=false;
		contBueno=0;
		contTiempoBueno=0;
		contMalo=0;
		contTiempoMalo=0;
		isDueling=false;

		hasCrashedEnemyToActivateDuel = false;
	}

	void Update () 
	{

//		Debug.Log(light_f);

		if(hasCrashedEnemyToActivateDuel == true)
		{
			Time.timeScale = 0;
			scriptVida.isForcing = true;
		}

		if(scriptVida.isForcing && !isDueling)
		{
			efectoLoco.SetActive(true);
			GarritasCounter.SetActive(false);
			barraVida.SetActive(false);
			isLighting=true;
		}
		if(isLighting)
		{
			IncreaseLight();
		}
		else if(isDueling)
		{
			Dueling();
		}

//		Debug.Log(isLighting + " " + numLight);
//		Debug.Log(light_f);
	}

//Funcion que aumenta poco a poco el canal alpha de 
//la imagen blanca cuando la duracion es mayor a 0 y
//se llama al iniciar el duelo(case 0) y al 
//terminarlo(case 1).
	void IncreaseLight(){
		luz.enabled = true;
		light_f+=incremento;
		luz.color=new Color(1,1,1,light_f);
		if(light_f>1){
			light_f=0;
			luz.color=new Color(1,1,1,light_f);
			luz.enabled=false;
			isLighting=false;
			switch(numLight)
			{
			case 0: //Para entrar al duelo
				imagenDuelo.enabled = true;
				dueloBueno.enabled=true;
				dueloMalo.enabled=true;
				flechaDuelo.enabled = true;
				balonDuelo.enabled=true;
				botonDuelo.SetActive(true);
				isDueling=true;
				numLight=1;
				break;
			case 1: //Para seguir corriendo si se ha ganado
				botonDuelo.SetActive(false);
				efectoLoco.SetActive(false);
				numLight=0;
				scriptVida.SetIsMoving(true);
				scriptVida.isForcing=false;
				GarritasCounter.SetActive(true);
				barraVida.SetActive(true);
				break;
			}
		}
	}
//Funcion que aumenta la barra enemiga poco a poco y
//opone una resistencia a la barra del jugador.
//Cuando gana aparece la luz para correr de nuevo y 
//cuando pierde se debe llamar a la interfaz de pierde(falta implementar).
	void Dueling(){
		if(contBueno>=74){
			dueloBueno.enabled=false;
			dueloMalo.enabled=false;
			balonDuelo.enabled=false;

			imagenDuelo.enabled = false;
			flechaDuelo.enabled = false;
			isDueling=false;
			luz.enabled=true;
			isLighting=true;

			botonDuelo.SetActive(false);

			hasCrashedEnemyToActivateDuel = false;

			Time.timeScale = 1;

			dueloBueno.sprite=imagenesDB[0];
			dueloMalo.sprite=imagenesDM[0];

			contBueno = 0;
			contMalo = 0;
		}
		else{
			contTiempoBueno++;
			contTiempoMalo++;
			if(contTiempoBueno==DELAY_BUENO){ //Delay para bajar la barra del jugador
				contTiempoBueno=0;
				if(contBueno>0 && contBueno<=74){
					contBueno--;
					dueloBueno.sprite=imagenesDB[contBueno];
				}
			}
			if(contTiempoMalo==DELAY_MALO){ //Delay para subir la barra del enemigo
				contMalo++;
				contTiempoMalo=0;
			}
			if(contMalo>=74){
				contMalo=74;
				dueloMalo.sprite=imagenesDM[contMalo];
				isDueling=false;
				imagenDuelo.enabled = false;
				dueloBueno.enabled=false;
				flechaDuelo.enabled = false;
				dueloMalo.enabled=false;
				balonDuelo.enabled=false;
				botonDuelo.SetActive(false);

				GarritasCounter.SetActive(true);
				barraVida.SetActive(true);

				numLight=0;
				scriptVida.isForcing=false;
				////////////////////
				/// PIERDE DUELO ///
				////////////////////

				//gameManager.gameOverTxt.enabled = true;//INSTANCE OF THE GAMEMANAGER SCRIPT SHOWING A GAME OVER.
				//					gameManager.SetHighScore();
//				gameManager.gameOverScreen.SetActive(true);
//				gameManager.pausaButton.SetActive(false);
				player.transform.position = new Vector3(0,0.24559f,0);
				imagePaAtras.SetActive(true);
				efectoLoco.SetActive(false);
				hasCrashedEnemyToActivateDuel = false;

//				Time.timeScale = 0;
//				efectoLoco.SetActive(false);
			}
			dueloMalo.sprite=imagenesDM[contMalo];
		}

	}
//Funcion que incrementa la barra de duelo del
//jugador al presionar el boton
	public void IncreaseBar(){
		contBueno+=3;
		if(contBueno>=74){
			contBueno=74;
		}
		dueloBueno.sprite=imagenesDB[contBueno];
	}
}
