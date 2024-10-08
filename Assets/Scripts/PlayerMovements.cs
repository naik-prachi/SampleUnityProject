using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovements : MonoBehaviour
{

    // Start() variables
    private Collider2D coll;        // the ground layers have collider2d
    private Animator anim;          // animator
    private Rigidbody2D rb;         // player body

    // FSM 
    private enum State { idle, running, jumping, falling, hurt, climbing };
    private State state = State.idle;

    // Inspector variables
    [SerializeField] private float hurtForce;
    [SerializeField] private LayerMask ground;      // layer mask



    // movement speed
    private float movementForce = 5f;
    private float jumpForce = 8f;
    private float dirX, dirY;
    public bool ClimbingAllowed { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // for climbing the ladder
        dirX = Input.GetAxisRaw("Horizontal") * movementForce;
        if (ClimbingAllowed)
        {
            dirY = Input.GetAxisRaw("Vertical") * movementForce;
        }

        // if not hurt, movement is allowed
        if (state != State.hurt)
        {
            Movement();
        }

        AnimationState();
        anim.SetInteger("state", (int)state);

    }


    private void FixedUpdate()
    {
        if (ClimbingAllowed)
        {
            rb.isKinematic = true;
            rb.velocity = new Vector2(dirX, dirY);
        }
        else
        {
            rb.isKinematic = false;
            rb.velocity = new Vector2(dirX, rb.velocity.y);
        }
    }

    // state finite system
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            // moving
            state = State.running;
        }
        else if (ClimbingAllowed)
        {
            state = State.climbing;
        }
        else
        {
            state = State.idle;
        }

    }

    // player movement
    private void Movement()
    {
        // these conversions because the condition gives a numerical value not a bool
        float hDirection = Input.GetAxis("Horizontal");


        // if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
        if (hDirection < 0)
        {
            if (rb.transform.rotation.y == 0)
            {
                rb.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            rb.velocity = new Vector2(-movementForce, rb.velocity.y);

            // run animation

        }
        else if (hDirection > 0)
        {
            if (rb.transform.rotation.y == 1)
            {
                rb.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            rb.velocity = new Vector2(movementForce, rb.velocity.y);

            // run animation

        }
        // else if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
        // {
        //     rb.velocity = new Vector2(0, rb.velocity.y);
        // }

        else
        {

        }

        // if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        if (Input.GetKeyDown(KeyCode.Space) && coll.IsTouchingLayers(ground))
        {
            Jump();

        }
    }

    // jump details
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }


    // trigger on coin collision
    private async void OnTriggerEnter2D(Collider2D other)
    {
        // if the player hits the coin, inc coin & destroy the coin
        if (other.gameObject.CompareTag("Gems"))
        {
            Destroy(other.gameObject);

        }

        // if the player hits the saw, destroy the player
        // if (other.gameObject.CompareTag("Trap"))
        // {
        //     Destroy(rb.gameObject);
        // }
    }

    // for the collision with the enemy
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (state == State.falling)
            {
                Destroy(other.gameObject);
                Jump();
            }
            else
            {
                state = State.hurt;
                // Destroy(gameObject);
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    // enemy to my right --> player damaged and move left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    // enemy to my left --> player damaged and move right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }

        }
    }



}
