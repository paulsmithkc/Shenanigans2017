using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTag : MonoBehaviour {

    public string itemName;
    public string itemDescription;

    // Use this for initialization
    void Start () {
        randomize();
    }

    public void randomize()
    {
        var rand = new Random();
        this.itemName = "dangerous vial of poison";
        this.itemDescription = string.Format("kills rats in {0} seconds", Random.Range(1, 5));
    }
}
