using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFollowWaves : MonoBehaviour
{
    public Waves waveMesh;

    public Transform boatBase;
    public LayerMask layerMask;

    private void Update()
    {
        GetHeight();
    }

    public void GetHeight()
    {
        RaycastHit hit;

        Vector3 raycastPoint = transform.position + Vector3.up;

        if (Physics.Raycast(raycastPoint, Vector3.down, out hit, 100, layerMask))
        {
            Vector3 pos = boatBase.transform.position;
            pos.y = hit.point.y;

            boatBase.transform.position = pos;

            Quaternion newRot = Quaternion.FromToRotation(Vector3.up, hit.normal);

            boatBase.transform.rotation = Quaternion.Lerp(boatBase.transform.rotation, newRot, 1 * Time.deltaTime);

            newRot = boatBase.transform.localRotation;
            newRot.y = 0f;

            boatBase.transform.localRotation = newRot; 
        }
    }
}
