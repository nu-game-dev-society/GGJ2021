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

        Fleet otherFleet = other.transform.root.GetComponent<Ship>()?.myFleet;
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

        Fleet otherFleet = other.transform.root.GetComponent<Ship>()?.myFleet;
        if (otherFleet != null)
        {
            myFleet.RemoveTargetFleet(otherFleet); //add layer check to add in collectibles etc.
        }
    }

    public void CalculateRadius()
    {
        int indexOfLastLivingShip = 1;
        for(int i=myFleet.ships.Count-1; i >= 0; --i)
        {
            if(myFleet.ships[i] !=null)
            {
                indexOfLastLivingShip = i;
                break;
            }
        }

        Vector3 midpoint = Vector3.zero;
        midpoint.z = ShipTargetPositionLocator.GetShipTargetPosition(indexOfLastLivingShip).z * 0.75f;
        transform.localPosition = midpoint;

        int lastRow = TriangleNumbers.GetRowInTriangle(indexOfLastLivingShip);

        // ********************************************************************************
        // DON'T DELETE - TOOK AGES TO FIGURE OUT *****************************************
        // ********************************************************************************
        //int lastRowStart = TriangleNumbers.GetTriangleNumber(lastRow - 1) + 1;
        //int lastRowCount = TriangleNumbers.GetCountInRow(lastRow);

        //Vector3 leftMost = ShipTargetPositionLocator.GetShipTargetPosition(lastRowStart);
        //Vector3 rightMost = ShipTargetPositionLocator.GetShipTargetPosition(lastRowStart + lastRowCount -1);

        //float lastRowWidth = Vector3.Distance(leftMost, rightMost);
        trigger.radius = ShipTargetPositionLocator.GAPSIZE * (lastRow+1) * 0.5f;
        //Debug.Log($"Detection Sphere radius is now: {trigger.radius}");

    
    }
   
    /// <summary>
    /// Gets a random point on the circumference of this fleet's detection circle
    /// </summary>
    /// <returns></returns>
    public Vector3 GetRandomPointOnDetectionCircumference()
    {
        Vector2 randomPointOnUnitCircle = (UnityEngine.Random.insideUnitCircle.normalized * trigger.radius);
        return new Vector3()
        {
            x = transform.position.x + randomPointOnUnitCircle.x,
            y = transform.position.y + 0,
            z = transform.position.z + randomPointOnUnitCircle.y,
        };
    }
}
