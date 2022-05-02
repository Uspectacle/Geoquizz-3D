using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : MonoBehaviour
{
    public GameObject earth;
    public float offsetDistance;
    public float rotateSpeedDay;
    public float rotateSpeedYear;
    public float zoomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(earth.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(earth.transform.position, Vector3.up, rotateSpeedDay * Time.deltaTime);
        transform.RotateAround(earth.transform.position, transform.up, rotateSpeedYear * Time.deltaTime);
    }
}
