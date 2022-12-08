using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectGrabable : MonoBehaviour
{


    Rigidbody _rigidbody;

    Transform _moverTransform;


    public bool IsGrabbed = false;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Drop object when clicked on it
    /// </summary>
    public void Drop()
    {
        _moverTransform = null;
        _rigidbody.useGravity = true;
        GetComponent<Instrument>().SetDescription();
    }

    /// <summary>
    /// Grab object when clicked on it
    /// </summary>
    /// <param name="newTransform"></param>
    public void Grab(Transform newTransform)
    {
        IsGrabbed = true;
        _moverTransform = null;
        _moverTransform = newTransform;
        _rigidbody.useGravity = false;
        GetComponent<Instrument>().RemoveDescription();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_moverTransform != null)
        {
            Vector3 newPosition = Vector3.Slerp(transform.position, _moverTransform.position+_moverTransform.forward*3, Time.deltaTime * 10
                );
            _rigidbody.MovePosition(newPosition);
        }
    }
}
