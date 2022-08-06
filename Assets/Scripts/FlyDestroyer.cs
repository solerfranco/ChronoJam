using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fly"))
        {
            Destroy(collision.gameObject);
        }
    }
}
