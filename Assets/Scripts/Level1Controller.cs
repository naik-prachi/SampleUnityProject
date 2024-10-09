using UnityEngine;
using UnityEngine.UI;

public class Level1Controller : MonoBehaviour
{
    public Text scoreDisplay; // Assign this in the Inspector

    private void Start()
    {
        // Retrieve the score from PlayerPrefs
        int gems = PlayerPrefs.GetInt("PlayerScore", 0); // Default to 0 if not found

        // Display the score in the UI
        scoreDisplay.text = "Score: " + gems.ToString();
    }
}