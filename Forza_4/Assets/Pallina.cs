using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallina : MonoBehaviour
{
    public GameObject pallinaRossa;
    public GameObject pallinaGialla;
    public float[] x;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void GeneraPallina(int index)
    {
        GameObject g = Random.value >= 0.5f ? pallinaGialla : pallinaRossa;
        Instantiate(g, new Vector3(x[index], 4.5f, 0), Quaternion.identity);
    }
}
