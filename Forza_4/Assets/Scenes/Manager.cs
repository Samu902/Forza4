using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public enum SlotState
    {
        Empty, Red, Yellow
    }

    public GameObject pRossa;
    public GameObject pGialla;

    public float[] columnsX;
    public SlotState[,] griglia = new SlotState[7, 6];

    public int turno;
    private bool busy;

    void Start()
    {
        turno = 0;
        busy = false;
    }

    void Update()
    {
        
    }
}
