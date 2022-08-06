using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        LeanTween.moveY(gameObject, 1.5f, 0.8f).setEaseInOutQuad().setLoopPingPong(-1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(-200 * Time.fixedDeltaTime, rb.velocity.y);
    }
}
