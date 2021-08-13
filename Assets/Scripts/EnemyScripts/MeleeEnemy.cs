using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : LOgBehavior
{
	public AudioSource Audio;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		CheckDistance();
	}

	public override void CheckDistance()
	{
		if (Vector3.Distance(target.position, transform.position) < ChaseRadius && Vector3.Distance(target.position, transform.position) > AttackRadius)
		{
			if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk && CurrentState != EnemyState.attack && CurrentState != EnemyState.stagger)
			{
				animator.SetBool("Mooving", true);
				Vector3 temp = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
				changeAnim(temp - transform.position);
				rigidbody.MovePosition(temp);
				ChangeState(EnemyState.walk);
			}
		}
		else if (Vector3.Distance(target.position, transform.position) < ChaseRadius && Vector3.Distance(target.position, transform.position) < AttackRadius)
		{
			if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk && CurrentState != EnemyState.attack && CurrentState != EnemyState.stagger)
			{
				Audio.Play();
				StartCoroutine(AttackCo());
			}
		}
		else
		{
			if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk && CurrentState != EnemyState.attack && CurrentState != EnemyState.stagger)
			{
				animator.SetBool("Mooving", false);
				Vector3 temp = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);

				changeAnim(temp - transform.position);
			}
		}
	}

	public IEnumerator AttackCo()
	{
		animator.SetBool("Mooving", false);

		animator.SetBool("attack", true);
		CurrentState = EnemyState.attack;
		yield return null;
		yield return new WaitForSeconds(.6f);

		animator.SetBool("attack", false);

		CurrentState = EnemyState.idle;
	}
}
