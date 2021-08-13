using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
	idle,
	walk,
	attack,
	stagger
}
public class Enemy : MonoBehaviour
{
	[Header("State Machine")]
	public EnemyState CurrentState;

	[Header("enemyStats")]
	public FloatValue maxHealth;
	public float Health;
	public string EnemyName;
	public int baseAttack;
	public float MoveSpeed;

	[Header("Death Effect")]
	public GameObject deathEffect;
	private float DeathDelay = 1f;
	public LootTable thisLoot;

	private void Awake()
	{
		Health = maxHealth.initialValue;
	}
	private void TakeDamage(float damage)
	{
		Health -= damage;
		if (Health <= 0)
		{
			DeathEffect();
			MakeLoot();
			this.gameObject.SetActive(false);
		}
	}

	private void MakeLoot()
	{
		if (thisLoot!=null)
		{
			PowerUp current = thisLoot.lootPowerUp();
			if (current != null)
			{
				Instantiate(current.gameObject, transform.position, Quaternion.identity);
			}
		}
	}
	private void DeathEffect()
	{
		if (deathEffect != null)
		{
			GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
			Destroy(effect, DeathDelay);
		}
	}
	public void Knock(Rigidbody2D rigidbody, float KnockTime, float damage)
	{
		StartCoroutine(KnockCo(rigidbody, KnockTime));
		TakeDamage(damage);
	}
	private IEnumerator KnockCo(Rigidbody2D rigidbody, float KnockTime)
	{
		if (rigidbody != null)
		{
			yield return new WaitForSeconds(KnockTime);
			rigidbody.velocity = Vector2.zero;
			CurrentState = EnemyState.idle;
			rigidbody.velocity = Vector2.zero;
		}
	}
}
