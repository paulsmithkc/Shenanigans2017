using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Interactive : MonoBehaviour {

    public AudioClip landingSound;
    public AudioClip crashSound;
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private bool ready;


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        ready = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (string.Equals(collision.gameObject.tag, "Floor"))
        {
            AudioSource.PlayClipAtPoint(crashSound, collision.contacts[0].point, 1);
            GameObject.Destroy(gameObject);
        }
        else if (ready && rigidBody.useGravity && landingSound != null)
        {
            AudioSource.PlayClipAtPoint(landingSound, collision.contacts[0].point, 1);
        }
        else if (!ready)
        {
            ready = true;
        }
    }
}
