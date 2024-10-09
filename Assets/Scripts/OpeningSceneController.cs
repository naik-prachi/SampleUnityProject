using UnityEngine;
using TMPro; // Add the TMPro namespace
using UnityEngine.UI;

public class OpeningSceneController : MonoBehaviour
{
    [SerializeField] private TMP_InputField privateKeyInput; // Use TMP_InputField instead of InputField
    [SerializeField] private Button startGameButton;

    void Start()
    {
        startGameButton.onClick.AddListener(SavePrivateKey);
    }

    void SavePrivateKey()
    {
        // Store the private key entered by the user
        
        string privateKey = privateKeyInput.text;
        PlayerPrefs.SetString("PrivateKey", privateKey);

        // Proceed to the next scene (Level 1)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
}