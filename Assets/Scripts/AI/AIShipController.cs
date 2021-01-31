using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ShipController))]
public class AIShipController : MonoBehaviour
{
    float walkRadius = 100f;
    ShipController shipController;
    Ship ship;
    float nextCheckTargetTime;

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Ship>();
        shipController = GetComponent<ShipController>();
        if(shipController.target == null)
        {
            GameObject targ = new GameObject("AIShipTarget");
            targ.transform.parent = (GameManager.instance.transform);
            shipController.target = targ.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextCheckTargetTime)
        {
            Debug.Log("Updating target");
            nextCheckTargetTime = Time.time + 5f;
            HeadAwayOrTowardsNearestFleet(GetNearestFleet());
            //shipController.target.position = GetRandomPointInNavMesh();
        }
    }

    public void HeadAwayOrTowardsNearestFleet(Fleet fleet)
    {
        if (!fleet)
            return;

        if (ship.myFleet == null)
            return;

        if (fleet.ships.Count <= ship.myFleet.ships.Count)
        {
            shipController.target.position = fleet.center + (3 * (fleet.center - transform.position).normalized);
        }
        else
        {
            shipController.target.position = transform.position + (3 * (fleet.center - transform.position).normalized);
        }
    }

    public Fleet GetNearestFleet()
    {
        return GameManager.instance.GetNearestFleetToPosition(ship.myFleet.center, ship.myFleet);
    }

    Vector3 GetRandomPointInNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitCircle.normalized * walkRadius;
        randomDirection += transform.position;

        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkRadius, 1);
        return hit.position;
    }
}
