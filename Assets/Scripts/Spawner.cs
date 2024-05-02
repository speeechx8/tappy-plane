using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField] private GameObject spawnObj;
	[SerializeField] private float spawnDelay;
	private bool spawnStarted;

	void Start()
	{
		spawnStarted = false;
	}

	void Update () 
	{
		if(!Manager.Instance.GameOver)
		{
			if(!spawnStarted)
			{
				InvokeRepeating("Spawn", 1, spawnDelay);
				spawnStarted = true;
			}
		}
		else if(Manager.Instance.GameOver)
		{
			CancelInvoke();
			spawnStarted = false;
		}
	}
	
	private void Spawn()
	{
		Instantiate(spawnObj, transform.position, Quaternion.identity);
	}
}
