using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]public List<Fleet> activeFleets;
    public Transform player;

    public float playSpaceRadius;
    public GameObject turnAroundWarning;

    public Volume volume;
    ColorAdjustments colorAdjustments;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;

        activeFleets = new List<Fleet>(); 
        activeFleets.AddRange(FindObjectsOfType<Fleet>());

        ColorAdjustments tmp;
        if (volume.profile.TryGet<ColorAdjustments>(out tmp))
        {
            colorAdjustments = tmp;
        }
    }

    public void Update()
    {
        float playerDistance = Vector3.Distance(Vector3.zero, player.transform.position);

        if (playerDistance > playSpaceRadius)
        {
            turnAroundWarning.SetActive(true);
        }
        else
        {
            turnAroundWarning.SetActive(false);
        }

        float perc = ((playerDistance - playSpaceRadius) / 60f);

        perc = Mathf.Clamp01(perc);

        if (colorAdjustments)
            colorAdjustments.saturation.value = -100 * perc;
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

    public void OnDestroy()
    {
        if (colorAdjustments)
            colorAdjustments.saturation.value = 0f;
    }

}
