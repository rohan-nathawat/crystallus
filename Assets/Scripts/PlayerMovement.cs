using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq.Expressions;

public class PlayerMovement : MonoBehaviour
{

    private GameManager gm;
    //public ParticleSystem dust;
    
    public Animator animator;

    private float horizontal;
    private float speed = 9f;
    private float jumpingPower = 16f;
    private bool facingRight = true;
    
    private float coyoteTime = 0.175f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.175f;
    private float jumpBufferCounter;

    public int maxHealth = 100;
    int currentHealth;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;

    public bool KnockFromRight;

    [SerializeField] GameObject Button1;
    [SerializeField] GameObject PlatformButton1;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Transform player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        currentHealth = maxHealth;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.isActive == true)
        {
            horizontal = 0;
            return;
        } 

        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));


        if(Grounded())
        {
            coyoteTimeCounter = coyoteTime;
            animator.SetBool("isJumping", false);
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            animator.SetBool("isJumping", true);
        }

        if(Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            animator.SetBool("isJumping", true);
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if(jumpBufferCounter > 0 && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferCounter = 0;
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        Flip();

    }

    void FixedUpdate()
    {
        if(KBCounter <= 0)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            if(KnockFromRight == true)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if(KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }

            KBCounter -= Time.deltaTime;
        }
        
        
        if (horizontal != 0)
        {
            //CreateDust();
        }
    }

    private bool Grounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    private void Flip()
    {
        if (facingRight && horizontal < 0f || !facingRight && horizontal > 0f)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    /*void CreateDust()
    {
        if (horizontal != 0)
        {
            dust.Play();
        }
    }*/

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player's health = " + currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Ded!!");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            currentHealth = currentHealth - 20;
            Debug.Log("Player's health = " + currentHealth);
        }
        if(currentHealth <= 0)
        {
            
        }
    }

}
