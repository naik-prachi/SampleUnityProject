using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLvl3 : MonoBehaviour
{

    public Animator anim;
    public int maxHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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

    }
}
