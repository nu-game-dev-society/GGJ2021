using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ShipController : MonoBehaviour
{
    public float accelerationSpeed;
    public float maxSpeed;
    public float turnSpeed;

    public Transform target;
    public NavMeshAgent agent;

    public ParticleSystem particles;

    private void Update()
    {
        if (agent.speed > 0.8f && !particles.isPlaying)
            particles.Play();
        else if (agent.speed < 0.8f && particles.isPlaying)
            particles.Stop();

        ParticleSystem.MainModule main = particles.main;
        main.startRotation = Mathf.Deg2Rad * (transform.eulerAngles.y);
 
        if (target)
        {
            agent.speed = Mathf.Lerp(agent.speed, maxSpeed, Time.deltaTime * accelerationSpeed);
            agent.angularSpeed = Mathf.Lerp(agent.angularSpeed, turnSpeed, Time.deltaTime);
            agent.destination = target.position;
        }
        else
        {
            agent.speed = Mathf.Lerp(agent.speed, 0, Time.deltaTime * accelerationSpeed);
            agent.angularSpeed = Mathf.Lerp(agent.angularSpeed, 0, Time.deltaTime); 
        }
    }

    private void OnDrawGizmos()
    {
        if (target != null)
            Gizmos.DrawWireSphere(target.position, 2.0f);
    }
}