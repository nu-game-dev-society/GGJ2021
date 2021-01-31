using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetManager : MonoBehaviour
{

    public static List<Fleet> activeFleets;
    public static Transform player;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Awake()
    {
        activeFleets = new List<Fleet>(); 
        activeFleets.AddRange(FindObjectsOfType<Fleet>());


        player = playerTransform;
    }



}
