using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	[Header("New Scene variables")]
	public string sceneToLoad;
	public Vector2 playerPosition;
	public VectorValue playerStorage;
	public Vector2 cameraNewmin;
	public Vector2 cameraNewMax;
	public VectorValue cameraMin;
	public VectorValue cameraMax;

	[Header("Transition variables")]
	public GameObject fadePanel;
	public GameObject fadeOutPanel;
	public float FadeWait;


	// public PlayerCurrentPosition currentPosition;
	//public PlayerMovement movement = new PlayerMovement();

	private void Awake()
	{

		if (fadePanel != null)
		{
			GameObject panel = Instantiate(fadePanel, Vector3.zero, Quaternion.identity) as GameObject;
			Destroy(panel, 1);
		}
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !collision.isTrigger)
		{
			playerStorage.initialValue = playerPosition;
			StartCoroutine(FadeCo());
			//SceneManager.LoadScene(sceneToLoad);
		}
	}

	public IEnumerator FadeCo()
	{
		if (fadeOutPanel = null)
		{
			Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
		}
		yield return new WaitForSeconds(FadeWait);//delete
		ResetCamerBounds();
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
		while (!asyncOperation.isDone)
		{
			yield return null;
		}
	}

	public void ResetCamerBounds()
	{
		cameraMax.initialValue = cameraNewMax;
		cameraMin.initialValue = cameraNewmin;
	}
}
