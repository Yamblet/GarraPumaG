using UnityEngine;
using System.Collections;

public class AnimationUV : MonoBehaviour {

	private bool isVertical = true;
	public float scrollSpeed = 10;

	void Update () {  
		float offset =  TimeController.deltaTime * -scrollSpeed;  
		if (isVertical) 
		{  
			renderer.material.mainTextureOffset = new Vector2 (0, offset);  
		}else
		{  
			renderer.material.mainTextureOffset = new Vector2 (offset, 0);  
		}  
	}  
}
