using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
	private static GameStateManager instance;
	
	public static Texture UserTexture;
	public static Texture FriendTexture = null;

	//	private int highScore;
	
	private string username = null;
	
	public static GameStateManager Instance { 
		get 
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<GameStateManager>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(instance.gameObject);
			}

			return current(); 
		} 
	}
	
	public static string Username
	{
		get { return Instance.username; }
		set { Instance.username = value; }
	}
	
	delegate GameStateManager InstanceStep();
	
	static InstanceStep init = delegate()
	{
		GameObject container = new GameObject("GameStateManager");
		instance = container.AddComponent<GameStateManager>();
		current = then;
		return instance;
	};

	static InstanceStep then = delegate() { return instance; };
	static InstanceStep current = init;
	
	void Start()
	{
		Time.timeScale = 1.0f;
	}
	
	public void StartGame()
	{
		Start();
	}

	void Awake()
	{
		if(instance == null)
		{
			//If I am the first instance, make me the Singleton
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != instance)
				Destroy(this.gameObject);
		}
	}
}
