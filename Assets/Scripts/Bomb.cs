using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bomb collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call the player's death or damage function
            // PlayerMovements player = collision.gameObject.GetComponent<PlayerMovements>();
            PlayerCombat player = collision.gameObject.GetComponent<PlayerCombat>();

            if (player != null)
            {
                player.TakeDamage(100);  // Assuming TakeDamage is a method on the player that handles health/dying
            }

            // Destroy the bomb after it hits the player
            Destroy(gameObject);

            // Load the ending scene
            SceneManager.LoadScene("EndingScene");
        }
    }

    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }
}
