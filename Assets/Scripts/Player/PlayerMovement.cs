using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField]private float coyoteTime;
    private float coyoteCounter;

    [SerializeField] private int extrtaJump;
    private int jumpCounter;

    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator moveAnimation;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        moveAnimation = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Zmiana kierunku modelu gracza
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Paramerty animatora
        moveAnimation.SetBool("run", horizontalInput != 0);
        moveAnimation.SetBool("grounded", isGrounded());

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            Jump();
        if (Input.GetKeyUp(KeyCode.UpArrow) && body.velocity.y>0 || Input.GetKeyUp(KeyCode.W) && body.velocity.y > 0)
            body.velocity =  new Vector2(body.velocity.x,body.velocity.y /2);
        if (onWall())
        {
            body.gravityScale = 4;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput*speed,body.velocity.y);
        }

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extrtaJump;

        }
        else
        {
            coyoteCounter -= Time.deltaTime;
                }
    }

    private void Jump()
    {
        if (coyoteCounter < 0 && !onWall()&& jumpCounter <= 0) return;
        SoundManager.instance.PlaySound(jumpSound);
       
        if(onWall())
        {
            WallJump();
        }
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpHeight);
            else
            {
                if(coyoteCounter>0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpHeight);
                }
                else
                {
                    if(jumpCounter>0)
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpHeight);
                        jumpCounter--;  
                    }
                }
            }
            //Ustawienie wartosci na 0 aby unikaæ double jumpa
            coyoteCounter = 0;
        }
    }
    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, platformLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return !onWall();
    }
}
