using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour 
{
	public GameObject door;
	public float gracePeriod = 0.0f;
	public bool startsOpen = false;

	private bool isOpen;
	private float timeClosed;

	void Start()
	{
		isOpen = startsOpen;
		timeClosed = -gracePeriod;
	}

	void Update()
	{
		if (Time.time - timeClosed <= gracePeriod) 
		{
			door.SetActive (isOpen);
		}
		else
		{
			door.SetActive (!isOpen);
		}

	}

	void OnTriggerStay2D()
	{
		isOpen = !startsOpen;
	}
	
		void OnTriggerExit2D()
	{
		isOpen = startsOpen;
		timeClosed = Time.time;
	}

}
