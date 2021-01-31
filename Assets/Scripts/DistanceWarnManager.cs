using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private Dictionary<Fleet, GameObject> warnSegments = new Dictionary<Fleet, GameObject>();

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
        if (targetFleet != null && targetFleet.isActiveAndEnabled && targetFleet != myFleet && !warnSegments.ContainsKey(targetFleet))
        {
            GameObject gObject = Instantiate(warnSegment);
            gObject.transform.parent = transform;
            gObject.transform.localPosition = Vector3.zero;

            DistanceWarnTracker tracker = gObject.GetComponent<DistanceWarnTracker>();
            tracker.target = targetFleet.transform;
            tracker.detectorCollider = detectorCollider;
            tracker.warnDistance = warnDistance;

            warnSegments.Add(targetFleet, gObject);
            targetFleet.fleetDestroyed.AddListener(RemoveFromList);
        }
    }

    public void RemoveFromList()
    {
        List<Fleet> fleets = warnSegments.Keys.ToList();
        foreach (Fleet f in fleets)
        {
            if (f == null || f.liveShipsCount == 0)
                RemoveFleet(f);
        }
    }
    void RemoveFleet(Fleet f)
    {
        if (warnSegments.TryGetValue(f, out GameObject segment))
        {
            Destroy(segment);
            warnSegments.Remove(f);
            f.fleetDestroyed.RemoveListener(RemoveFromList);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Fleet targetFleet = other.transform.root.GetComponent<Ship>()?.myFleet;
        if (targetFleet != null && targetFleet.isActiveAndEnabled && targetFleet != myFleet)
        {
            RemoveFleet(targetFleet);
        }
    }
}
