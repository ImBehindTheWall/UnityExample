using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnter : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            animator.SetBool("IsEnter", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger )
        {
            animator.SetBool("IsEnter", false);
        }
    }


    //private IEnumerator openCO()
    //{
    //    animator.SetBool("IsEnter", true);
    //    yield return new WaitForFixedUpdate();
    //}

    //private IEnumerator closeCO()
    //{

    //    animator.SetBool("IsEnter", false);
    //    yield return new WaitForSeconds(.3f);


    //}

}
