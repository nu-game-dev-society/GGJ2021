using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]public List<Fleet> activeFleets;
    public Transform player;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;

        activeFleets = new List<Fleet>(); 
        activeFleets.AddRange(FindObjectsOfType<Fleet>());
    }

    public Fleet GetNearestFleetToPosition(Vector3 pos, Fleet shipFleet)
    {
        Fleet nearest = null;
        float closestDist = float.MaxValue;

        foreach (Fleet fleet in activeFleets)
        {
            if (fleet != shipFleet)
            {
                float dist = Vector3.Distance(pos, fleet.center);

                if (dist < closestDist)
                {
                    closestDist = dist;
                    nearest = fleet;
                }
            }
        }

        return nearest;
    }

}
