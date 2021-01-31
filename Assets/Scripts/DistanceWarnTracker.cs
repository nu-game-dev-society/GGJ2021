using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceWarnTracker : MonoBehaviour
{
    public Transform target;

    public SphereCollider detectorCollider;

    public float warnDistance;

    [SerializeField]
    private MeshRenderer warnRenderer;

    private Material warnMaterial;

    // Start is called before the first frame update
    void Start()
    {
        warnMaterial = new Material(warnRenderer.material);
        warnRenderer.material = warnMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
		{
            Destroy(gameObject);
		}

        transform.LookAt(target);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);

        float dist = Vector3.Distance(transform.position, target.position);

        float pct = Mathf.Clamp((warnDistance - (dist - detectorCollider.radius)) / warnDistance, 0f, 1f);
        warnMaterial.color = new Color(warnMaterial.color.r, warnMaterial.color.g, warnMaterial.color.b, pct);
    }
}
