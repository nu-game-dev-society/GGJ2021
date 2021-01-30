using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTargetFactory : MonoBehaviour
{
    public IntVariable playerShips;
    [SerializeField] GameObject shipObject;
    Rigidbody Rigidbody;
    const float GAPSIZE = 10f;

    // Start is called before the first frame update
    void Start()
    {
        this.Rigidbody = this.GetComponent<Rigidbody>();
        SpawnShips(10);
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
        
    }

    void RemoveShip()
    {
        
    }

    void SpawnRowsOfShips(int rows)
    {
        for (int row = 0; row < rows; row++)
        {
            for (int posInRow = 0; posInRow < row + 1; posInRow++)
            {
                SpawnShip(row, posInRow);
            }
        }
    }

    void SpawnShips(int count)
    {
        for(int i =1; i<=count;++i)
        {
            Vector3 position = GetShipTargetPosition(i);
            SpawnShip(position);
        }
    }

    void SpawnShip(Vector3 position)
    {
        GameObject newShip = Instantiate(shipObject, transform.position, Quaternion.identity, transform);
        newShip.transform.localPosition = position;
    }
    void SpawnShip(int row, int posInRow) => SpawnShip(GetShipTargetPosition(row, posInRow));

    /// <summary>
    /// Gets the target position of the ship at the provided <paramref name="index"/>
    /// </summary>
    /// <param name="index">The index of the ship in the fleet</param>
    /// <returns>The target position of the ship</returns>
    public static Vector3 GetShipTargetPosition(int index)
    {
        System.Tuple<int, int> rowAndPos = TriangleNumbers.GetRowAndPos(index);
        int row = rowAndPos.Item1;
        int posInRow = rowAndPos.Item2;

        Debug.Log($"Ship #{index} is in row {row} at position {posInRow}");

        return GetShipTargetPosition(row, posInRow);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="row"></param>
    /// <param name="posInRow"></param>
    /// <returns></returns>
    public static Vector3 GetShipTargetPosition(int row, int posInRow)
    {
        return new Vector3
        {
            x = (posInRow - row * 0.5f) * GAPSIZE,
            y = 0,
            z = row * GAPSIZE * Mathf.Sin(Mathf.PI / 3f) * -1f
        };
    }
}
