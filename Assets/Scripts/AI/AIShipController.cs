using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ShipController))]
public class AIShipController : MonoBehaviour
{
    float walkRadius = 100f;
    ShipController shipController;

    // Start is called before the first frame update
    void Start()
    {
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
        if(shipController.agent.remainingDistance < 5f)
        {
            shipController.target.position = GetRandomPointInNavMesh();
        }
    }

    Vector3 GetRandomPointInNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitCircle.normalized * walkRadius;
        randomDirection += transform.position;

        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkRadius, 1);
        return hit.position;
    }
}
