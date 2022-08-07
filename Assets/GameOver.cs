using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveLocalY(gameObject, 0, 1).setEaseOutBounce();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
