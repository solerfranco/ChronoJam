using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blobl : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject particles;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        rb.velocity = new Vector2(-800 * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void OnDestroy()
    {
        if (particles)
        {
            Instantiate(particles, transform.position, Quaternion.identity, null);
        }
    }
}
