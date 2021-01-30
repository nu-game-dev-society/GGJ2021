using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ShipController))]
public class AIShipController : MonoBehaviour
{
    float walkRadius = 50f;
    ShipController ShipController;

    // Start is called before the first frame update
    void Start()
    {
        ShipController = GetComponent<ShipController>();
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
        Vector3 randomDirection = Random.onUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        return hit.position;
    }
}
