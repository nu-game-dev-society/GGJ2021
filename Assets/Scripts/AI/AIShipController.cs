using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ShipController))]
public class AIShipController : MonoBehaviour
{
    float walkRadius = 100f;
    ShipController ShipController;

    // Start is called before the first frame update
    void Start()
    {
        ShipController = GetComponent<ShipController>();
        if(ShipController.target == null)
        {
            ShipController.target = Instantiate(new GameObject()).transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ShipController.agent.remainingDistance < 5f)
        {
            ShipController.target.position = GetRandomPointInNavMesh();
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
