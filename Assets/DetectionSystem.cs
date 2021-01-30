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
        myFleet = GetComponentInParent<Fleet>();
    }
    private void OnTriggerEnter(Collider other)
    {
        myFleet.AddTargetFleet(other.transform.root.GetComponent<Ship>().myFleet); //add layer check to add in collectibles etc.
    }
    private void OnTriggerExit(Collider other)
    {
        myFleet.RemoveTargetFleet(other.transform.root.GetComponent<Ship>().myFleet);
    }

    public void CalculcateRadius(Vector3 midpoint)
    {
        transform.localPosition = midpoint;
        trigger.radius = transform.localPosition.z + 18;
    }
}
