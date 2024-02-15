using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.2f;
    public float damage = 10f;

    private Rigidbody2D rb;
    private float knockbackTimer;

    public Animator animator;
    public PlayerMovement playerMovement;

    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Knockback(Vector2 knockbackDirection)
    {
        if(currentHealth >= 0)
        {
            // Apply knockback force
            rb.velocity = Vector2.zero;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            // Start knockback timer
            knockbackTimer = knockbackDuration;

        }
        else
        {
            rb.isKinematic = true;
        }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            if(col.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            if(col.transform.position.x >= transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }
            col.gameObject.GetComponent<PlayerMovement>().TakeDamage(20);
        }
        
    }

    void Die()
    {
        Debug.Log("Ded");

        // Die animation
        animator.SetBool("isDead", true);

        // Diable enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
