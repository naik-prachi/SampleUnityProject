using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexTypes;
using System.Threading.Tasks;
using Nethereum.Util;

public class PlayerMovements : MonoBehaviour
{

    // Start() variables
    private Collider2D coll;        // the ground layers have collider2d
    private Animator anim;          // animator
    private Rigidbody2D rb;         // player body

    // FSM 
    private enum State { idle, running, jumping, falling, hurt, climbing };
    private State state = State.idle;

    // Inspector variables
    // [SerializeField] 
    [SerializeField] private LayerMask ground;      // layer mask

//eth
    public string privateKey;// = "0x5e84b71eeaaa173a0f937e19863b4449f25c2f5c0c03df1f8bf5dee03b671062"; // Replace with your private key
    public string recipientAddress ;//= "0x8e0b25F4B06dbE87D442d258fd869B790066513f"; // Replace with the recipient address
    // public Button updateButton;
    public Text balanceText;

    private Web3 web3;
    private string accountAddress;

    // movement speed
    private float movementForce = 5f;
    private float jumpForce = 8f;
    private float hurtForce = 20f;

    // player health
    private int currentHealth;
    public int maxHealth = 100;

    // to climb the ladder
    private float dirX, dirY;
    public bool ClimbingAllowed { get; set; }
    [SerializeField] private int gems = 0;
    [SerializeField] private Text gemtext;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        var account = new Account(privateKey);
        web3 = new Web3(account, "http://127.0.0.1:7545"); // Ganache default URL


        // player health
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        // for climbing the ladder
        dirX = Input.GetAxisRaw("Horizontal") * movementForce;
        if (ClimbingAllowed)
        {
            dirY = Input.GetAxisRaw("Vertical") * movementForce;
        }

        // if not hurt, movement is allowed
        if (state != State.hurt)
        {
            Movement();
        }

        AnimationState();
        anim.SetInteger("state", (int)state);

    }


    private void FixedUpdate()
    {
        if (ClimbingAllowed)
        {
            rb.isKinematic = true;
            rb.velocity = new Vector2(dirX, dirY);
        }
        else
        {
            rb.isKinematic = false;
            rb.velocity = new Vector2(dirX, rb.velocity.y);
        }
    }

    // state finite system
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            // moving
            state = State.running;
        }
        else if (ClimbingAllowed)
        {
            state = State.climbing;
        }
        else
        {
            state = State.idle;
        }

    }

    // player movement
    private void Movement()
    {
        // these conversions because the condition gives a numerical value not a bool
        float hDirection = Input.GetAxis("Horizontal");


        // if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
        if (hDirection < 0)
        {
            if (rb.transform.rotation.y == 0)
            {
                rb.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            rb.velocity = new Vector2(-movementForce, rb.velocity.y);

            // run animation

        }
        else if (hDirection > 0)
        {
            if (rb.transform.rotation.y == 1)
            {
                rb.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            rb.velocity = new Vector2(movementForce, rb.velocity.y);

            // run animation

        }
        // else if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
        // {
        //     rb.velocity = new Vector2(0, rb.velocity.y);
        // }

        else
        {

        }

        // if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        if (Input.GetKeyDown(KeyCode.Space) && coll.IsTouchingLayers(ground))
        {
            Jump();

        }
    }

    // jump details
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }


    // trigger on coin collision
     async void OnTriggerEnter2D(Collider2D other)
    {
        // if the player hits the coin, inc coin & destroy the coin
        if (other.gameObject.CompareTag("Gems"))
        {
            Destroy(other.gameObject);
            gems += 1;
            gemtext.text = gems.ToString();

        }
        if(gems>3)
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

        // if the player hits the saw, destroy the player
        // if (other.GetComponent<Collider>().tag == "Trap")
        if (other.gameObject.CompareTag("Trap"))
        {
            Destroy(gameObject);
        }
    }

    // for the collision with the enemy
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (state == State.falling)
            {
                Destroy(other.gameObject);
                Jump();
            }
            else
            {
                state = State.hurt;
                // Destroy(gameObject);
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    // enemy to my right --> player damaged and move left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    // enemy to my left --> player damaged and move right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }

        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // play hurt animation
        // anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
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

    public async Task UpdateBalanceDisplay()
    {
        // Get the balance of the account
        var balance = await web3.Eth.GetBalance.SendRequestAsync(accountAddress);
        var balanceInEther = Web3.Convert.FromWei(balance.Value);

        // Update UI with balance
        balanceText.text = $"Balance: {balanceInEther} ETH";
        Debug.Log(balanceInEther);
    }


    void Die()
    {
        // Handle player's death
        Debug.Log("Player died!");

        // play death animation
        anim.SetBool("IsDead", true);

        // disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        // You can add death animations or restart the level here
        Destroy(gameObject);  // Remove the player from the game
    }




}
