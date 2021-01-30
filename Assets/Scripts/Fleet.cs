using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{
    public enum fleetColour { red, blue, green };
    public fleetColour colour;
    public List<Ship> ships; //maybe hashset optimise
    public Vector3 center;

    private Fleet targetFleet;

    private void Awake()
    {
        ships = new List<Ship>();
        center = transform.position;
    }

    public Fleet(fleetColour colour)
    {
        this.colour = colour;
    }

    public void removeShip(Ship ship)
    {
        //ships.Remove(ship); 
        int index = ships.IndexOf(ship);
        if (index == -1) return;
        ships[index] = null; //allows empty armada points to be found
    }

    public int getCount()
    {
        return ships.Count;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ship"></param>
    /// <returns>the index of the ship in the Fleet's array</returns>
    public int AddShip(Ship ship)
    {
        int index;
        if (ships.Contains(null))
        {
            do
            {
                index = UnityEngine.Random.Range(1, ships.Count - 1);
            } while (ships[index] != null); //maybe change
        }
        else
        {
            index = ships.Count;
        }
        AddShip(index, ship);
        return index;
    }

    void AddShip(int index, Ship ship)
    {

        if (index >= ships.Count)
        {
            Debug.Log($"ADDING ship at index {index}");
            ships.Add(ship);
        }
        else
        {
            Debug.Log($"REPLACING ship at index {index}");
            ships[index] = ship;
        }

    }

    public void setTargetFleet(Fleet fleet)
	{
        targetFleet = fleet;
        UpdateShipTargets();
	}

    void UpdateShipTargets()
	{
        foreach (Ship ship in ships)
		{
            // Set ship target to random ship
            ship.target = targetFleet.ships[Random.Range(0, targetFleet.getCount() - 1)];
		}
	}
}
