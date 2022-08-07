using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fly") || collision.gameObject.CompareTag("Bee"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fly") || collision.gameObject.CompareTag("Bee"))
        {
            Destroy(collision.gameObject);
        }
    }
}
