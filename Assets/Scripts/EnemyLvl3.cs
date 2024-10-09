using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyLvl3 : MonoBehaviour
{
    //  enemy animator
    public Animator anim;

    // enemy health
    public int maxHealth = 100;
    private int currentHealth;

    // player damage
    public int damage = 10; // Damage dealt to the player
    public float attackRange = 1.5f; // Range to attack the player
    public float attackCooldown = 2f; // Time between attacks

    private float lastAttackTime;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Check if the player is in range to attack
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D player in hitPlayers)
        {
            if (player.CompareTag("Player")) // Make sure the player has the tag "Player"
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    Attack(player.GetComponent<PlayerCombat>());
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // play hurt animation
        anim.SetTrigger("Hurt");


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // play death animation
        anim.SetBool("IsDead", true);

        // disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        // destroy the GameObject after a delay (optional)
        Destroy(gameObject, 1f); // Adjust the delay as needed

        // load the ending scene
        // SceneManager.LoadScene("EndingScene");

    }

    void Attack(PlayerCombat playerHealth)
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }


}
