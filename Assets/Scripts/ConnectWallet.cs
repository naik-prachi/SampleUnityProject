using UnityEngine;
using UnityEngine.UI;
using Nethereum.Web3;
using System.Threading.Tasks;

public class ConnectWallet : MonoBehaviour
{
    public GameObject Player; // Drag your GameObject from the Inspector here

    private Web3 web3;
    private string rpcUrl = "HTTP://127.0.0.1:7545"; // Replace with your RPC URL or Infura endpoint

    private void Start()
    {
        // Initialize Web3 instance
        web3 = new Web3(rpcUrl);
    }

    private void OnMouseDown()
    {
        // Check if the GameObject was clicked
        // if (Player != null)
        // {
            // Debug.Log("GameObject clicked, attempting to connect to MetaMask...");
        ConnectMetaMask();
        // }
    }

    private async void ConnectMetaMask()
    {
        try
        {
            // Request accounts from MetaMask
            var accounts = await web3.Eth.Accounts.SendRequestAsync();

            if (accounts != null && accounts.Length > 0)
            {
                string selectedAccount = accounts[0];
                Debug.Log("Connected account: " + selectedAccount);

                // Now you can interact with Ethereum using web3 object
                var balance = await web3.Eth.GetBalance.SendRequestAsync(selectedAccount);
                Debug.Log("Balance: " + balance.Value);
            }
            else
            {
                Debug.Log("MetaMask connection failed or no accounts available.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error connecting to MetaMask: " + ex.Message);
        }
    }
}

// using UnityEngine;
// using UnityEngine.UI;
// using Nethereum.Web3;
// using Nethereum.Web3.Accounts;
// using System.Threading.Tasks;

// public class ConnectWallet : MonoBehaviour
// {
//     public Button connectButton; // Drag your ConnectButton from the Inspector here

//     private Web3 web3;
//     private string rpcUrl = "HTTP://127.0.0.1:7545"; // Replace with your RPC URL or Infura endpoint

//     private void Start()
//     {
//         // Add a listener to the connectButton
//         connectButton.onClick.AddListener(async () =>
//         {
//             await ConnectMetaMask();
//         });

//         // Initialize Web3 instance
//         web3 = new Web3(rpcUrl);
//     }

//     private async Task ConnectMetaMask()
//     {
//         // Check if MetaMask is installed and ethereum object is injected
        
//             try
//             {
//                 // Request accounts from MetaMask
//                 var accounts = await web3.Eth.Accounts.SendRequestAsync();
                
//                 if (accounts != null && accounts.Length > 0)
//                 {
//                     string selectedAccount = accounts[0];
//                     Debug.Log("Connected account: " + selectedAccount);

//                     // Now you can interact with Ethereum using `web3` object
//                     // Example: Get balance
//                     var balance = await web3.Eth.GetBalance.SendRequestAsync(selectedAccount);
//                     Debug.Log("Balance: " + balance.Value);
//                 }
//                 else
//                 {
//                     Debug.Log("MetaMask connection failed or no accounts available.");
//                 }
//             }
//             catch (System.Exception ex)
//             {
//                 Debug.LogError("Error connecting to MetaMask: " + ex.Message);
//             }
        
        
//     }


// }