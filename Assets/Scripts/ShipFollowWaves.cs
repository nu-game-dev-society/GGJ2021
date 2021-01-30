using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFollowWaves : MonoBehaviour
{
    public Waves waveMesh;

    public Transform raycastPoint;
    public LayerMask layerMask;

    private void Update()
    {
        GetHeight();
    }

    public void GetHeight()
    {
        RaycastHit hit;
       
        if (Physics.Raycast(raycastPoint.position, Vector3.down, out hit, 100, layerMask))
        {
            Vector3 pos = transform.position;
            pos.y = hit.point.y;

            transform.position = pos;
        }
    }
}
