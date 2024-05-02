using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float jumpForce;
	[SerializeField] private float moveSpeed;
	[SerializeField] private AudioClip flySound;
	private Rigidbody2D rb2d;
	private AudioSource audioSource;
	private Vector3 pos1 = new Vector3(-3.5f, -1.5f);
	private Vector3 pos2 = new Vector3(-3.5f, -0.5f);

	// Use this for initialization
	void Start () 
	{
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}
	
	void FixedUpdate () 
	{
		if(Manager.Instance.FirstStart)
		{
			transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time, 1.0f));
		}
		if(!Manager.Instance.GameOver)
		{

			rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);

			#if UNITY_EDITOR || UNITY_STANDALONE
			if (Input.GetAxis("Jump") > 0)
			{
				rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
				//audioSource.PlayOneShot(flySound);
			}

			#elif UNITY_ANDROID
			if(Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				if(touch.phase == TouchPhase.Stationary)
				{
					// user is touching screen. 
					rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
					//audioSource.PlayOneShot(flySound);
				}
			}
			#endif
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Ground")
		{
			// Game Over!
			Debug.Log("Game over!");
			Manager.Instance.GameEnded();
			rb2d.Sleep();
		}
	}
}
