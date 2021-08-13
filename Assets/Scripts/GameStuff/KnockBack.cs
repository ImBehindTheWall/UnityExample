using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{

    public float Thrust;
    public float KnockTime;
    public float damage;
    public AudioSource Audio;

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable") && this.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PotScripts>().Smash();
        }
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 different = hit.transform.position - transform.position;
                different = different.normalized * Thrust;
                hit.AddForce(different, ForceMode2D.Impulse);
                if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
                {
                    hit.GetComponent<Enemy>().CurrentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(hit, KnockTime,damage);
                }
                if (collision.gameObject.CompareTag("Player"))
                {
                    if (collision.GetComponent<PlayerMovement>().currentState != PlayerStates.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerStates.stagger;
                      //  Audio.Play();
                        collision.GetComponent<PlayerMovement>().Knock(KnockTime, damage);
                    }
               
                }
               
                
            }
        }
    }
 

}
