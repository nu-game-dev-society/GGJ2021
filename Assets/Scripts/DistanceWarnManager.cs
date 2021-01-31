using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DistanceWarnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject warnSegment;

    [SerializeField]
    private SphereCollider detectorCollider;

    [SerializeField]
    private float warnDistance = 10f;

    private SphereCollider trigger;
    private Fleet myFleet;
    private Dictionary<int, GameObject> warnSegments = new Dictionary<int, GameObject>();

    void Start()
    {
        trigger = GetComponent<SphereCollider>();
        myFleet = GetComponentInParent<Ship>().myFleet;
    }

	void FixedUpdate()
	{
        // Update the sphere size
        trigger.radius = detectorCollider.radius + warnDistance;
    }

    private void OnTriggerEnter(Collider other)
	{
        Fleet targetFleet = other.transform.root.GetComponent<Ship>()?.myFleet;
        if (targetFleet != null && targetFleet.isActiveAndEnabled && targetFleet != myFleet && !warnSegments.ContainsKey(targetFleet.transform.GetInstanceID()))
		{
            GameObject gObject = GameObject.Instantiate(warnSegment);
            gObject.transform.parent = transform;
            gObject.transform.localPosition = Vector3.zero;

            DistanceWarnTracker tracker = gObject.GetComponent<DistanceWarnTracker>();
            tracker.target = targetFleet.transform;
            tracker.detectorCollider = detectorCollider;
            tracker.warnDistance = warnDistance;

            warnSegments.Add(targetFleet.transform.GetInstanceID(), gObject);
        }
	}

	private void OnTriggerExit(Collider other)
	{
        Fleet targetFleet = other.transform.root.GetComponent<Ship>()?.myFleet;
        if (targetFleet != null && targetFleet.isActiveAndEnabled && targetFleet != myFleet)
        {
            GameObject segment;
            if (warnSegments.TryGetValue(targetFleet.transform.GetInstanceID(), out segment))
			{
                Destroy(segment);
                warnSegments.Remove(targetFleet.transform.GetInstanceID());
            }
            
        }
    }
}
