using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Coin : PowerUp
{
	public Inventory playerInventory;
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !collision.isTrigger)
		{
			playerInventory.coin += 1;
			powerUpSignal.Raise();
			Destroy(this.gameObject);
		}
	}
}
