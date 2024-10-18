using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class startmanager : MonoBehaviour
{
    public GameObject a,b,c,d,shape;
    
    public void play() {
        a.SetActive(true);
        b.SetActive(true);
        c.SetActive(true);
        d.SetActive(true);
        shape.SetActive(true);
    }

    public void back() {
        a.SetActive(false);
        b.SetActive(false);
        c.SetActive(false);
        d.SetActive(false);
        shape.SetActive(false);
    }
}
