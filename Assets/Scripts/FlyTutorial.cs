using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTutorial : MonoBehaviour
{
    private GameObject instance;
    public GameObject fly;

    void Start()
    {
        InvokeRepeating(nameof(Check), 1f, 2f);
    }
    void Check()
    {
        if(instance == null)
        {
            instance = Instantiate(fly, transform.position, Quaternion.identity, null);
        }
    }
}
