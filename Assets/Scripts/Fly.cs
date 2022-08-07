using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = -300;
    public GameObject particles;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        LeanTween.moveY(gameObject, Random.Range(0.5f, 1.5f), 0.8f).setEaseInOutQuad().setLoopPingPong(-1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void OnDestroy()
    {
        if (particles)
        {
            Instantiate(particles, transform.position, Quaternion.identity, null);
        }
    }
}
