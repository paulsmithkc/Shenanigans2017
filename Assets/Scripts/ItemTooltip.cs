using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    public Text itemNameField;
    public Text itemDescriptionField;

    public void ShowTooltip(GameObject obj)
    {
        var itemTag = obj.GetComponent<ItemTag>();
        var item = obj.GetComponent<Item>();
        if (itemTag != null)
        {
            itemNameField.text = itemTag.itemName;
            itemDescriptionField.text = itemTag.itemDescription;
            gameObject.SetActive(true);
        }
        else if (item != null)
        {
            itemNameField.text = item.colorName + " " + item.modelName;
            itemDescriptionField.text = "???";
            gameObject.SetActive(true);
        }
        else
        {
            itemNameField.text = "???";
            itemDescriptionField.text = "???";
            gameObject.SetActive(true);
        }
    }
}
