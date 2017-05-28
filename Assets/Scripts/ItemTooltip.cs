using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    public Text itemNameField;
    public Text itemDescriptionField;
    public Slider itemValueField;

    public void ShowTag(ItemTag itemTag)
    {
        if (itemTag != null)
        {
            itemNameField.text = itemTag.itemName;
            itemDescriptionField.text = itemTag.itemDescription;
            itemValueField.value = 0;
            gameObject.SetActive(true);
        }
        else
        {
            itemNameField.text = "??";
            itemDescriptionField.text = "???";
            itemValueField.value = 0;
            gameObject.SetActive(true);
        }
    }

    public void ShowTooltip(GameObject obj)
    {
        var item = obj.GetComponent<Item>();
        if (item != null)
        {
            var itemTag = item.itemTag;
            if (itemTag != null && !string.IsNullOrEmpty(itemTag.itemName))
            {
                itemNameField.text = itemTag.itemName;
                itemDescriptionField.text = itemTag.itemDescription;
                itemValueField.value = item.itemValue;
                gameObject.SetActive(true);
            }
            else
            {
                itemNameField.text = item.colorName + " " + item.modelName;
                itemDescriptionField.text = "???";
                itemValueField.value = item.itemValue;
                gameObject.SetActive(true);
            }
        }
        else
        {
            itemNameField.text = "???";
            itemDescriptionField.text = "???";
            itemValueField.value = 0;
            gameObject.SetActive(true);
        }
    }
}
