using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Manager man;
    private CameraMove camMove;
    private Crono crono;
    private bool hasAlreadyFallen;

    void Start()
    {
        man = GameObject.Find("Manager").GetComponent<Manager>();
        camMove = Camera.main.gameObject.GetComponent<CameraMove>();
        hasAlreadyFallen = false;
        crono = GameObject.Find("Crono").GetComponent<Crono>();
        crono.stopTime = true;
    }

    void Update()
    {
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //Debug.Log("OK");
    //    if (man.gameOver)
    //        return;
    //    man.CheckMove(gameObject.name, man.PosInGrid(gameObject));
    //    man.busy = false;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (hasAlreadyFallen)
            return;

        if (man.turn >= 42)
            man.Win("Ciao");

        hasAlreadyFallen = true;
        man.CheckMove(gameObject.name, man.PosInGrid(gameObject));

        if (man.gameOver)
            return;

        StartCoroutine(camMove.TurnCam());
        //man.busy = false;
    }
}
