using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelcounter : MonoBehaviour
{
    [SerializeField] private TMP_Text num;
    private int i;

    void Start() {
        i = PlayerPrefs.GetInt("Level");
        num.text = i.ToString();
    }

    // void Update() {
    //     Debug.Log(i);
    // }

    public void NextLevel(int n) {
        i = n;
        PlayerPrefs.SetInt("Level",i);
    }
}
