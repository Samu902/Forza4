using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                if(hit.collider.name == "ResetButton" && man.gameOver)
                {
                    StartCoroutine(MakeCoinFall());
                }
            }
        }
    }

    IEnumerator MakeCoinFall()
    {
        GameObject lin = GameObject.Find("Linguetta");
        while (lin.transform.position.x >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            lin.transform.position += Vector3.left * 0.2f;
        }
        lin.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
