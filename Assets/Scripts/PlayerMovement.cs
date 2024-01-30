using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }
   
    // Update is called once per frame
    void Update()
    {
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
        
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
      

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

    void OnCollisionEnter2D(Collision2D col)
    {
    
    }
}
