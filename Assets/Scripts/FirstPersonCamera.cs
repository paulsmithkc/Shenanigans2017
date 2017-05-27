using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    public float mouseSensitivity = 1;
    public float controllerSensitivity = 1;

    public bool invertMouse = true;
    public bool invertController = false;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {

        float turn = Input.GetAxis("Horizontal");
        if (turn != 0)
        {
            Turn(turn, false);
        }
        else
        {
            turn = Input.GetAxis("Mouse X");
            if (turn != 0)
            {
                Turn(turn, true);
            }
        }

        float look = Input.GetAxis("Vertical");
        if (look != 0)
        {
            Look(look, false);
        }
        else
        {
            look = Input.GetAxis("Mouse Y");
            if (look != 0)
            {
                Look(look, true);
            }
        }
	}

    void Look(float amount, bool isMouse)
    {
        float sensitivity = isMouse ? mouseSensitivity : controllerSensitivity;
        int invert = ((isMouse && invertMouse) || (!isMouse && invertController)) ? -1 : 1;
        transform.eulerAngles += new Vector3(invert * amount, 0, 0) * sensitivity;
    }

    void Turn(float amount, bool isMouse)
    {
        float sensitivity = isMouse ? mouseSensitivity : controllerSensitivity;
        transform.eulerAngles += new Vector3(0, amount, 0) * sensitivity;
    }

}
