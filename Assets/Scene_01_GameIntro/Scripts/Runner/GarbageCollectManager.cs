using UnityEngine;
using System.Collections;

public class GarbageCollectManager : MonoBehaviour {
	 
	void Update()  
	{  
		if (Time.frameCount % 40 == 0)
		{
			System.GC.Collect();
		}  
	}
}