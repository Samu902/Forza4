using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject cam;
    private float distance;
    private float angle;
    public float turnRate;
    private Manager man;
    public bool isMoving;

    void Start()
    {
        man = GameObject.Find("Manager").GetComponent<Manager>();
        cam = gameObject;
        distance = Mathf.Abs(cam.transform.position.z);
        angle = 0;
        isMoving = false;
    }

    void Update()
    {
        cam.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, angle + 180, 0);
        cam.transform.position = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * distance, cam.transform.position.y, Mathf.Cos(angle * Mathf.Deg2Rad) * distance);
    }

    public IEnumerator TurnCam()
    {
        float oldZ = cam.transform.position.z;
        Crono c = GameObject.Find("Crono").GetComponent<Crono>(); 
        isMoving = true;
        while (oldZ != -cam.transform.position.z)
        {
            yield return new WaitForSeconds(0.05f);
            angle -= turnRate * 0.05f;

            cam.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, angle + 180, 0);
            cam.transform.position = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * distance, cam.transform.position.y, Mathf.Cos(angle * Mathf.Deg2Rad) * distance);
        }
        c.currentTime = c.turnTime;
        c.stopTime = false;
        isMoving = false;
        man.busy = false;
    }
}
