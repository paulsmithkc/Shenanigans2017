using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    public ItemColor[] itemColors;
    public Item[] itemModels;
    
    private Item _lastSpawnedItem;

    // Use this for initialization
    void Start() {
        SpawnItem();
    }

    void OnTriggerExit(Collider other)
    {
        if (object.ReferenceEquals(other.gameObject, _lastSpawnedItem.gameObject))
        {
            SpawnItem();
        }
    }

    public void SpawnItem()
    {
        var color = itemColors[Random.Range(0, itemColors.Length)];
        var model = itemModels[Random.Range(0, itemModels.Length)];

        Item obj = GameObject.Instantiate(
            model, transform.position, Quaternion.Euler(-90, 0, 0)
        );
        _lastSpawnedItem = obj;

        obj.colorName = color.colorName;
        obj.material = color.material;

        foreach (var r in obj.GetComponentsInChildren<MeshRenderer>(true))
        {
            r.material = color.material;
        }
    }
}
