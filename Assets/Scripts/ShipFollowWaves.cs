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

            Vector3 vectorRot = boatBase.transform.localEulerAngles;
            vectorRot.y = 0f;
            vectorRot.x = Mathf.Clamp(newRot.x, -3f, 3f);
            vectorRot.z = Mathf.Clamp(newRot.z, -3f, 3f);

            boatBase.transform.localEulerAngles = vectorRot; 
        }
    }
}
