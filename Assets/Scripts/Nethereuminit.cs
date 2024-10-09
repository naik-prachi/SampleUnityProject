using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Nethereum.JsonRpc.UnityClient;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Util;

public class Nethereuminit : MonoBehaviour
{
    public string rpcUrl = "HTTP://127.0.0.1:7545"; // Replace with your RPC URL

    private Web3 web3;

    private void Start()
    {
        web3 = new Web3(rpcUrl);
    }

    public async void GetBalance(string address)
    {
        var balance = await web3.Eth.GetBalance.SendRequestAsync(address);
        Debug.Log("Balance: " + balance.Value);
    }
}
