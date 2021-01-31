using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdgeWarn : MonoBehaviour
{
    [SerializeField]
    private Transform ship;

    [SerializeField]
    private float warnRadius = 10f;

    [SerializeField]
    private float warnDist = 1f;

    [SerializeField]
    private RawImage warnTexture;

    [SerializeField]
    private Transform warnTextureRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xDiff = ship.position.x - transform.position.x;
        float zDiff = ship.position.z - transform.position.z;

        float dist = Mathf.Sqrt(Mathf.Pow(xDiff, 2) + Mathf.Pow(zDiff, 2));

        if (dist > warnRadius - warnDist)
		{
            float pct = Mathf.Clamp((dist - warnRadius + warnDist) / warnDist, 0f, 1f);
            warnTexture.color = new Color(warnTexture.color.r, warnTexture.color.g, warnTexture.color.b, pct);

            warnTextureRot.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(zDiff, xDiff));
        }
        else
		{
            warnTexture.color = new Color(warnTexture.color.r, warnTexture.color.g, warnTexture.color.b, 0f);
        }
    }

    void OnDrawGizmosSelected()
	{
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, new Vector3(1, 0.01f, 1));

        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(Vector3.zero, warnRadius);

        Gizmos.color = new Color(0, 1, 0);
        Gizmos.DrawWireSphere(Vector3.zero, warnRadius - warnDist);
        Gizmos.matrix = oldMatrix;
    }
}
