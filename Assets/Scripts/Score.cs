using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text num;
    private int i = 0;

    void Start() {
        num.text = i.ToString();
    }

    public void Addscore() {
        i+=1;
        num.text = i.ToString();
    }
}
