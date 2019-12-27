using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Manager man;
    private CameraMove camMove;
    private bool hasAlreadyFallen;

    void Start()
    {
        man = GameObject.Find("Manager").GetComponent<Manager>();
        camMove = Camera.main.gameObject.GetComponent<CameraMove>();
        hasAlreadyFallen = false;
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

        hasAlreadyFallen = true;
        man.CheckMove(gameObject.name, man.PosInGrid(gameObject));

        if (man.gameOver)
            return;

        StartCoroutine(camMove.TurnCam());
        //man.busy = false;
    }
}
