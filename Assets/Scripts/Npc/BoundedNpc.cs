using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedNpc : Interactable
{
    private Vector3 direktionVector;
    private Transform transform;
    public float speed;
    private Rigidbody2D rigidbody;
    private Animator animator;
    public Collider2D bounds;
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        ChangeDirektion();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 temp = transform.position + direktionVector * speed * Time.deltaTime;
        if (bounds.bounds.Contains(temp))
        {
            rigidbody.MovePosition(temp);

        }
        else
        {
            ChangeDirektion();
        }

    }

    void ChangeDirektion()
    {
        int direktion = Random.Range(0, 4);
        switch (direktion)
        {
            case 0:
                //walking right
                direktionVector = Vector3.right;
                break;
            case 1:
                //walking up
                direktionVector = Vector3.up;
                break;
            case 2:
                //walking left
                direktionVector = Vector3.left;
                break;
            case 3:
                //walking down
                direktionVector = Vector3.down;
                break;
            default:
                break;
        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        animator.SetFloat("MoveX", direktionVector.x);
        animator.SetFloat("MoveY", direktionVector.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 temp = direktionVector;
        ChangeDirektion();
        int loops = 0;
        while (temp == direktionVector && loops < 100)
        {
            ChangeDirektion();
        }
    }

   
      
}
