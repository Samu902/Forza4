using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Manager man;

    void Start()
    {
        man = GameObject.Find("Manager").GetComponent<Manager>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("OK");
        if (man.gameOver)
            return;
        man.CheckMove(gameObject.name, man.PosInGrid(gameObject));
        man.busy = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OK");
        if (man.gameOver)
            return;
        man.CheckMove(gameObject.name, man.PosInGrid(gameObject));
        man.busy = false;
    }
}
