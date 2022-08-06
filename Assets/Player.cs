using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;

    private bool jumpInput;
    private float direction;
    public bool onFloor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -14.6f, 14.6f), transform.position.y, 0);
        direction = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.W) && onFloor)
        {
            jumpInput = true;
        }

        if (!onFloor)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = new Vector2(0, 0);
                StartCoroutine(Dash());
                rb.AddForce(Vector2.up * jumpForce * 1.2f, ForceMode2D.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                rb.velocity = new Vector2(0, 0);
                StartCoroutine(Dash());
                rb.AddForce(Vector2.left * jumpForce * 1.2f, ForceMode2D.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                rb.velocity = new Vector2(0, 0);
                StartCoroutine(Dash());
                rb.AddForce(Vector2.right * jumpForce * 1.2f, ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        if (onFloor)
        {
            rb.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, rb.velocity.y);
        }
        if (jumpInput)
        {
            LeanTween.scaleX(gameObject, 0.65f, 0.05f).setLoopPingPong(1);
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpInput = false;
            onFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fly"))
        {
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator Dash()
    {
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = 3;
    }
}
