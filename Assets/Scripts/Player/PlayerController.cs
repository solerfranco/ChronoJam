using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI dashCounter;

    [Header("Player Properties")]
    [SerializeField]
    protected LayerMask groundLayer;
    [SerializeField]
    protected float jumpForce = 12f;
    [SerializeField]
    protected float speed = 12f;

    private int remainingDashes = 0;
    private int RemainingDashes
    {
        get
        {
            return remainingDashes;
        }
        set
        {
            remainingDashes = value;
            dashCounter.text = "Dash amount: " + value.ToString();
        }
    }
    public AudioSource jumpSound;
    public AudioSource dashSound;
    public AudioSource enemyHitSound;
    private Coroutine freezeCoroutine;
    public GameObject dustParticle;

    private bool IsDashing
    {
        get { return isDashing; }
        set
        {
            isDashing = value;
            anim.SetBool("Dashing", value);
        }
    }
    private bool isDashing;

    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D capsuleCollider;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
    }

    private void Update()
    {
        if (playerInputActions.Player.Movement.WasPressedThisFrame() && CanDash())
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        float inputDirection = GetDirection();
        if(inputDirection != 0) anim.SetFloat("Direction", inputDirection);

        if (IsGrounded())
        {
            rb.velocity = new Vector2(inputDirection * speed, rb.velocity.y);
        }
        else if(!CanDash() && !IsDashing)
        {
             rb.velocity = new Vector2(inputDirection * speed / 1.5f, rb.velocity.y);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (IsGrounded())
            {
                jumpSound.Play();
                anim.SetTrigger("Jump");
                rb.velocity = Vector2.zero;
                rb.velocity = Vector2.up * jumpForce;
            }
            else if(CanDash())
            {
                StartCoroutine(Dash());
            }
        }
    }

    private bool CanDash()
    {
        return !IsDashing && !IsGrounded() && RemainingDashes > 0;
    }

    private bool IsGrounded()
    {
        if (capsuleCollider == null) return false;
        float extraHeight = 0.3f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size - new Vector3(0.1f,0.1f,0), 0, Vector2.down, extraHeight, groundLayer);
        Color rayColor = raycastHit.collider != null ? Color.green : Color.red;
        Debug.DrawRay(capsuleCollider.bounds.center, Vector2.down * (capsuleCollider.bounds.extents.y + extraHeight), rayColor);
        if (raycastHit.collider != null)
        {
            RemainingDashes = 1;
            Combo.instance.ComboMultiplier = 0;
        }
        anim.SetBool("Floor", raycastHit.collider != null);
        return raycastHit.collider != null;
    }

    private IEnumerator Dash()
    {
        Instantiate(dustParticle, transform.position, Quaternion.identity, null);
        dashSound.Play();
        RemainingDashes--;
        IsDashing = true;
        yield return new WaitForSecondsRealtime(0.03f);
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(GetDirection() * (GetJump() ? 1 : 1.3f) * jumpForce, (GetJump() ? 1 : 0) * jumpForce * 1.2f), ForceMode2D.Impulse);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = rb.velocity / 2f;
        yield return new WaitForSeconds(0.1f);
        rb.gravityScale = 3;
        IsDashing = false;
    }

    private bool GetJump()
    {
        return playerInputActions.Player.Jump.IsPressed();
    }

    private float GetDirection()
    {
        return playerInputActions.Player.Movement.ReadValue<Vector2>().x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.collider);
    }

    private void HandleCollision(Collider2D collision)
    {
        if (collision.TryGetComponent(out Mob mob))
        {
            if (!mob.Death())
            {
                RemainingDashes++;
                freezeCoroutine = StartCoroutine(FreezeFrame());
            }
            else
            {
                enemyHitSound.Play();
            }
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

    private void RestoreTime()
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }
}
