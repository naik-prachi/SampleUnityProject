using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject key; // Reference to the key game object
    public string nextLevelName; // Name of the next scene

    private bool isOpen = false;
    private Animator animator; // Reference to the Animator

    public levelcounter lv;
    public int nextLevelnumber;

    private void Start()
    {
        // Get the Animator component attached to the door
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(key); // Destroy the key when the player reaches the door
            OpenDoor();
        }
        else {
            Debug.Log("Get the key");
        }
    }

    private void OpenDoor()
    {
        if (!isOpen)
        {
            Debug.Log("Door is opening...");

            // Play the "OpenDoor" animation by triggering the parameter
            animator.SetTrigger("OpenDoor");

            lv.NextLevel(nextLevelnumber);

            // Start the coroutine to load the next level after the animation
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator LoadNextLevel()
    {
        // Wait for the door opening animation to complete (adjust the delay to match animation time)
        yield return new WaitForSeconds(1f);

        // Load the next level/scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
    }
}