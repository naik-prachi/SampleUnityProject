using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexTypes;
using System.Threading.Tasks;
using Nethereum.Util;

public class Level4Controller : MonoBehaviour
{
    public string closingSceneName = "ClosingScene"; // Name of the closing scene
    public GameObject door; // Reference to the door object
    public Text scoreText; // UI element to display the score
    private int coins = 0;

    // Ethereum transfer-related variables
    private Web3 web3;
    private string accountAddress;
    public string recipientAddress;

    private void Start()
    {
        // Get the private key saved from the opening scene
        string privateKey = PlayerPrefs.GetString("PrivateKey");

        // Initialize Web3 with the private key
        var account = new Account(privateKey);
        web3 = new Web3(account, "http://127.0.0.1:7545");
        accountAddress = account.Address;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == door) // If player collides with the door
        {
            // Trigger the Ethereum transfer and load the closing scene
            HandleLevelCompletion();
        }
    }

    private async void HandleLevelCompletion()
    {
        // Display "Transferring..." or a loading screen while waiting for Ethereum transfer
        scoreText.text = "Transferring Ethereum...";

        // Perform the Ethereum transfer
        string transactionHash = await TransferEthereum();

        if (transactionHash != null)
        {
            // Store the transaction hash for display in the closing scene
            PlayerPrefs.SetString("TransactionHash", transactionHash);
        }

        // Load the closing scene after the transfer is complete
        SceneManager.LoadScene(closingSceneName);
    }

    private async Task<string> TransferEthereum()
    {
        try
        {
            // Send 2 ETH
            var transactionHash = await SendEtherAsync(accountAddress, recipientAddress, 2);
            Debug.Log($"Transaction hash: {transactionHash}");

            return transactionHash;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error transferring Ethereum: {ex.Message}");
            return null;
        }
    }

    private async Task<string> SendEtherAsync(string fromAddress, string toAddress, decimal amountInEther)
    {
        var amountInWei = Web3.Convert.ToWei(amountInEther);
        var transaction = new Nethereum.RPC.Eth.DTOs.TransactionInput
        {
            From = fromAddress,
            To = toAddress,
            Value = new HexBigInteger(amountInWei),
            Gas = new HexBigInteger(21000),
            GasPrice = new HexBigInteger(Web3.Convert.ToWei(20, UnitConversion.EthUnit.Gwei))
        };

        var transactionHash = await web3.Eth.Transactions.SendTransaction.SendRequestAsync(transaction);
        return transactionHash;
    }
}