using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    public float mouseSensitivity = 1;
    public float controllerSensitivity = 1;

    public bool invertMouse = true;
    public bool invertController = false;

    public float minRotation = -15;
    public float maxRotation = 40;

    float x = 0.0f;
    float y = 0.0f;

	// Use this for initialization
	void Start ()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
	}
	
	// Update is called once per frame
	void Update ()
    {

        // Get controller values
        float turn = Input.GetAxis("Horizontal");
        float look = Input.GetAxis("Vertical");
        bool isMouse = false;

        // Maybe we using a mouse?
        if (turn == 0 && look == 0)
        {
            isMouse = true; 
            turn = Input.GetAxis("Mouse X");
            look = Input.GetAxis("Mouse Y");
        }

        // Determine invert
        int invert = ((isMouse && invertMouse) || (!isMouse && invertController)) ? -1 : 1;
        float sensitivity = isMouse ? mouseSensitivity : controllerSensitivity;

        // Apply changes, clamp y, and update rotation
        x += turn * sensitivity;
        y += look * invert * sensitivity;
        y = ClampAngle(y, minRotation, maxRotation);
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;
	}

    // Mathf.clamp is not sufficient for rotation values
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

}
