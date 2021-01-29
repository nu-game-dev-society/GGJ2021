using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ShipController))]
public class PlayerSetShipTargetLocation : MonoBehaviour
{
    public ShipController controller;
    public Transform cursorTransform;
    public Camera mainCamera;
    public LayerMask groundLayers;
    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }
    void Update()
    {
        if (Mouse.current.leftButton.IsPressed())
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, 50000.0f, groundLayers))
            {
                cursorTransform.position = new Vector3(hit.point.x, 0, hit.point.z);
                
            }
        }
        if(Mouse.current.leftButton.wasPressedThisFrame)
            controller.target = cursorTransform;
        if (Mouse.current.leftButton.wasReleasedThisFrame)
            controller.target = null;
    }
}
