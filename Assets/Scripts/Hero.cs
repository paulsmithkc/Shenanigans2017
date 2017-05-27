using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    public float buffer;
    public float stepDistance;

    public Transform following;
	
	// Update is called once per frame
	void Update ()
    {
        if (following != null)
        {
            Vector3 direction = (transform.position - following.position).normalized;
            Vector3 target = following.position + direction * buffer;
            Debug.DrawLine(following.position, target);
            transform.position = Vector3.MoveTowards(transform.position, target, stepDistance);
        }
	}

}
