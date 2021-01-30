using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{
    public int liveShipsCount = 0;
    public enum fleetColour { red, blue, green };
    public fleetColour colour;
    public List<Ship> ships; //maybe hashset optimise
    public Vector3 center;
    DetectionSystem detector;
    public List<Fleet> targetFleets;

    private void Awake()
    {
        ships = new List<Ship>();
        targetFleets = new List<Fleet>();
        detector = GetComponentInChildren<DetectionSystem>();
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
        {
            ships[index] = null; //allows empty armada points to be found
            liveShipsCount--;
            CalculateDetection();
        }

    }

    public int getCount()
    {
        return liveShipsCount;
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
        liveShipsCount++;
        CalculateDetection();
        return index;
    }

    private void CalculateDetection()
    {
        Vector3 value = Vector3.zero;
        for (int i = ships.Count - 1; i >= 1; i--)
        {
            if (ships[i] != null)
            {
                value.z = transform.InverseTransformPoint(ships[i].transform.position).z/2.0f;
                break;
            }

        }
        detector.CalculcateRadius(value);
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

    public void AddTargetFleet(Fleet fleet)
    {
        if (fleet != this && !targetFleets.Contains(fleet))
        {
            targetFleets.Add(fleet);
        }
    }
    public void RemoveTargetFleet(Fleet fleet)
    {
        if (targetFleets.Contains(fleet))
        {
            targetFleets.Remove(fleet);
        }
    }

}
