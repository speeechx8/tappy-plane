using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	[SerializeField] private AudioClip coinSound;
	private AudioSource audioSource;

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			audioSource.PlayOneShot(coinSound);
			Manager.Instance.Score += 1;
			Manager.Instance.UpdateScore();
			Debug.Log(Manager.Instance.Score);
			Destroy(gameObject);
		}
	}
}
