using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour 
{
	public GameObject door;
	
	void OnTriggerStay2D()
	{
			door.SetActive(false);
	}
	
		void OnTriggerExit2D()
	{
			door.SetActive(true);
	}

}
