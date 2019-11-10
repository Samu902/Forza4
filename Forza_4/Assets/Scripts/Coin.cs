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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OK");
        man.busy = false;
    }
}
