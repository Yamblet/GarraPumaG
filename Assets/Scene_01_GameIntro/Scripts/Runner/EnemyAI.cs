using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public Transform playerPos; //Player Position
	public float damping = 140f; // velocidad de giro

	private float distanceToPlayer = 0; //Distance Player
 	private float lookingThePlayerDotProduct = 0; // variable dot product 
	public float destinationDistance = 11.1f; // Distance when the movement of enemies activate 
	public float DistanceToSweep = 5.09f;

	public float impulso; // force used to go towards the player

//	private Color connectionDistanceColor; //Just the ray COlor for debugging the distance between player and enemy

	private Quaternion targetRotation; //Store the targert rotation

	public Animator anim;

	public bool isTurning;

	void Start () 
	{
//		Debug.Log("Is FB logged in?" + FB.IsLoggedIn);
		isTurning = true;
	}
	
	void FixedUpdate ()
	{
		if(DistanceToPlayer() <=  destinationDistance && DistanceToPlayer() >= 0)
		{
			anim.SetBool("IsComing", true);
			anim.SetBool("IsNear", false);
			transform.Translate(Vector3.forward * impulso * Time.deltaTime);

			if(DistanceToPlayer() <=  DistanceToSweep && DistanceToPlayer() >= 0)
			{
				anim.SetBool("IsNear", true);
				anim.SetBool("IsComing", false);
				transform.Translate(Vector3.forward * impulso * Time.deltaTime);
				isTurning = false;
				impulso = 26f;
			}
		}

		
		if(LookingDirectionToPlayer() == true && AnimacionesCamara.actual.animacion == false && isTurning == true)
		{
			LookAtPlayerAndMove();
		}
	}

	float DistanceToPlayer()
	{
		distanceToPlayer = Vector3.Distance(transform.position, playerPos.position);
		return distanceToPlayer;
	}

	bool LookingDirectionToPlayer()
	{
		bool isLooking = true;

		Vector3 forward = transform.forward.normalized;
		Vector3 toOther = (playerPos.position - transform.position);

		toOther.Normalize();

		Debug.DrawRay(transform.position, forward,Color.red);
		Debug.DrawRay(transform.position, toOther,Color.green);

		lookingThePlayerDotProduct = Vector3.Dot(forward, toOther);


		if(lookingThePlayerDotProduct > 0)
		{
			return isLooking;
		}
		
		if(lookingThePlayerDotProduct < -1)
		{
//			Debug.Log("Se destruyo por producto punto");
			Destroy(this.gameObject);
			return !isLooking;
		}
		return false;
	}

	void LookAtPlayerAndMove()
	{
		if(DistanceToPlayer() <=  destinationDistance && DistanceToPlayer() >= 0)
		{
			Vector3 destDir = playerPos.position - transform.position;
			destDir.y = 0;
			targetRotation = Quaternion.LookRotation(destDir);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, damping * Time.fixedDeltaTime);
		}

	}
}
