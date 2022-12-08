using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    public delegate void Dropped(bool status,Item droppedItem,string boxName);

    public static Dropped OnDropped;

    [SerializeField]
    bool _isAResetBox;

    [SerializeField]
    ParticleSystem _particleEffect;

    [SerializeField]
    string _boxName=default;


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Item droppedItem))
        {
            SpawnParticleEffect();
            OnDropped?.Invoke(_isAResetBox,droppedItem,_boxName);
        }
        
    }

    /// <summary>
    /// Spawn particle effects when dropped into the box
    /// </summary>
    public void SpawnParticleEffect()
    {
        if (_particleEffect == null) 
            return;
        _particleEffect.Play();
    }

}
