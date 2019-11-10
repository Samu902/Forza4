using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallina : MonoBehaviour
{
    public GameObject pallinaRossa;
    public GameObject pallinaGialla;
    public float[] x;
    //Da controllare
    private int cont = 0;
    public SlotState[,] griglia = new SlotState[7, 6];
    public enum SlotState
    {
        Empty, Red, Yellow
    }
    private bool busy = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            GeneraPallina(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            GeneraPallina(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            GeneraPallina(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            GeneraPallina(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            GeneraPallina(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            GeneraPallina(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            GeneraPallina(6);
    }

    private void AzzeraGriglia()
    {
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                griglia[x, y] = SlotState.Empty;
            }
        }
    }

    private void inserisciPallina(int x, int y, SlotState type)
    {
        griglia[x, y] = type;
    }

    private int trovaY(int x)
    {
        for (int i = 0; i < 6; i++)
        {
            if (griglia[x, i] == SlotState.Empty)
            {
                return i;
            }
        }
        return 0;
    }

    public void GeneraPallina(int index)
    {
        if (busy)
            return;
        
        busy = true;
        if (griglia[index, 5] == SlotState.Empty)
        {
            GameObject g;
            SlotState stato;
            cont++;
            if (cont % 2 == 1)
            {
                g = pallinaRossa;
                stato = SlotState.Red;
            }
            else
            {
                g = pallinaGialla;
                stato = SlotState.Yellow;
            }
            Instantiate(g, new Vector3(x[index], 4.5f, 0), Quaternion.identity);
            inserisciPallina(index, trovaY(index), stato);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        busy = false;
    }
}
