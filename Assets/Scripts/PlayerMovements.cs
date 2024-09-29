using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovements : MonoBehaviour
{
    // player body
    private Rigidbody2D rb;

    // movement speed
    private float movementForce = 3f;
    private float jumpForce = 5f;

    // the ground layers have collider2d
    private Collider2D coll;

    // animator
    private Animator anim;

    // layer mask
    [SerializeField] private LayerMask ground;

    // state finite system
    private enum State { idle, running, jumping, falling, hurt };
    private State state = State.idle;

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
        // if not hurt, movement is allowed
        if (state != State.hurt)
        {
            Movement();
        }

        AnimationState();
        anim.SetInteger("state", (int)state);
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

}
