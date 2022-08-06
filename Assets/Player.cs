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
    public int dashLimit;
    private int currentDash;
    private Coroutine freezeCoroutine;
    private bool dashing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -13f, 13f), Mathf.Clamp(transform.position.y, -10, 7.3f), 0);
        direction = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.W) && onFloor)
        {
            jumpInput = true;
        }

        if (!onFloor)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                CallDash(Vector2.up);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                CallDash(Vector2.left);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                CallDash(Vector2.right);
            }
        }
    }

    private void CallDash(Vector2 direction)
    {
        if (dashing) return;
        RestoreTime();
        Time.timeScale = 1;
        if (currentDash < dashLimit)
        {
            rb.velocity = new Vector2(0, 0);
            StartCoroutine(Dash());
        }

    }

    private void RestoreTime()
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    private void FixedUpdate()
    {
        if (onFloor)
        {
            rb.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, rb.velocity.y);
            resetDashLimit();
        }
        if (jumpInput)
        {
            LeanTween.scaleX(gameObject, 0.65f, 0.05f).setLoopPingPong(1);
            //rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpInput = false;
            onFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            RestoreTime();
            resetDashLimit();
            onFloor = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fly"))
        {
            currentDash--;
            Destroy(collision.gameObject);
            freezeCoroutine = StartCoroutine(FreezeFrame());
        }
    }

    private IEnumerator FreezeFrame()
    {
        Time.timeScale = 0.4f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    private IEnumerator Dash()
    {
        dashing = true;
        yield return new WaitForSecondsRealtime(0.02f);
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetKey(KeyCode.W) ? 1 : 0).normalized * jumpForce * 1.2f, ForceMode2D.Impulse);
        currentDash++;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.3f);
        rb.velocity = rb.velocity/2;
        yield return new WaitForSeconds(0.1f);
        rb.gravityScale = 3;
        dashing = false;
    }

    private void resetDashLimit()
    {
        //TODO: agregar logica de seteo de dash
        currentDash = 0;
    }
}

//Dash diagonal
//Multiples generadores de moscas

//El tiempo queda congelado
//En las paredes te quedas trabado
