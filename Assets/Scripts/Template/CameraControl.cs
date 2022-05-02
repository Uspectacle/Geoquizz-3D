using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject earth;
    public float offsetDistance = 2f;
    public float rotateSpeed = 3.0F;

    private Vector3 positionForCamera;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // offset = transform.position - earth.transform.position;
        transform.LookAt(earth.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 positionForCamera = earth.transform.position - earth.transform.forward * offsetDistance;

        // transform.LookAt(earth);
        transform.RotateAround(earth.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        transform.RotateAround(earth.transform.position, Vector3.right, rotateSpeed * Time.deltaTime);

    }
    
     void LateUpdate ()
     {
        //set camera position
        // transform.position = offset + positionForCamera;
        //set camera rotation
        // transform.rotation = Quaternion.LookRotation(earth.transform.position - transform.position, Vector3.up);
        // transform.position = player.transform.position + offset;
        // transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
    }
}
