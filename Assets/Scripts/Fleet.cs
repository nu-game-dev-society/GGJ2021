using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Fleet : MonoBehaviour
{
    public int liveShipsCount = 0;
    public enum fleetColour { red, blue, green };
    public fleetColour colour;
    public List<Ship> ships; //maybe hashset optimise
    public Vector3 center;
    [HideInInspector] public DetectionSystem detector;
    public List<Fleet> targetFleets;

    public float totalHealth;

    private void Awake()
    {
        ships = new List<Ship>();
        targetFleets = new List<Fleet>();
        detector = GetComponentInChildren<DetectionSystem>();
        center = transform.position;
    }

    public void GetTotalHealth()
    {
        float h = 0;

        foreach (Ship s in ships)
        {
            if (s)
                h += s.health;
        }

        totalHealth = h;
    }


    public Fleet(fleetColour colour)
    {
        this.colour = colour;
    }

    private void Update()
    {
        if (detector != null)
            center = detector.transform.position;

        GetTotalHealth();
    }

    public void RemoveShip(Ship ship)
    {
        //ships.Remove(ship); 
        int index = ships.IndexOf(ship);

        if (index == -1) return;
        {
            ships[index] = null; //allows empty armada points to be found
            liveShipsCount--;
            detector.CalculateRadius();
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
        int index = -1;
        if (ships.Contains(null))
        {
            for (int i = 0; i < ships.Count; i++)
            {
                if (ships[i] == null)
                    index = i; 
            }

            if (index == -1)
                index = ships.Count;
        }
        else
        {
            index = ships.Count;
        }
        AddShip(index, ship);
        liveShipsCount++;
        detector.CalculateRadius();

        center = detector.transform.position;
        return index;
    }

    void AddShip(int index, Ship ship)
    {

        if (index >= ships.Count)
        {
            //Debug.Log($"ADDING ship at index {index}");
            ships.Add(ship);
        }
        else
        {
            //Debug.Log($"REPLACING ship at index {index}");
            ships[index] = ship;
        }

        if(ship.colour)
            ship.colour.setShipColour(ships[0].colour.getShipColour());
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

#if UNITY_EDITOR
    [Header("TESTING")]
    public int addNumberOfShipsTest = 1;
    public GameObject AIPrefab;
    [ContextMenu("TestAddShips X")]
    public void TestAddShips()
    {
        StartCoroutine(AddEverySecondEditor());
    }
    IEnumerator AddEverySecondEditor()
    {
        for (int i = 0; i < addNumberOfShipsTest; i++)
        {
            GameObject go = Instantiate(AIPrefab);
            Destroy(go.GetComponent<AIShipController>());


            Ship ship = go.GetComponent<Ship>();
            ship.myFleet = this;
            ship.LeaveFleet();
            int index = AddShip(ship);

            Vector3 newPos = ShipTargetPositionLocator.GetShipTargetPosition(index + 1);

            Transform t = new GameObject().transform;
            t.parent = transform;
            t.localPosition = newPos;
            ship.controller.target = t;

            ship.transform.position = transform.TransformPoint(newPos);

            ship.SetAnimTrigger("ShipSpawn");
            yield return new WaitForSeconds(0.5f);
        }
    }
#endif
}
