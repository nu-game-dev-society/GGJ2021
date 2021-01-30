using System;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    public float accelerationSpeed;
    public float maxSpeed;

    public Transform target;
    public NavMeshAgent agent;

    Vector3 lastDir;
    private void Update()
    {
        if (target)
        {
            agent.speed = Mathf.Lerp(agent.speed, maxSpeed, Time.deltaTime * accelerationSpeed);
            agent.destination = target.position;
            lastDir = transform.position - target.position;
        }
        else
        {
            agent.speed = Mathf.Lerp(agent.speed, 0, Time.deltaTime * accelerationSpeed);
            agent.destination = transform.position;
        }
    }
    private void OnDrawGizmos()
    {
        if (target != null)
            Gizmos.DrawWireSphere(target.position, 2.0f);
    }
}