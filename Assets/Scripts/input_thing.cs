using System.Collections;
using System.Collections.Generic;

using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Nethereum.Hex.HexTypes;
using Nethereum.Util;

public class input_thing : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button btn;
    public SceneLoader sce;

    private string input;

    // most of the part from connect wallet script
    private Web3 web3;
    private string rpcUrl = "HTTP://127.0.0.1:8545";

    private void Start(){

        btn.onClick.AddListener(async () => 
        { 
            await validateInput(); 
        });
        
        // if (Input.GetKey("return")) {
        //     validateInput();
        // }

        // Initialize Web3 instance
        web3 = new Web3(rpcUrl);
    }

    private async Task validateInput() {
        input = inputField.text;
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
                Debug.Log(accounts[i]);
                if (accounts != null && accounts[i] == input.ToLower())
                {
                    Debug.Log("Connected account: " + input);
                    // Now you can interact with Ethereum using `web3` object
                    // Example: Get balance
                    var balance = await web3.Eth.GetBalance.SendRequestAsync(input);
                    Debug.Log("Balance: " + balance.Value);

                    PlayerPrefs.SetString("public key",input);
                    PlayerPrefs.SetInt("Level",1);
                    PlayerPrefs.SetInt("Score",0);
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
