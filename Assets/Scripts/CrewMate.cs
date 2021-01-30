using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMate : MonoBehaviour
{
    public Vector3 startPoint;
    public Transform movementTarget;
    Vector3 startScale;

    public void Start()
    {
        startPoint = transform.localPosition;
        startScale = transform.localScale; 
    }

    public void Update()
    {
        transform.localPosition = startPoint + ( (movementTarget.localPosition - startPoint) * Mathf.PingPong((Time.time * 0.5f), 1f));


        transform.localScale = startScale * (0.8f + Mathf.PingPong((Time.time * 0.5f), 0.2f));
    }
}
