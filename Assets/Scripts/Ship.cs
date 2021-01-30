using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float health;
    public enum fleetColour{ red, blue, green };
    public float strength;

    public fleetColour fleet;

    public Ship(fleetColour myFleet)
    {
        health = 100;
        strength = 2;
        fleet = myFleet;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
