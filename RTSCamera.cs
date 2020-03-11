using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour
{
    public float mainSpeed = 15.0f; //regular speed
    public float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    public float maxShift = 250.0f; //Maximum speed when holdin gshift
    public float camSens = 0.25f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); // kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    private bool isRotating = false; // Can be called by other things (e.g. UI) to see if camera is rotating
    private float speedMultiplier; // Used by Y axis to match the velocity on X/Z axis

    public float mouseSensitivity = 5.0f; // Mouse rotation sensitivity.
    private float rotationY = 0.0f;


    void Update()
    {

        // Rotation Manager
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }
        if (isRotating)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
            rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
            rotationY = Mathf.Clamp(rotationY, -90, 90);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0.0f);
        }

        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
            speedMultiplier = totalRun * shiftAdd * Time.deltaTime;
            speedMultiplier = Mathf.Clamp(speedMultiplier, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
            speedMultiplier = mainSpeed * Time.deltaTime;
        }

        p = (p / 3) * Time.deltaTime;

        Vector3 newPosition = transform.position;// If player wants to move on X and Z axis only
        transform.Translate(p);
        if (transform.position.x < 50 && transform.position.x > -50)
        {
            newPosition.x = transform.position.x;
            //Debug.Log(newPosition.x);
        }

        if (transform.position.z < 50 && transform.position.z > -50)
        {
            newPosition.z = transform.position.z;
        }

        // Manipulate Y plane by using Q/E keys
        if (Input.GetKey(KeyCode.Q))
        {
            float tempVal = -speedMultiplier / 4;
            if (newPosition.y + tempVal > 1)
            {
                newPosition.y += tempVal;
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            float tempVal = speedMultiplier / 4;
            if (newPosition.y + tempVal < 5)
            {
                newPosition.y += tempVal;
            }
        }

        transform.position = newPosition;
    }

    // Angryboy: Can be called by other code to see if camera is rotating
    // Might be useful in UI to stop accidental clicks while turning?
    public bool amIRotating()
    {
        return isRotating;
    }

    private Vector3 GetBaseInput()
    { 
        //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}

