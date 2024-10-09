using UnityEngine;
using UnityEngine.UI;

public class ClosingSceneController : MonoBehaviour
{
    public Text scoreText;
    public Text transactionText;

    void Start()
    {
        // Get the score and transaction hash from PlayerPrefs
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        string transactionHash = PlayerPrefs.GetString("TransactionHash", "No transaction");

        // Display the score and transaction hash
        scoreText.text = $"Final Score: {finalScore}";
        transactionText.text = $"Ethereum Transaction: {transactionHash}";
    }
}