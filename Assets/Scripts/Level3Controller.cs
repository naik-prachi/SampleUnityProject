using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Controller : MonoBehaviour
{
    public Text scoreDisplay;
     // Assign this in the Inspector
     int gems;

    private void Start()
    {
        // Retrieve the score from PlayerPrefs
        // int score = PlayerPrefs.GetInt("PlayerScore", 0); // Default to 0 if not found

        // Display the score in the UI
        scoreDisplay.text = "Score: " + gems.ToString();
    }
}