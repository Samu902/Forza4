using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crono : MonoBehaviour
{
    private TMP_Text frontText;
    private TMP_Text backText;

    public float turnTime;
    [HideInInspector]
    public float currentTime;
    [HideInInspector]
    public bool stopTime;

    void Start()
    {
        frontText = transform.GetChild(1).GetComponent<TMP_Text>();
        backText = transform.GetChild(2).GetComponent<TMP_Text>();

        currentTime = turnTime;
        stopTime = false;

        //StartCoroutine(CountTime());
    }

    void Update()
    {
        string txt;

        if (stopTime)
            return;

        if (Camera.main.GetComponent<CameraMove>().isMoving)
            return;

        if (currentTime <= 0)
        {
            //currentTime = turnTime;
            Manager man = GameObject.Find("Manager").GetComponent<Manager>();
            int result;
            do
            {
                result = man.GenerateCoin(Random.Range(0, 7));
            }
            while (result != 1);
        }

        currentTime -= Time.deltaTime;

        txt = Mathf.Round(currentTime) < 10 ? "0: 0" + Mathf.Round(currentTime) : "0: " + Mathf.Round(currentTime);
        frontText.text = txt;
        backText.text = txt;
    }

    IEnumerator CountTime()
    {
        string txt;

        int i = 0;
        while (i < 3)
        {
            i++;

            if(currentTime <= 0)
            {
                currentTime = turnTime;
                Manager man = GameObject.Find("Manager").GetComponent<Manager>();
                int result;
                do
                {
                    result = man.GenerateCoin(Random.Range(0, 7));
                }
                while (result != 1);
            }

            while (Camera.main.GetComponent<CameraMove>().isMoving)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(0.2f);
            currentTime -= 0.2f;

            txt = Mathf.Round(currentTime) < 10 ? "0: 0" + Mathf.Round(currentTime) : "0: " + Mathf.Round(currentTime);
            frontText.text = txt;
            backText.text = txt;
        }
    }
}
