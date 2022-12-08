using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
   
    private List<Item> itemList;

    //Event to handle when items are changed in inventory
    public event  EventHandler OnItemListChanged;

    /// <summary>
    /// Create a new invetory
    /// </summary>
    public Inventory()
    {
        itemList = new List<Item>();
    }


    /// <summary>
    /// Add the item to the inventory
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this,  EventArgs.Empty);
    }


    public List<Item> GetItemList()
    {
        return itemList;
    }

    /// <summary>
    /// Remove the item into worldspace
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(Item item)
    {
        if (itemList.Contains(item))
        {
            ItemAssetSO itemAssetSo = (ItemAssetSO)ItemAsset.Instance.GetItem(item);
            var prefab = Instantiate(itemAssetSo.PrefabObject, Camera.main.transform.position+Camera.main.transform.forward*8, Quaternion.identity);
            prefab.GetComponent<ObjectGrabable>().Grab(Camera.main.transform);
            itemList.Remove(item);

            SoundManager.PlaySound(SoundManager.Sound.DropSound);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    
}
