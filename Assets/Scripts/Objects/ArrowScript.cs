using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
	public float speed;
	public Rigidbody2D rigidbody;

	// Start is called before the first frame update
	void Start()
	{

	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("bounds"))
		{
			Destroy(this.gameObject);
		}
	}

	public void Setup(Vector2 velocity, Vector3 direction)
	{
		rigidbody.velocity = velocity.normalized * speed;
		transform.rotation = Quaternion.Euler(direction);
	}
}
