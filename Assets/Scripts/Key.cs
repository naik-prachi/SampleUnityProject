using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isCollected = false;
    public Transform player; // Reference to the player's transform
    public float followSpeed = 5f; // Speed at which the key follows the player

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollected = true;
        }
    }

    void Update()
    {
        if (isCollected)
        {
            // Move the key towards the player
            transform.position = Vector3.Lerp(transform.position, player.position, followSpeed * Time.deltaTime);
        }
    }
}