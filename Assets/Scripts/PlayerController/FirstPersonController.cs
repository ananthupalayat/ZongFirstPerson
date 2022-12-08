using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zong;

public class FirstPersonController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;

    [Header("Movement Parameters")]
    [SerializeField] 
    private float _walkSpeed = 3.0f;
    [SerializeField]
    private float _gravity = 30f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)]
    private float _lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)]
    private float _lookSpeedY = 2.0f;
    [SerializeField]
    private float _upperLookLimit = 80.0f;
    [SerializeField]
    private float _lowerLookLimit = 80.0f;

    private Camera _playerCamera;
    private CharacterController _characterController;

    private Vector3 _moveDirection;
    private Vector2 _currentInput;

    private float _rotationX;


    bool lockMovement = false;

   

    public void OnEnable()
    {
        Raycaster.OnObjectPicked += LockMovement;
    }

    private void LockMovement(Item item)
    {
        lockMovement = true;
    }





    // Start is called before the first frame update
    void Start()
    {
        _playerCamera = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            ApplyFinalMovements();
        }
    }

    private void HandleMovementInput()
    {
        if (GameManager.Instance.Lock == true)
            return;
        _currentInput = new Vector2(
            _walkSpeed * Input.GetAxis("Vertical"), _walkSpeed * Input.GetAxis("Horizontal"));

        float moveDirectionY = _moveDirection.y;
        _moveDirection = (transform.TransformDirection
            (Vector3.forward) * _currentInput.x) + (transform.TransformDirection(Vector3.right) * _currentInput.y);
        _moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        if (GameManager.Instance.Lock == true)
            return;
        _rotationX -= Input.GetAxis("Mouse Y") * _lookSpeedY;
        _rotationX = Mathf.Clamp(_rotationX, -_upperLookLimit, _lowerLookLimit);
        _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeedX, 0);
    }

    private void ApplyFinalMovements()
    {
        if (!_characterController.isGrounded)
            _moveDirection.y -= _gravity * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);

    }

    
    public void MoveToSpecificCheckPoint(Vector3 checkPointPosition)
    {
        _characterController.enabled = false;
        transform.position = checkPointPosition;
        _characterController.enabled = true;
        
    }

   
}
