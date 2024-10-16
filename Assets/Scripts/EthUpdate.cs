using UnityEngine;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexTypes;
using System.Threading.Tasks;
using UnityEngine.UI;
using Nethereum.Util;
using TMPro;

public class EthUpdate : MonoBehaviour
{
    public string privateKey = "0xa8304db76115697d5d193df573bc58c72d08cafc0558b5d6e9f0f00ebe8e088c"; // Replace with your private key
    private string key;
    // private int i=0;
    public Button updateButton;
    public TMP_Text balanceText;

    public EthUpdate instance;

    private Web3 web3;
    private string accountAddress;

    void Start()
    {
        // Initialize Web3 instance
        var account = new Account(privateKey);
        web3 = new Web3(account, "http://127.0.0.1:8545"); // Ganache default URL

        // Get the account address from the private key
        accountAddress = account.Address;

        // Set up the button click listener
        updateButton.onClick.AddListener(OnUpdateButtonClick);
    }

    async void OnUpdateButtonClick()
    {
        try
        {
            // Update the balance by sending some ETH
            var transactionHash = await SendEtherAsync(accountAddress, PlayerPrefs.GetString("public key"), PlayerPrefs.GetInt("Score"));
            Debug.Log($"Transaction hash: {transactionHash}");

            // Update the balance display
            await UpdateBalanceDisplay();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error: {ex.Message}");
        }
    }

    async Task<string> SendEtherAsync(string fromAddress, string toAddress, decimal amountInEther)
    {
        // Convert Ether to Wei
        var amountInWei = Web3.Convert.ToWei(amountInEther);

        var transaction = new Nethereum.RPC.Eth.DTOs.TransactionInput
        {
            From = fromAddress,
            To = toAddress,
            Value = new HexBigInteger(amountInWei),
            Gas = new HexBigInteger(21000), // Gas limit for standard transfers
            GasPrice = new HexBigInteger(Web3.Convert.ToWei(20, UnitConversion.EthUnit.Gwei)) // Gas price in Gwei
        };

        // Send the transaction
        var transactionHash = await web3.Eth.Transactions.SendTransaction.SendRequestAsync(transaction);
        return transactionHash;
    }

    async Task UpdateBalanceDisplay()
    {
        // Get the balance of the account
        var balance = await web3.Eth.GetBalance.SendRequestAsync(accountAddress);
        var balanceInEther = Web3.Convert.FromWei(balance.Value);
        
        var balance2 = await web3.Eth.GetBalance.SendRequestAsync(PlayerPrefs.GetString("public key"));
        var balanceInEther2 = Web3.Convert.FromWei(balance2.Value);

        // Update UI with balance
        balanceText.text = $"Balance: {balanceInEther2} ETH";
        Debug.Log(balanceInEther);

        PlayerPrefs.DeleteKey("public key");

        // try {
        //     // Update UI with balance
        //     balanceText.text = $"Balance: {balanceInEther2} ETH";
        //     Debug.Log(balanceInEther);
        // }
        // catch (Nethereum.Web3.Error) {
        //     Debug.Log("Play again or enter public key");
        // }
        // finally {
        //     PlayerPrefs.DeleteKey("public key");
        // }
    }
}
