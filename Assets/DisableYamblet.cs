using UnityEngine;
using System.Collections;

public class DisableYamblet : MonoBehaviour {

	public GameObject yambletLogo;

	public void BoomYamblet()
	{
		yambletLogo.SetActive(false);
	}
}
