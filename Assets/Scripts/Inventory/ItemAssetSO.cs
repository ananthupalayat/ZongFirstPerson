using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemAssetSO : ScriptableObject
{
    
    public GameObject PrefabObject;
    public string ItemDescription;
    public Item.ItemType ItemType;
    public Sprite UISprite;
}
