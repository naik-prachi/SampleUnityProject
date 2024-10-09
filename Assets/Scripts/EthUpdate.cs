using UnityEngine;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexTypes;
using System.Threading.Tasks;
using UnityEngine.UI;
using Nethereum.Util;

public class EthUpdate : MonoBehaviour
{
    public string privateKey = "0x7fd7a811d7260aa15724f57acf92bb12a5df118030136ccc05491867aa096274"; // Replace with your private key
    public string recipientAddress = "0x97ef45EA531f797A19e88d7EE27C4ea170349eA3"; // Replace with the recipient address
    public Button updateButton;
    public Text balanceText;

    private Web3 web3;
    private string accountAddress;

    void Start()
    {
        // Initialize Web3 instance
        var account = new Account(privateKey);
        web3 = new Web3(account, "http://127.0.0.1:7545"); // Ganache default URL

        // Get the account address from the private key
        accountAddress = account.Address;

        // Set up the button click listener
        updateButton.onClick.AddListener(OnUpdateButtonClick);
    }

    async void OnUpdateButtonClick()
    {
        try
        {
            // Update the balance by sending 2 ETH
            var transactionHash = await SendEtherAsync(accountAddress, recipientAddress, 2);
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

        // Update UI with balance
        balanceText.text = $"Balance: {balanceInEther} ETH";
        Debug.Log(balanceInEther);
    }
}
