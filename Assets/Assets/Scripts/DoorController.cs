using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	public int  numberOfTriggers = 1;
	public float gracePeriod = 0.0f;
	public bool startsOpen = false;

	private List<int> curTriggeredIDs;
	private bool isOpen;
	private float timeClosed;

	void Start()
	{
		curTriggeredIDs = new List<int> ();
		isOpen = startsOpen;
		timeClosed = -gracePeriod;
	}

	void SetDoorState(bool doorState)
	{
		SpriteRenderer rend = GetComponent<SpriteRenderer> ();
		rend.enabled = doorState;
		BoxCollider2D box = GetComponent<BoxCollider2D> ();
		box.enabled = doorState;
	}

	void Update()
	{
		if (Time.time - timeClosed <= gracePeriod) 
		{
			SetDoorState (isOpen);
		} 
		else 
		{
			SetDoorState (!isOpen);
		}
	}

	public void Trigger(int id)
	{
		if (!curTriggeredIDs.Contains (id)) 
		{
			curTriggeredIDs.Add (id);
		}
		if (curTriggeredIDs.Count >= numberOfTriggers) 
		{
			isOpen = !startsOpen;
		} 
	}

	public void UnTrigger(int id)
	{
		curTriggeredIDs.Remove(id);

		if (curTriggeredIDs.Count < numberOfTriggers) 
		{
			isOpen = startsOpen;
		} 
		timeClosed = Time.time;
	}

}
