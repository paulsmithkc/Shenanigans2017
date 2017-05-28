using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesCounterTop : MonoBehaviour
{
    public List<Item> items;

	// Use this for initialization
	void Start ()
    {
        items = new List<Item>();
	}

    void OnTriggerEnter(Collider other)
    {
        var item = other.gameObject.GetComponent<Item>();
        if (item != null)
        {
            items.Add(item);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var item = other.gameObject.GetComponent<Item>();
        if (item != null)
        {
            items.Remove(item);
        }
    }
}
