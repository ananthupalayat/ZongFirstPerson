using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycaster : MonoBehaviour
{

    [SerializeField]
    private LayerMask _interactableLayer = default;

    [SerializeField]
    private float _interactionDistance=default;

    [SerializeField]
    private KeyCode _interactionKey = KeyCode.Mouse0;

    ObjectGrabable _objectGrabbable=null;


    public delegate void ObjectPicked(Item item);
    public static ObjectPicked OnObjectPicked;

    bool Grabbed = false;


    private void Awake()
    {
        Grabbed = false;
    }
    // Update is called once per frame
    void Update()
    {
        
        HandInteractionInput();
    }


    private void HandInteractionInput()
    {

        if (Input.GetKeyDown(_interactionKey))
        {
            if (_objectGrabbable == null)
            {
                RaycastHit hit;
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _interactableLayer))
                {

                    if (hit.transform.TryGetComponent(out _objectGrabbable))
                    {
                        if (_objectGrabbable.IsGrabbed==false)
                        {
                            Item grabbedItem = _objectGrabbable.GetComponent<Item>();
                            OnObjectPicked?.Invoke(grabbedItem);
                            _objectGrabbable.IsGrabbed = true;
                        }
                        else
                        {
                            _objectGrabbable.Drop();
                            _objectGrabbable.IsGrabbed = false;
                            _objectGrabbable = null;
                        }
                        
                    }
                }
            }
            
        }
        
    }

    
}
