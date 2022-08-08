using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Combo combo;
    public float speed;
    public float jumpForce;
    private Animator anim;
    public AudioSource audioSourceJump;
    public AudioSource audioSourceDash;
    public AudioSource audioSourceHit;
    public LifeCounter lifeCounter;


    public int[] comboThreshold;
    private bool jumpInput;
    private float direction;
    public bool onFloor;
    private int dashesRemaning;
    private Coroutine freezeCoroutine;
    private bool dashing;
    private Vector2 velocity;
    public GameObject dustParticle;
    public TextMeshProUGUI dashCounter;
    private int DashesRemaning
    {
        get
        {
            return dashesRemaning;
        }
        set
        {
            dashesRemaning = value;
            dashCounter.text = "Dashes Remaning: " + value.ToString();
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        anim.SetTrigger("Walk");
        combo = Combo.instance;
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
        if (DashesRemaning > 0)
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
            // rb.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, rb.velocity.y);
            if (combo)
            {
                combo.ResetCombo();
            }
            DashesRemaning = 1;
        }
        else if(!dashing)
        {
            rb.velocity = new Vector2(direction * speed / 1.5f * Time.fixedDeltaTime, rb.velocity.y);
        }
        if (jumpInput)
        {
            anim.SetBool("Floor", false);
            //LeanTween.scaleX(gameObject, 0.65f, 0.05f).setLoopPingPong(1);
            //rb.velocity = Vector2.zero;
            anim.SetTrigger("Jump");
            audioSourceJump.Play();
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
            anim.SetBool("Floor", true);
        }

        if (collision.gameObject.CompareTag("Wall") || dashing)
        {
            RestoreTime();
            rb.AddForce(new Vector2(-velocity.x, velocity.y), ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Bee"))
        {
            audioSourceHit.Play();
            lifeCounter.DecreaseLife(collision.gameObject.tag);
            Destroy(collision.gameObject);
            //freezeCoroutine = StartCoroutine(FreezeFrame());
        }
        if (combo)
        {
            combo?.IncrementComboByEnemyType(collision.gameObject.tag);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fly"))
        {
            lifeCounter?.IncreaseLife();
            dashRefill(combo ? combo.ComboMultiplier : 1);
            Destroy(collision.gameObject);
            freezeCoroutine = StartCoroutine(FreezeFrame());
        }
        if (collision.gameObject.CompareTag("Bee"))
        {
            audioSourceHit.Play();
            lifeCounter.DecreaseLife(collision.gameObject.tag);
            Destroy(collision.gameObject);
            //freezeCoroutine = StartCoroutine(FreezeFrame());
        }
        if(collision.gameObject.CompareTag("LaunchBox"))
        {
            SceneManager.LoadScene(2);
        }
        if (combo)
        {
            combo.IncrementComboByEnemyType(collision.gameObject.tag);
        }
    }

    private void CheckColor()
    {
        switch (DashesRemaning)
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
        Instantiate(dustParticle, transform.position, Quaternion.identity, null);
        anim.SetBool("Dashing", true);
        dashing = true;
        yield return new WaitForSecondsRealtime(0.03f);
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ? 1 : 1.3f), (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ? 1 : 0) * jumpForce * 1.2f, ForceMode2D.Impulse);
        audioSourceDash.Play();
        DashesRemaning--;
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
        DashesRemaning = 0;
    }

    void dashRefill(int combo)
    {
        DashesRemaning++;
        if(combo > comboThreshold[0] && combo <= comboThreshold[1])
        {
            DashesRemaning = 2;
        }
        else if (combo > comboThreshold[1])
        {
            DashesRemaning = 3;
        }
    }
}

//Dash diagonal
//Multiples generadores de moscas

//El tiempo queda congelado
//En las paredes te quedas trabado
