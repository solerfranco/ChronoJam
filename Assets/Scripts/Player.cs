using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private Animator anim;

    private bool jumpInput;
    private float direction;
    public bool onFloor;
    public int dashLimit;
    private int CurrentDash
    {
        get
        {
            return currentDash;
        }
        set
        {
            currentDash = value;
            //CheckColor();
        }
    }
    private int currentDash;
    private Coroutine freezeCoroutine;
    private bool dashing;
    private Vector2 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        anim.SetTrigger("Walk");
    }

    void Update()
    {
        if (rb.velocity != Vector2.zero) velocity = rb.velocity;
        direction = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Direction", direction);
        if (Input.GetKeyDown(KeyCode.W) && onFloor)
        {
            jumpInput = true;
        }

        if (!onFloor)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
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
        if (CurrentDash < dashLimit)
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
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -13f, 13f), Mathf.Clamp(transform.position.y, -10, 7.3f), 0);
        if (onFloor)
        {
            rb.velocity = new Vector2(direction * (direction < 0 ? speed : speed / 1.5f) * Time.fixedDeltaTime, rb.velocity.y);
        }
        else if(!dashing)
        {
            rb.velocity = new Vector2(direction * speed / 1.5f * Time.fixedDeltaTime, rb.velocity.y);
        }
        if (jumpInput)
        {
            //LeanTween.scaleX(gameObject, 0.65f, 0.05f).setLoopPingPong(1);
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
            ResetDashLimit();
            onFloor = true;
        }

        if (collision.gameObject.CompareTag("Wall") || dashing)
        {
            RestoreTime();
            rb.AddForce(new Vector2(-velocity.x, velocity.y), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fly"))
        {
            CurrentDash--;
            Destroy(collision.gameObject);
            freezeCoroutine = StartCoroutine(FreezeFrame());
        }
    }

    private void CheckColor()
    {
        switch (dashLimit - CurrentDash)
        {
            case 0:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 1:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case 2:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            default:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
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
        anim.SetBool("Dashing", true);
        dashing = true;
        yield return new WaitForSecondsRealtime(0.03f);
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ? 1 : 0).normalized * jumpForce * 1.5f, ForceMode2D.Impulse);
        CurrentDash++;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = rb.velocity/1.3f;
        yield return new WaitForSeconds(0.1f);
        rb.gravityScale = 3;
        dashing = false;
        anim.SetBool("Dashing", false);
    }

    private void ResetDashLimit()
    {
        //TODO: agregar logica de seteo de dash
        CurrentDash = 0;
    }
}

//Dash diagonal
//Multiples generadores de moscas

//El tiempo queda congelado
//En las paredes te quedas trabado
