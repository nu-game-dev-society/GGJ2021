using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    Fleet myFleet;

    SphereCollider trigger;
    private void Start()
    {
        trigger = GetComponent<SphereCollider>();
        myFleet = GetComponentInParent<Ship>().myFleet;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(myFleet == null)
        {
            myFleet = GetComponentInParent<Ship>().myFleet;
            if(myFleet == null) { return; }
        }

        Fleet otherFleet = other?.transform?.root?.GetComponent<Ship>()?.myFleet;
        if (otherFleet != null)
        {
            myFleet.AddTargetFleet(otherFleet); //add layer check to add in collectibles etc.
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (myFleet == null)
        {
            myFleet = GetComponentInParent<Ship>().myFleet;
            if (myFleet == null) { return; }
        }

        Fleet otherFleet = other?.transform?.root?.GetComponent<Ship>()?.myFleet;
        if (otherFleet != null)
        {
            myFleet.RemoveTargetFleet(otherFleet); //add layer check to add in collectibles etc.
        }
    }

    public void CalculcateRadius(Vector3 midpoint)
    {
        transform.localPosition = midpoint;

        int lastRow = TriangleNumbers.GetRowInTriangle(myFleet.ships.Count+1);
        int lastRowStart = TriangleNumbers.GetTriangleNumber(lastRow-1)+1; 
        int lastRowCount = TriangleNumbers.GetCountInRow(lastRow);
        
        float lastRowWidth = Vector3.Distance(ShipTargetPositionLocator.GetShipTargetPosition(lastRowStart + lastRowCount), 
                                              ShipTargetPositionLocator.GetShipTargetPosition(lastRowStart));
        trigger.radius = lastRowWidth + 18;
        Debug.Log($"Detection Sphere radius is now: {trigger.radius}");
    }
}
