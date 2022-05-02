using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SatteliteController : MonoBehaviour
{
    public GameObject earth;
    public Camera cam;
    public Vector2 rotateSpeed;
    public float rotateFriction;
    public float distancePower;
    public float rotateBoundry;
    public float moveSpeed;
    public float touchZoom;
    public float moveFriction;
    public float moveFOV;
    public Vector2 moveBoundry;

    private Vector2 rotateVector;
    private Vector2 torqueVector;
    private float move;
    private Vector2 mousePrevPos;
    private Vector2 screenRes;


    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(earth.transform.position);
        rotateVector = new Vector2(0.0f, 0.0f);
        move = 0.0f;
        mousePrevPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    // Ways of controling the zoom
    float ForceInput()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = ((touchZeroPrevPos - touchOnePrevPos)/screenRes).magnitude;
            float currentMagnitude = ((touchZero.position - touchOne.position)/screenRes).magnitude;

            return (currentMagnitude - prevMagnitude) * touchZoom;
        }
        // TODO: implement for multitouch  
        return Input.mouseScrollDelta.y;
    }

    // Zoom within defined boundry according to a force
    void MoveSattelite(float force)
    {
        move += (force * moveSpeed - move * moveFriction) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, earth.transform.position, move);
        float distance = (transform.position - earth.transform.position).magnitude;

        if (distance < moveBoundry.x || distance > moveBoundry.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, earth.transform.position, -move);
            move = 0.0f;
        }
        cam.fieldOfView += move * moveFOV;
    }

    // Ways of controling the zoom
    Vector2 TorqueInput()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y)/screenRes;
        Vector2 mouseDelta = new Vector2(0f, 0f);
        if (Input.GetMouseButton(0) & !Input.GetMouseButtonDown(0))
        {
            mouseDelta = mousePosition - mousePrevPos;
        }
        mousePrevPos = mousePosition;
        return -mouseDelta;
    }

    // Roatate around within defined boundry according to a torque
    void RotateSattelite(Vector2 torque)
    {
        float distance = (transform.position - earth.transform.position).magnitude;
        distance = (distance - moveBoundry.y) / (moveBoundry.x - moveBoundry.y);
        distance = Mathf.Pow(1 + distance, distancePower);
        rotateVector += (torque * rotateSpeed * distance - rotateVector * rotateFriction) * Time.deltaTime;

        transform.RotateAround(earth.transform.position, Vector3.up, rotateVector.x * Time.deltaTime);
        transform.RotateAround(earth.transform.position, transform.right, rotateVector.y * Time.deltaTime);

        if (Mathf.Abs(transform.eulerAngles.x-180) < Mathf.Abs(rotateBoundry-180))
        {
            transform.RotateAround(earth.transform.position, transform.right, -rotateVector.y * Time.deltaTime);
            rotateVector.y = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        screenRes = new Vector2(Screen.width, Screen.height);
        MoveSattelite(ForceInput());
        if (Input.touchCount > 1)
        {
            rotateVector.y = 0.0f;
            return;
        }
        RotateSattelite(TorqueInput());

    }
}
