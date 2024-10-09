using System.Collections;
using System.Collections.Generic;

using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class input_thing : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text resultText;
    [SerializeField] Button btn;
    public SceneLoader sce;

    // most of the part from connect wallet script
    private Web3 web3;
    private string rpcUrl = "HTTP://127.0.0.1:7545";

    private void Start(){

        btn.onClick.AddListener(async () => 
        { 
            await validateInput(); 
        });
        
        if (Input.GetKey("return")) {
            validateInput();
        }

        // Initialize Web3 instance
        web3 = new Web3(rpcUrl);

        sce = FindObjectOfType<SceneLoader>();
    }

    private async Task validateInput() {
        string input = inputField.text;
        // Debug.Log(input);

        try
        {
            // Request accounts from MetaMask
            string[] accounts = await web3.Eth.Accounts.SendRequestAsync();

            // Just simply add a for loop for verification procedure prototype

            // if (accounts[0] == input.ToLower()) {
            //     Debug.Log(accounts[0]);
            //     Debug.Log(input);
            //     sce.level_one();
            // }

            for (int i=0; i<accounts.Length; i++) {
                if (accounts != null && accounts[i] == input.ToLower())
                {
                    string selectedAccount = input;
                    Debug.Log("Connected account: " + selectedAccount);
                    var balance = await web3.Eth.GetBalance.SendRequestAsync(selectedAccount);
                    Debug.Log("Balance: " + balance.Value);
                    sce.LoadLevel();
                    break;
                }
                else
                {
                    Debug.Log("MetaMask connection failed or no accounts available.");
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error connecting to MetaMask: " + ex.Message);
        }
    }
}
