using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = -300;
    public GameObject deathParticles;
    public int damage;
    public bool isEnemy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    public bool Death()
    {
        if (deathParticles)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity, null);
        }
        Destroy(gameObject);
        LifeCounter.instance.Life -= damage;
        if (isEnemy)
        {
            Combo.instance.ComboMultiplier = 0;
        }
        else
        {
            Combo.instance.ComboMultiplier++;
        }
        return isEnemy;
    }
}
