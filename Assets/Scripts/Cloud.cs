using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public bool destroy = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(DestroyCloud), 25f);
    }

    void DestroyCloud()
    {
        if (destroy)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }
}
