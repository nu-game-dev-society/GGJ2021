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
    [HideInInspector] public DetectionSystem detector;
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

    private void Update()
    {
        center = detector.transform.position;
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
        detector.CalculateRadius();

        center = detector.transform.position;
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
