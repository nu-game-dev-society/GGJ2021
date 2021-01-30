using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float damping;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point = Vector3.Lerp(transform.position, target.position + offset, Time.time * damping);
        transform.position = point;
        transform.LookAt(point);
    }
}
