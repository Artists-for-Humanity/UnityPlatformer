using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //VARIABLES//
    public float speed = 5f;
    public float jumpingPower = 10f;
    private Rigidbody2D rb2d;
    private BoxCollider2D playerCollider;
    private SpriteRenderer sprite;
    // private Vector2 moveX;
    private float moveX;
    private Animator anim;

    public Transform groundCheck; //Attached as a child to the Player, checks if players are on the ground Layer or not
    public LayerMask groundLayer; //Reference to the Ground Layer in the game 
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        // rb2d.MovePosition(rb2d.position + moveX * speed * Time.deltaTime);

    //If the player is on the Ground layer and they tap the jump button, jump
        if(IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpingPower);
            anim.SetBool("isJumping",true);
            anim.SetBool("isMoving", false);
        }

        //If player is holding down jump button their jump height is higher 
        if(Input.GetKeyUp(KeyCode.Space) && rb2d.velocity.y > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
            anim.SetBool("isJumping",true);
            anim.SetBool("isMoving", false);
        }
    }

    public void FixedUpdate()
    {
        rb2d.velocity = new Vector2(moveX * speed, rb2d.velocity.y);
        if(rb2d.velocity.y == 0)
        {
                anim.SetBool("isJumping",false);
                if(rb2d.velocity.x > 0)
                {
                    anim.SetBool("isMoving",true);
                    sprite.flipX = false;
                }
                else if(rb2d.velocity.x == 0)
                {
                    anim.SetBool("isMoving", false);
                }
                else if(rb2d.velocity.x < 0)
                {
                    anim.SetBool("isMoving",true);
                    sprite.flipX = true;
                }
        }
       
    }
    //Checks if the player's GroundCheck collider is overlapping with the Ground Layer.
    //It takes the groundCheck.position, the radius size (as a float), and the groundLayer as parameters.
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
