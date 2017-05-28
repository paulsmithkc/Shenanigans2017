﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FirstPersonCamera : MonoBehaviour
{
    // Sensitivity feels different for mouse and controller
    public float mouseSensitivity = 1;
    public float controllerSensitivity = 1;

    // Invert controls?
    public bool invertMouse = true;
    public bool invertController = false;

    // Constrain vertical camera movement
    public float minRotation = -15;
    public float maxRotation = 40;

    // Maximum reach for picking up objects
    public float maxReach = 10;
    public ItemTooltip itemTooltip = null;
    public ItemTagGenerator labelDispensor = null;

    private Transform heldItem;
    private float heldItemDistance;
    private float previousPickupValue; 

    float x = 0.0f;
    float y = 0.0f;

    private AudioSource audioSource;
    public AudioClip pickupSound;

	// Use this for initialization
	void Start ()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Lock mouse cursor
        Cursor.lockState = CursorLockMode.Locked;

        audioSource = GetComponent<AudioSource>();
	}

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
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

        // Gotta do some shifty bits to get the layer masks to work as expected
        int interactiveOnlyMask = 1 << LayerMask.NameToLayer("Interactive");

        if (Input.GetButtonDown("Label"))
        {
            var itemTag = labelDispensor.DispenseTag();

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxReach, interactiveOnlyMask))
            {
                var item = hit.collider.gameObject.GetComponent<Item>();
                if (item != null && !item.isBought)
                {
                    item.itemTag = itemTag;
                }
            }
        }

        // See if we're trying to pick something up
        float pickup = Input.GetAxis("Pickup");
        if (pickup != 0 && previousPickupValue == 0)
        {
            if (heldItem == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, maxReach, interactiveOnlyMask))
                {
                    Pickup(hit.transform);
                }
            }
            else
            {
                DropItem();
            }
            HideTooltip();
        }
        else if (heldItem == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxReach, interactiveOnlyMask))
            {
                ShowTooltip(hit);
            }
            else
            {
                HideTooltip();
            }
        }

        // Update position of held item
        if (heldItem != null)
        {
            heldItem.position = transform.position + transform.forward * heldItemDistance;
        }

        // Update previous value
        previousPickupValue = pickup;
	}

    void Pickup(Transform target)
    {
        heldItem = target;
        heldItemDistance = Vector3.Distance(transform.position, target.position);

        // Turn off gravity, or else shit gets weird
        heldItem.gameObject.GetComponent<Rigidbody>().useGravity = false;

        if (audioSource != null && pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound, 1);
        }
    }

    void DropItem()
    {
        if (heldItem != null)
        {
            // Turn the gravities back on!
            heldItem.gameObject.GetComponent<Rigidbody>().useGravity = true;
            heldItem = null;
        }
    }

    void ShowTooltip(RaycastHit hit)
    {
        if (itemTooltip != null)
        {
            itemTooltip.transform.position = hit.point - 0.3f * transform.forward;
            itemTooltip.transform.rotation = transform.rotation;

            itemTooltip.ShowTooltip(hit.collider.gameObject);
        }
    }

    void HideTooltip()
    {
        if (itemTooltip != null)
        {
            itemTooltip.gameObject.SetActive(false);
        }
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
