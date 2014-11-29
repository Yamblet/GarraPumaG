using UnityEngine;
using System.Collections;

public class TouchLogical : MonoBehaviour {

	public static int currentTouch = 0;
	[HideInInspector]
	public int touchToWatch = 64;
	public Touch touch;

	//Virtual methods for anywhere
	public virtual void OnTouchBeganAnywhere(){}
	public virtual void OnTouchEndedAnywhere(){}
	public virtual void OnTouchMovedAnywhere(){}
	public virtual void OnTouchStationaryAnywhere(){}
	// Update is called once per frame
	public void CheckTouches () {
		if(Input.touchCount == 0){
			//There are no touches on screen
			
		}
		else{
			//Do something with all the touches on screen
			for(int i=0; i<Input.touchCount; i++){
				currentTouch = i;
				//Gui texture touch
				touch=Input.GetTouch(i);
				//Anywhere touch
				if(Input.GetTouch(i).phase == TouchPhase.Began){
					//call function "OnTouchBeganAnywhewre"
					OnTouchBeganAnywhere();
				}
				//Touch moved
				if(Input.GetTouch(i).phase == TouchPhase.Moved){
					//call function "OnTouchMovedAnywhewre"
					OnTouchMovedAnywhere();
				}
				
				//Touch stationary
				if(Input.GetTouch(i).phase == TouchPhase.Stationary){
					//call function "OnTouchStationaryAnywhewre"
					OnTouchStationaryAnywhere();
				}
				
				//End touch
				if(Input.GetTouch(i).phase == TouchPhase.Ended){
					//call function "OnTouchEndedAnywhewre"
					OnTouchEndedAnywhere();
				}	
			}
		}
	}
}
