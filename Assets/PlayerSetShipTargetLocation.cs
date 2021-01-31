using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ShipController))]
public class PlayerSetShipTargetLocation : MonoBehaviour
{
    public PlayerMovement playerInput;
    public ShipController controller;
    public Transform cursorTransform;
    public Camera mainCamera;
    public LayerMask groundLayers;

    bool lmbDown = false;
    void Awake()
    {
        InitialiseControls();
        GetComponent<Ship>().onDie.AddListener(OnDie);
    }

    void OnDie()
    {
        enabled = false;
    }
    private void InitialiseControls()
    {
        playerInput = new PlayerMovement();
        playerInput.Movement.MouseClick.performed += MouseClick;
        playerInput.Movement.MouseRelease.performed += MouseRelease;

    }
    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }
    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    public void MouseClick(InputAction.CallbackContext ctx)
    {
        Debug.Log("Click");
        lmbDown = true;
        controller.target = cursorTransform;
    }
    public void MouseRelease(InputAction.CallbackContext ctx)
    {
        controller.target = null;
        lmbDown = false;
    }
    public void Update()
    {
        if (lmbDown)
        {
            Ray ray = mainCamera.ScreenPointToRay(playerInput.Movement.MousePosition.ReadValue<Vector2>());
            if (Physics.Raycast(ray, out RaycastHit hit, 50000.0f, groundLayers))
            {
                cursorTransform.position = new Vector3(hit.point.x, 0, hit.point.z);

            }
        }
    }

}
