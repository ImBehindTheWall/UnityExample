using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartcontainer : PowerUp
{
    public FloatValue heartContanerz;
    public FloatValue playerHealth;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (heartContanerz.RunTimeValue<5)
            {
                heartContanerz.RunTimeValue += 1;
                playerHealth.RunTimeValue = heartContanerz.RunTimeValue * 2;
                powerUpSignal.Raise();
            }
      
            Destroy(this.gameObject);
        }
    }
}
