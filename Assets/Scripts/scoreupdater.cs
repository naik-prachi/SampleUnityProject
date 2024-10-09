using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreupdater : MonoBehaviour
{
    [SerializeField] private Score sc;

    public void update() {
        sc.TransferScore();
    }
}
