using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class EarthAction : MonoBehaviour
{
    public float speed = 100;
    public float init_speed = 100;
    
    private float movementX;
    private float movementY;
    private float rotationX;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(new Vector3(0.0f, 1.0f, 0.0f) * init_speed);
        rotationX = transform.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(-movementY, movementX, 0.0f);


        rb.AddTorque(movement * speed);
        // Debug.Log("transform.rotation.x " + transform.rotation.x.ToString());
        // Debug.Log("rotationX " + rotationX.ToString());
        // Debug.Log("transform.localEulerAngles.x " + transform.localEulerAngles.x.ToString());
        // Debug.Log("rotationX " + rotationX.ToString());
        // rotationX = Mathf.Clamp(rotationX, -8.0f, 8.0f);
        // transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, Mathf.Clamp(transform.localEulerAngles.z, -8.0f, 8.0f));
        // transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, -8.0f, 8.0f), transform.eulerAngles.y, 0.0f);
        // transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, -80.0f, 80.0f), transform.localEulerAngles.y, Mathf.Clamp(transform.localEulerAngles.z, -0.0f, 0.0f));
        // transform.eulerAngles = transform.eulerAngles

    }
    
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
}
