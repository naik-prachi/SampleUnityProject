using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBoom : MonoBehaviour
{
    public float fuseTime = 3.0f; // Time before explosion
    private bool hasExploded = false;

    private Animator animator;
    private float timer;
    private Rigidbody2D rb;

    public float bounceForce = 1.0f; // Force to apply for the bounce effect

    void Start()
    {
        animator = GetComponent<Animator>();
        timer = fuseTime;
        rb = GetComponent<Rigidbody2D>();
        // Play the fuse lit animation
        animator.Play("bomb");
    }

    void Update()
    {
        if (hasExploded) return;

        // Countdown timer
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Explode();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasExploded) return;
         if (collision.relativeVelocity.y < 0) // This ensures the bomb is falling
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, 0)); // Prevent negative vertical velocity
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse); // Apply bounce force
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            // If the bomb touches the player immediately, explode immediately
            Explode();
            Destroy(collision.gameObject);
        }
    }

    void Explode()
    {
        if (hasExploded) return;

        hasExploded = true;
// Stop the Rigidbody2D from moving
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.bodyType = RigidbodyType2D.Kinematic; // Set Rigidbody2D to Kinematic to prevent any movement

        // Play explosion animation
        animator.Play("boom");
        // animator.SetTrigger("Explode");

        // Optionally, you can destroy the bomb after the animation has finished
        Destroy(gameObject, 0.5f); // Adjust the delay based on your animation length
    }

}
