using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour

{
    public bool IsStarted { get; set; }
    public float Life { get; set; }

    private float timePlayed;


    void Update()
    {
        if(IsStarted)
        {
            float life = 100f;
            timePlayed += Time.deltaTime;
            life -= lifeDecreaseRate(timePlayed);
        }

    }

    float lifeDecreaseRate(float timePlayed)
    {
        float decreaseValue = 1;
        if(timePlayed > 20f)
        {
           decreaseValue = 1.5f;
        }
        return decreaseValue;
    }

}
