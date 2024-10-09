using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;

    // defining attack point

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }
    }

    void Attack(){
        // Play an attack animation
        anim.SetTrigger("Attack");

        // Detect enemies in range of attack

        // Disable them
    }
}
