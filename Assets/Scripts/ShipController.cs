using System;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    //public IntVariable playerShips;
    //public Transform target = null;
    //public Rigidbody rb;

    //public float rotationSpeed;
    //public float accelerationSpeed;
    //public float maxSpeed;
    //private float speed = 0.0f;

    //void Start()
    //{

    //    if (rb == null)
    //        rb = GetComponent<Rigidbody>();
    //}

    //void Update()
    //{
    //    if (target != null)
    //    {
    //        MoveTowards();
    //    }
    //    else
    //    {
    //        speed = rb.velocity.magnitude;
    //    }
    //}

    //private void MoveTowards()
    //{
    //    if (Vector3.Distance(transform.position, target.position) < 2.0f)
    //    {
    //        speed = Mathf.Clamp(Vector3.Distance(transform.position, target.position) - 1.0f, 0.0f, 100.0f);
    //    }
    //    else
    //    {
    //        speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * accelerationSpeed);
    //    }


    //    var lookPos = target.position - transform.position;
    //    lookPos.y = 0;
    //    var rotation = Quaternion.LookRotation(lookPos);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed * Mathf.Clamp(speed, 0.0f, 1.0f));

    //    rb.velocity = transform.forward * speed;
    //}

    public IntVariable playerShips;

    public Transform target;
    public NavMeshAgent agent;


    private void Update()
    {
        if(target)
            agent.destination = target.position;
        else
            agent.destination = transform.position;
    }
    private void OnDrawGizmos()
    {
        if (target != null)
            Gizmos.DrawWireSphere(target.position, 2.0f);
    }
}