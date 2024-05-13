using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimationTest : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public CinemachineVirtualCamera gameplayCamera;
    public int yon;

    public float rayUzunlugu = 10f; // Ray uzunluðu
    public Vector3 rayYonu = Vector3.forward; // Ray yönü
    public Transform rayOrigin; // Rayin baþlangýç noktasý

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    bool isGrounded;
    bool isDeath=false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Engel")
        {
            Death();
        }
    }
    private void Update()
    {
        if (isDeath==false)
        {
            //KeyboardMove();
            UIMove();
        }
        GroundCheck();
    }
    void UIMove()
    {
        if (yon==1)
        {
             rb.velocity = new Vector2(speed, rb.velocity.y);
             spriteRenderer.flipX = false;
             if (isGrounded == true)
             {
                 animator.Play("RunAnim");
             }
        }
        else if (yon==-1)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            spriteRenderer.flipX = true;
            if (isGrounded == true)
            {
                animator.Play("RunAnim");
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (isGrounded == true)
            {
                animator.Play("IdleAnim");
            }
        }
        
    }
    void KeyboardMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            spriteRenderer.flipX = true;
            if (isGrounded == true)
            {
                animator.Play("RunAnim");
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            spriteRenderer.flipX = false;
            if (isGrounded == true)
            {
                animator.Play("RunAnim");
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (isGrounded == true)
            {
                animator.Play("IdleAnim");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();

        }
    }
    public void Jump()
    {
        if (isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce);

        }
    }
    void GroundCheck()
    {
        if (isDeath==false)
        {
            Vector2 merkez = rayOrigin.position;

            RaycastHit2D hit = Physics2D.Raycast(merkez, rayYonu, rayUzunlugu);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Zemin")
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
                    animator.Play("JumpAnim");
                }

                // Çarpma noktasýný görselleþtirme (Sadece Editörde görünür)
               // Debug.DrawRay(merkez, rayYonu * rayUzunlugu, Color.red);
            }
            else
            {
                isGrounded = false;
                animator.Play("JumpAnim");
                //Debug.DrawRay(merkez, rayYonu * rayUzunlugu, Color.green);
            }
        }
        
    }
    void Death() 
    {
        rb.gravityScale = -0.7f;
        animator.Play("Death");
        gameplayCamera.enabled = false;
        isDeath = true;
    }
    public void ChangeDirection(int yeniYon)
    {
        yon = yeniYon;
    }

}
