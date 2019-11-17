using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInput : MonoBehaviour
{
    private float[] columnsX;
    private Manager man;

    void Start()
    {
        columnsX = new float[]{ -3, -2, -1, 0, 1, 2, 3};
        man = GameObject.Find("Manager").GetComponent<Manager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.collider.tag == "Button")
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (hit.transform.position.x == columnsX[i])
                        {
                            man.GenerateCoin(i);
                            break;
                        }
                    }
                }
            }
        }
    }
}
