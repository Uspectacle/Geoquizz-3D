using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonMove : MonoBehaviour
{
    public GameObject earth;
    public float offsetDistance;
    public float rotateSpeed;
    public float zoomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(earth.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(earth.transform.position, transform.up, rotateSpeed * Time.deltaTime);
    }
}
