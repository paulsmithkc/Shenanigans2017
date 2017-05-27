using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Interactive : MonoBehaviour {

    private AudioSource landingSound;
    private Rigidbody rb;
    private bool ready = false;


    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        landingSound = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (ready && rb.useGravity)
        {
            landingSound.Play();
        }

        if (!ready)
        {
            ready = true;
        }
    }
}
