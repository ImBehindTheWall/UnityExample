using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
	public GameObject WinPanel;
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !collision.isTrigger)
		{
			WinPanel.SetActive(true);
			Time.timeScale = 0;
		}
	}
}
