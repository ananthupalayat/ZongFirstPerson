using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zong;

public class GameManager : MonoBehaviour
{


    [SerializeField]
    GameObject _checkPointA;

    [SerializeField]
    FirstPersonController _player;

    [SerializeField]
    List<ParticleSystem> _particleSystems;


    [SerializeField]
    List<GameObject> _checkPointList=new List<GameObject>();

    [SerializeField]
    UI_Inventory _Ui_Inventory;

    Item _item;

    public static GameManager Instance;

    public bool Lock = false;

    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        Raycaster.OnObjectPicked += SetMemory;

        Box.OnDropped += ResetEvent;
    }


    private void OnDisable()
    {
        Raycaster.OnObjectPicked -= SetMemory;

        Box.OnDropped -= ResetEvent;
    }

    /// <summary>
    /// Reset to the point before this
    /// </summary>
    /// <param name="status"></param>
    /// <param name="droppedItem"></param>
    /// <param name="boxname"></param>
    private void ResetEvent(bool status,Item droppedItem,string boxname)
    {
        
        _Ui_Inventory.SetBoxUI(boxname);
        if (!status) return;

        SoundManager.PlaySound(SoundManager.Sound.ResetSound);
        CommandHandler.UndoCommand();
        Destroy(droppedItem.gameObject);
    }

    /// <summary>
    /// Set Key Event
    /// </summary>
    /// <param name="item"></param>
    private void SetMemory(Item item)
    {
        SoundManager.PlaySound(SoundManager.Sound.PickUpObjectSound);
        _Ui_Inventory.SetBoxUI(string.Empty);
        CreateCommand(_checkPointList, _particleSystems, _Ui_Inventory.pInventory, item);
       
    }

    private void CreateCommand(List<GameObject> checkPoints, List<ParticleSystem> particleSystems, Inventory inventory, Item item)
    {
        ICommand command = new KeyEvent(_player, checkPoints, particleSystems, inventory, item, _Ui_Inventory);
        CommandHandler.ExecuteCommand(command);
    }


   public void SetLock(bool status)
    {
        Lock = status;
    }
}
