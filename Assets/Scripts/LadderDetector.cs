using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMovements;

public class LadderDetector : MonoBehaviour
{
    [SerializeField] public PlayerMovements player;

    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovements>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ladder>())
        {
            player.ClimbingAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Ladder>())
        {
            player.ClimbingAllowed = false;
        }
    }
}
