using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float jumpForce = 600f;
    public float minHeight, maxHeight;

    private float currentSpeed;
    private Rigidbody rb;
    public Animator anim;

    private bool isDead = false;
    private bool facingRight = true;

    

    public Transform groundCheck;
    private bool onGround = false;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //groundCheck = gameObject.transform.Find("GroundCheck");
        currentSpeed = maxSpeed;
    }

    void Update()
    {
        //anim.SetBool("OnGround", onGround);
        //anim.SetBool("Dead", isDead);
        //Debug.Log(onGround);

        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            anim.SetBool("OnGround", false);
            
            rb.AddForce(new Vector2(0, jumpForce));
        }

    }

    void FixedUpdate()
    {
        if(!isDead)
        {
            onGround = Physics.OverlapSphere(groundCheck.position, groundRadius, whatIsGround).Length > 0;
            anim.SetBool("OnGround", onGround);
            anim.SetFloat("vSpeed", rb.velocity.y);
            
            float h = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if(!onGround)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            } 
          
            if((h > 0f && !facingRight) || (h < 0f && facingRight))
            {
                Flip();
            }     

            rb.velocity = new Vector3(h * currentSpeed, rb.velocity.y, z * currentSpeed);

            if(onGround)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                anim.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));
            }

            float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0,0,10)).x;
            float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,10)).x;

            rb.position = new Vector3(Mathf.Clamp(rb.position.x, minWidth + 1, maxWidth - 1),
            rb.position.y, Mathf.Clamp(rb.position.z, minHeight, maxHeight));  
        }
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = maxSpeed;
    }

    void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
