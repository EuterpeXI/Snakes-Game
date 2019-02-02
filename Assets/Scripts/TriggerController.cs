using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour 
{
	public DoorController door;

	void OnTriggerStay2D()
	{
		door.Trigger(GetInstanceID());
	}
	
	void OnTriggerExit2D()
	{
		door.UnTrigger(GetInstanceID());
	}

}
