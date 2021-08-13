using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum PlayerStates
{
	walk,
	attack,
	interact,
	stagger,
	idle
}
public class PlayerMovement : MonoBehaviour
{
	public PlayerStates currentState;
	public float speed;
	//delete new
	private new Rigidbody2D rigidbody;
	private Vector3 change;
	private Animator animator;
	public FloatValue currentHealth;
	public SignalSender PlayerHealthSignal;
	public VectorValue startigPosition;
	public Inventory playerInventory;
	public SpriteRenderer reciveSpriteItem;
	public PlayerCurrentPosition currentPosition;
	public SignalSender playerHit;
	public GameObject deathEffect;
	private float DeathDelay = 1f;
	public GameObject projectile;
	public GameObject DeadPanel;

	public AudioSource audioSource;
	public AudioSource walkSource;
	public AudioClip walkSound;

	[Header("Iframe")]
	public Color flashColor;
	public Color regularColor;
	public float flashDuration;
	public int FlashCount;
	public Collider2D triggerCollider;
	public SpriteRenderer spriteRenderer;



	// Start is called before the first frame update
	void Start()
	{

		SceneTransition sceneTransition = new SceneTransition();
		currentState = PlayerStates.walk;
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
		animator.SetFloat("MoveX", 0);
		animator.SetFloat("MoveY", -1);
		transform.position = startigPosition.initialValue;
		// transform.position = currentPosition.current;

	}
	private void DeathEffect()
	{
		if (deathEffect != null)
		{
			GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
			DeadPanel.SetActive(true);
			Destroy(effect, DeathDelay);
		}
	}



	// Update is called once per frame
	void Update()
	{
		if (currentState == PlayerStates.interact)
		{
			return;
		}
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");
		Vector3 inputDir = new Vector3(change.x, change.y).normalized;
		float inputAngel = (Mathf.Atan2(inputDir.x, inputDir.y)) * Mathf.Rad2Deg;
		Debug.DrawRay(transform.position, inputDir * 3, Color.white);


		if (Input.GetButtonDown("attack") && currentState != PlayerStates.attack && currentState != PlayerStates.stagger)
		{
			StartCoroutine(attackCO());
		}
		else if (Input.GetButtonDown("SecondWeapon") && currentState != PlayerStates.attack && currentState != PlayerStates.stagger)
		{

			StartCoroutine(secondAttackCO());
		}
		else if (currentState == PlayerStates.walk || currentState == PlayerStates.idle)
		{
			UpdateAnimation();

			//currentPosition.current = rigidbody.position;
		}

	}

	private IEnumerator attackCO()
	{
		animator.SetBool("Attacking", true);
		audioSource.Play();
		currentState = PlayerStates.attack;
		yield return null;
		animator.SetBool("Attacking", false);
		yield return new WaitForSeconds(.3f);
		if (currentState != PlayerStates.interact)
		{
			currentState = PlayerStates.walk;
		}
	}
	private IEnumerator secondAttackCO()
	{
		//animator.SetBool("Attacking", true);
		currentState = PlayerStates.attack;
		yield return null;
		MakeArrow();
		//animator.SetBool("Attacking", false);
		yield return new WaitForSeconds(.3f);
		if (currentState != PlayerStates.interact)
		{
			currentState = PlayerStates.walk;
		}
	}

	private void MakeArrow()
	{
		Vector2 temp = new Vector2(animator.GetFloat("MoveX"),animator.GetFloat("MoveY"));
		ArrowScript arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<ArrowScript>();
		arrow.Setup(temp, ChooseArrowDirection());
	}

	Vector3 ChooseArrowDirection()
	{
		//Vector3 inputDir = new Vector3(change.x, change.y).normalized;

		//float inputAngel = (Mathf.Atan2(inputDir.x, inputDir.y)) * Mathf.Rad2Deg;

		float temp = Mathf.Atan2(animator.GetFloat("MoveY"), animator.GetFloat("MoveX"))* Mathf.Rad2Deg;
		return new Vector3(0,0, temp);
		

	}

	public void RaiseItem()
	{
		if (playerInventory.currentItem != null)
		{
			if (currentState != PlayerStates.interact)
			{
				animator.SetBool("ReceiveItem", true);
				currentState = PlayerStates.interact;
				reciveSpriteItem.sprite = playerInventory.currentItem.itemSprite;
			}
			else
			{
				animator.SetBool("ReceiveItem", false);
				currentState = PlayerStates.idle;
				reciveSpriteItem.sprite = null;
				playerInventory.currentItem = null;

			}
		}


	}

	void UpdateAnimation()
	{
		if (change != Vector3.zero)
		{
			MoveCharacter();
			animator.SetFloat("MoveX", change.x);
			animator.SetFloat("MoveY", change.y);
			animator.SetBool("Moving", true);
			


		}
		else
		{
			animator.SetBool("Moving", false);
		}

	}

	void MoveCharacter()
	{
		change.Normalize();
		rigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
	}

	

	public void Knock(float KnockTime, float damage)
	{
		currentHealth.RunTimeValue -= damage;
		PlayerHealthSignal.Raise();

		if (currentHealth.RunTimeValue > 0)
		{
			StartCoroutine(KnockCo(KnockTime));

		}
		else
		{
			DeathEffect();
			this.gameObject.SetActive(false);
		}
	}

	private IEnumerator KnockCo(float KnockTime)
	{
		playerHit.Raise();
		if (rigidbody != null)
		{
			StartCoroutine(FlashCo());
			yield return new WaitForSeconds(KnockTime);
			rigidbody.velocity = Vector2.zero;
			rigidbody.GetComponent<PlayerMovement>().currentState = PlayerStates.idle;
			rigidbody.velocity = Vector2.zero;


		}
	}

	private IEnumerator FlashCo()
	{
		int temp = 0;
		triggerCollider.enabled = false;
		while (temp < FlashCount)
		{
			spriteRenderer.color = flashColor;
			yield return new WaitForSeconds(flashDuration);
			spriteRenderer.color = regularColor;
			yield return new WaitForSeconds(flashDuration);
			temp++;
		}
		triggerCollider.enabled = true;
	}
}
 