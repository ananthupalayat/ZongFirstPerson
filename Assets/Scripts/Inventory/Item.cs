using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType itemType;
    /// <summary>
    /// Set item type in the inspector
    /// </summary>
    public enum ItemType
    {
        Stone,
        Sword,
        Staff,
        Stick
    }
   
}
