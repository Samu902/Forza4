using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crono : MonoBehaviour
{
    private TMP_Text frontText;
    private TMP_Text backText;

    private float currentTime;

    void Start()
    {
        frontText = transform.GetChild(1).GetComponent<TMP_Text>();
        backText = transform.GetChild(2).GetComponent<TMP_Text>();

        currentTime = 30;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        string txt = currentTime < 10 ? "0: 0" + Mathf.Round(currentTime) : "0: " + Mathf.Round(currentTime);
        frontText.text = txt;
        backText.text = txt;
    }
}
