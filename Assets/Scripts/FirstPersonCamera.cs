using System.Collections;
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

    private GameObject heldItem;
    private float heldItemDistance;

    public float x = 0.0f;
    public float y = 0.0f;

    private AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip labelSound;

    // Use this for initialization
    void Start ()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        
        audioSource = GetComponent<AudioSource>();
	}

    private void OnDestroy()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool paused = Time.timeScale == 0.0f;
        if (paused) { return; }

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

        if (Input.GetButtonDown("Label") ||
            Input.GetButtonDown("Pickup"))
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

            if (audioSource != null && labelSound != null)
            {
                audioSource.PlayOneShot(labelSound, 1);
            }
        }

        // See if we're trying to pick something up
        if (Input.GetButtonDown("Pickup"))
        {
            if (heldItem == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, maxReach, interactiveOnlyMask))
                {
                    Pickup(hit.transform.gameObject);
                }
            }
            else
            {
                DropItem();
            }
            HideTooltip();
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxReach, interactiveOnlyMask))
            {
                if (itemTooltip != null)
                {
                    //itemTooltip.transform.parent = this.transform;
                    itemTooltip.transform.position = hit.point - 0.3f * transform.forward;
                    itemTooltip.transform.rotation = transform.rotation;
                    itemTooltip.ShowTooltip(hit.transform.gameObject);
                }
            }
            else if (heldItem != null)
            {
                ShowTooltip(heldItem);
            }
            else
            {
                HideTooltip();
            }
        }

        // Update position of held item
        if (heldItem != null)
        {
            heldItem.transform.position = transform.position + transform.forward * heldItemDistance;
        }
	}

    void Pickup(GameObject target)
    {
        heldItem = target;
        heldItemDistance = Vector3.Distance(transform.position, target.transform.position);

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

    void ShowTooltip(GameObject obj)
    {
        if (itemTooltip != null)
        {
            itemTooltip.ShowTooltip(obj);
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
