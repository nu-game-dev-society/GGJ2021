using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactory : MonoBehaviour
{
    public IntVariable playerShips;

    [SerializeField] GameObject shipObject;
    [SerializeField] GameObject playerShip;
    

    public List<GameObject> ships = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerShip.transform.localPosition = ShipTargetPositionLocator.GetShipTargetPosition(1, 1);
        ships.Add(playerShip);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeleteRandomShip()
    {
        int index;
        do
        {
            index = UnityEngine.Random.Range(1, ships.Count - 1);
        } while (ships[index] == null);

        DeleteShip(index);
    }

    void AddShip(int index)
    {
        GameObject newShip = CreateShip(index);
        
        if(index >= ships.Count)
        {
            Debug.Log($"ADDING ship at index {index}");
            ships.Add(newShip);
        }
        else
        {
            Debug.Log($"REPLACING ship at index {index}");
            ships[index] = newShip;
        }
        
        UpdatePlayerShipCount();
    }
    public void AddShip()
    {
        int index;
        if (ships.Contains(null))
        {
            do
            {
                index = UnityEngine.Random.Range(1, ships.Count - 1);
            } while (ships[index] != null);
        }
        else
        {
            index = ships.Count;
        }
        AddShip(index);
    }

    void DeleteShip(int index)
    {
        GameObject shipToDestroy = ships[index];
        shipToDestroy.GetComponentInChildren<Animator>().Play("ShipSink");
        ships[index] = null; // don't remove, because we need to replace it
        Debug.Log($"REMOVING ship at index {index}");
        Destroy(shipToDestroy, 1.0f);
        UpdatePlayerShipCount();
    }
    public void DeleteShip() => DeleteShip(ships.Count - 1);

    void UpdatePlayerShipCount()
    {
        playerShips.RuntimeValue = ships.Count;
    }

    GameObject CreateShip(Vector3 position, Transform parent)
    {
        GameObject newShip = Instantiate(shipObject, transform.position, Quaternion.identity, parent);
        newShip.transform.localPosition = position;
        return newShip;
    }
    GameObject CreateShip(Vector3 position) => CreateShip(position, ships[0].transform);
    GameObject CreateShip(int row, int posInRow) => CreateShip(ShipTargetPositionLocator.GetShipTargetPosition(row, posInRow));
    GameObject CreateShip(Tuple<int, int> rowAndPos) => CreateShip(rowAndPos.Item1, rowAndPos.Item2);
    GameObject CreateShip(int index) => CreateShip(TriangleNumbers.GetRowAndPos(index + 1));


    void SpawnShips(int count)
    {
        for (int i = 1; i <= count; ++i)
        {
            AddShip();
        }
    }
    void SpawnRowsOfShips(int rows) => SpawnShips(TriangleNumbers.GetTriangleNumber(rows));
}
