using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    public Inventory pInventory;

    [SerializeField]
    Transform _itemSlotContainer;
    [SerializeField]
    Transform _itemSlotTemplate;

    [SerializeField]
    GameObject _mainUI;

    [SerializeField]
    TextMeshProUGUI _boxText;

    //set this value to determine the number of slots in one row
    [SerializeField, Range(1, 5)]
    int _rowCount = 4;

    public void AddItemAndSetUI(Item item)
    {
        GameManager.Instance.SetLock(true);
        _mainUI.SetActive(true);
        pInventory.AddItem(item);

        if (item != null)
        {
            Destroy(item.gameObject);
        }
    }

    public void RemoveItemAndSetUI(Item item)
    {
        _mainUI.SetActive(true);
        pInventory.RemoveItem(item);
    }

    

    private void Start()
    {
        pInventory = new Inventory();
        SetInventory(pInventory);
    }

    public void SetInventory(Inventory inventory)
    {
        this.pInventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    /// <summary>
    /// Refresh inventory items 
    /// </summary>
    private void RefreshInventoryItems()
    {
        foreach (Transform  child in _itemSlotContainer)
        {
            if (child == _itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 75f;
        foreach(Item item in pInventory.GetItemList())
        {
            RectTransform itemSlotRectTransform =
                Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.GetComponent<Image>().sprite = ItemAsset.Instance.GetSprite(item);
            itemSlotRectTransform.anchoredPosition =
                new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            itemSlotRectTransform.gameObject.GetComponent<Button>().onClick.AddListener(() => { pInventory.RemoveItem(item);_mainUI.SetActive(false);GameManager.Instance.SetLock(false); });
            x++;
            if (x > _rowCount)
            {
                x = 0;
                y++;
            }
        }
    }

    /// <summary>
    /// Set Description about the box where stone is dropped into
    /// </summary>
    /// <param name="boxName"></param>
    public void SetBoxUI(string boxName)
    {
        if (boxName == string.Empty)
        {
            _boxText.SetText(string.Empty);
        }
        else
        {
            _boxText.SetText("Dropped in " + boxName);
        }
    }

    /// <summary>
    /// UI Button call back to turn on main UI
    /// </summary>
    public void ShowMainUI()
    {
        GameManager.Instance.SetLock(true);
        _mainUI.SetActive(true);
        
    }
}
