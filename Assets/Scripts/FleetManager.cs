using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetManager : MonoBehaviour
{
    [SerializeField]
    private float attackRange = 20f;

    private Fleet[] fleets;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateFleets", 0f, 1f);
    }

    void FixedUpdate()
    {
        foreach (Fleet fleet1 in fleets)
		{
            foreach (Fleet fleet2 in fleets)
            {
                if (Vector3.Distance(fleet1.center, fleet2.center) <= attackRange)
				{
                    fleet1.setTargetFleet(fleet2);
                    break;
				}
            }
        }
    }

    void UpdateFleets()
	{
        fleets = GameObject.FindObjectsOfType<Fleet>();
	}
}
