using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invis_timer : MonoBehaviour
{
    private int i = 0;
    [SerializeField] private GameObject tile1;
    [SerializeField] private GameObject tile2;

    private int duration = 2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(duration);
        if (Time.time >= duration) {
            choose();
        }
    }

    void choose() {
        if(i == 0) {
            i = 1;
            tile1.SetActive(true);
            tile2.SetActive(false);
            StartCoroutine(Timer());
        }
        else {
            i = 0;
            tile1.SetActive(false);
            tile2.SetActive(true);
            StartCoroutine(Timer());
        }
    }
}
