using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public string colorName;
    public string modelName;
    public Material material;
    public ItemTag itemTag;
    public Rigidbody rigidBody;
    public Collider collider;
    public bool isBought;
    public float itemValue;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        isBought = false;
    }
}
