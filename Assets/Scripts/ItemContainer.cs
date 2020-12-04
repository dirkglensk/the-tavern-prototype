using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    private ItemStorageSlot[] internalStorageSlots;
    // Start is called before the first frame update
    void Start()
    {
        var slotObjects = transform.GetComponentsInChildren<ItemStorageSlot>();
        internalStorageSlots = slotObjects;

        //storageSlots[5].Item
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}