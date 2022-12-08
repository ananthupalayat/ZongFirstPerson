using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instrument : MonoBehaviour
{
    TextMeshPro _itemDescriptonText;

    string _itemDescription;

    public void Awake()
    {
        _itemDescriptonText = GetComponentInChildren<TextMeshPro>();
    }
    public void SetDescription()
    {
        _itemDescriptonText.SetText(_itemDescription);
    }

   /// <summary>
   /// Remove item description while grabbing
   /// </summary>
   public void RemoveDescription()
   {
        _itemDescriptonText.SetText(string.Empty);
   }

    /// <summary>
    /// Set Item description wgeb spawned in world space
    /// </summary>
    /// <param name="itemDescription"></param>
    public void Initialise(string itemDescription)
    {
        _itemDescription = itemDescription;
        _itemDescriptonText.SetText(_itemDescription);
    }

    
}
