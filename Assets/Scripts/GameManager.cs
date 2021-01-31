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



}
