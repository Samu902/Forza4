using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject cam;
    private float distance;
    private float angle;
    public float turnRate;

    void Start()
    {
        cam = gameObject;
        distance = Mathf.Abs(cam.transform.position.z);
        angle = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            angle += turnRate * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow))
            angle -= turnRate * Time.deltaTime;

        cam.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, angle + 180, 0);
        cam.transform.position = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * distance, cam.transform.position.y, Mathf.Cos(angle * Mathf.Deg2Rad) * distance);
    }
}
