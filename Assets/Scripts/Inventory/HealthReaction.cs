using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    public FloatValue playerHealth;
    public FloatValue playerMaxHealth;

    public SignalSender healthSignal;

    public void Use(int amountToIncrease)
    {
     
       // playerHealth.RunTimeValue += amountToIncrease;
        playerHealth.RunTimeValue = playerMaxHealth.RunTimeValue * 2;
        healthSignal.Raise();
    }
}
