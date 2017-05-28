using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesCounterFront : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (string.Equals(tag, "Hero"))
        {
            var hero = other.gameObject.GetComponent<Hero>();
            if (hero != null)
            {
                hero._isAtCounter = true;
            }
        }
    }
}
