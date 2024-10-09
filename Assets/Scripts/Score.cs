using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text num;
    private int i;

    void Start() {
        i = PlayerPrefs.GetInt("Score");
        num.text = i.ToString();
    }

    // void Update() {
    //     Debug.Log(i);
    // }

    public void Addscore() {
        i += 1;
        num.text = i.ToString();
    }

    public void TransferScore() {
        PlayerPrefs.SetInt("Score",i);
    }

    public void Resetscore() {
        PlayerPrefs.SetInt("Score",0);
    }
}
