using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemTag {

    public string itemName;
    public string itemDescription;

    public override string ToString()
    {
        return string.Format("{0}\n\n{1}", itemName, itemDescription);
    }
}
