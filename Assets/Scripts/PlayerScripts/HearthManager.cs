using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HearthManager : MonoBehaviour
{

    public Image[] hearts;
    public Sprite FullHearth;
    public Sprite HalfHearth;
    public Sprite EmptyHearth;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHearts;

    // Start is called before the first frame update
    void Start()
    {
        InitHearth();
    }
    void Update()
    {
        UpdateHearts();
    }
    public void InitHearth()
    {
        for (int i = 0; i < heartContainers.RunTimeValue; i++)
        {
            if (i<hearts.Length)
            {
                hearts[i].gameObject.SetActive(true);
                hearts[i].sprite = FullHearth;
            }
         

        }
    }

    public void UpdateHearts()
    {
        InitHearth();
        float temphealth = playerCurrentHearts.RunTimeValue / 2;
        for (int i = 0; i < heartContainers.RunTimeValue; i++)
        {
            if (i<=temphealth-1)
            {
                //full hearth
                hearts[i].sprite = FullHearth;
            }
            else if(i>= temphealth)
            {
                //empty heart
                hearts[i].sprite = EmptyHearth;

            }
            else
            {
                //half full hearth
                hearts[i].sprite = HalfHearth;

            }
        }
     
    }

}
