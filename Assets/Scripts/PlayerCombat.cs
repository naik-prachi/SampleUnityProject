using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    // defining attack point
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;

    // enemy damage + attack
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0;

    // player health
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

    void Attack()
    {
        // Play an attack animation
        anim.SetTrigger("Attack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyLvl3>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage: " + damage);

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Implement player death logic here (e.g., game over, respawn)

        // play death animation
        anim.SetBool("IsDead", true);

        // disable the enemy
        this.enabled = false;

        // destroy the GameObject after a delay (optional)
        // Destroy(gameObject, 2f);

        // load the ending scene
        // SceneManager.LoadScene("EndingScene");
        // Start the coroutine to wait for the animation
        StartCoroutine(WaitForAnimationAndLoadScene());
    }

    private IEnumerator WaitForAnimationAndLoadScene()
    {
        // Wait until the death animation is completed
        // Assuming your death animation is 1 second long (adjust if necessary)
        // yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Destroy the GameObject
        Destroy(gameObject, 2f);

        // Load the ending scene
        SceneManager.LoadScene("EndingScene");
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
