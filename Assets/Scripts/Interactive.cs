using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Interactive : MonoBehaviour {

    private AudioSource landingSound;
    private Rigidbody rb;


    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        landingSound = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.useGravity)
        {
            landingSound.Play();
        }
    }
}
