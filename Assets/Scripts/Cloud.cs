using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Sprite[] variants;
    private SpriteRenderer spriteRend;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = variants[Random.Range(0, variants.Length)];
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(-300 * Time.fixedDeltaTime, rb.velocity.y);
    }
}
