using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[Header("Position variables")]
	public Transform target;
	public float Smoothing;
	public Vector2 maxPosition;
	public Vector2 minPosition;

	public Animator animator;

	[Header("Position reset")]
	public VectorValue camMin;
	public VectorValue camMax;

	// Start is called before the first frame update
	void Start()
	{
		maxPosition = camMax.initialValue;
		minPosition = camMin.initialValue;
		animator = GetComponent<Animator>();
		transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
	}

	// Update is called once per frame
	void LateUpdate()
	{
		if (transform.position != target.position)
		{
			Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

			targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
			targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

			transform.position = Vector3.Lerp(transform.position,
				targetPosition, Smoothing);
		}
	}

	public void ScreenKick()
	{
		animator.SetBool("KickActive", true);
		StartCoroutine(KickCo());
	}

	public IEnumerator KickCo()
	{
		yield return null;
		animator.SetBool("KickActive", false);
	}
}
