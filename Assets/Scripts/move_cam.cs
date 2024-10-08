using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_cam : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("up")) {
            Debug.Log("up");
            rb.velocity = new Vector2(rb.velocity.x, 1f);
        }

        if(Input.GetKey("down")) {
            Debug.Log("down");
            rb.velocity = new Vector2(rb.velocity.x, -1f);
        }

        if(Input.GetKey("left")) {
            Debug.Log("left");
            rb.velocity = new Vector2(-1f, rb.velocity.y);
        }

        if(Input.GetKey("right")) {
            Debug.Log("right");
            rb.velocity = new Vector2(1f, rb.velocity.y);
        }
    }
}