using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{
    public enum fleetColour { red, blue, green };
    public fleetColour colour;
    public List<Ship> ships;
    public Vector3 center;

    public Fleet(fleetColour colour)
    {
        this.colour = colour;
        ships = new List<Ship>();
    }

    public void addShip(Ship ship)
    {
        ships.Add(ship);
    }

    public void removeShip(Ship ship)
    {
        ships.Remove(ship);
    }

    public int getCount()
    {
        return ships.Count;
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
