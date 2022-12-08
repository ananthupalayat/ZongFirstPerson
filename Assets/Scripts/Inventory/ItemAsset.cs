using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemAsset : MonoBehaviour
{

    [SerializeField]
    List<ItemAssetSO> _assetList=new List<ItemAssetSO>();

    /// <summary>
    /// Singleton to items at runtime
    /// </summary>
    public static ItemAsset Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public ScriptableObject GetItem(Item item)
    {
        ItemAssetSO newAsset = _assetList.FirstOrDefault(r => r.ItemType == item.itemType);
        return newAsset;
    }

    public Sprite GetSprite(Item item)
    {
        var requiredSprite = _assetList.FirstOrDefault(r => r.ItemType == item.itemType);
        return requiredSprite.UISprite;
    }
   
}
