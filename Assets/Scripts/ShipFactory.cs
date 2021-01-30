using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactory : MonoBehaviour
{
    public IntVariable playerShips;

    [SerializeField] GameObject shipObject;

    List<GameObject> ships = new List<GameObject>();

    Rigidbody Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        this.Rigidbody = this.GetComponent<Rigidbody>();
        SpawnShips(100);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
            AddShip();
        if (Input.GetKey(KeyCode.KeypadMinus))
            RemoveShip();

        this.Rigidbody.velocity = new Vector3(0, 0, 10f);
    }

    void AddShip()
    {
        GameObject newShip = CreateShip(TriangleNumbers.GetRowAndPos(ships.Count + 1));
        ships.Add(newShip);
        UpdatePlayerShipCount();
    }

    void RemoveShip()
    {
        GameObject shipToDestroy = ships[ships.Count - 1];
        ships.Remove(shipToDestroy);
        Destroy(shipToDestroy);
        UpdatePlayerShipCount();
    }

    void UpdatePlayerShipCount()
    {
        playerShips.RuntimeValue = ships.Count;
    }

    GameObject CreateShip(Vector3 position)
    {
        GameObject newShip = Instantiate(shipObject, transform.position, Quaternion.identity, transform);
        newShip.transform.localPosition = position;
        return newShip;
    }
    GameObject CreateShip(int row, int posInRow) => CreateShip(ShipTargetPositionLocator.GetShipTargetPosition(row, posInRow));
    GameObject CreateShip(Tuple<int, int> rowAndPos) => CreateShip(rowAndPos.Item1, rowAndPos.Item2);


    void SpawnShips(int count)
    {
        for (int i = 1; i <= count; ++i)
        {
            AddShip();
        }
    }
    void SpawnRowsOfShips(int rows) => SpawnShips(TriangleNumbers.GetTriangleNumber(rows));
}
