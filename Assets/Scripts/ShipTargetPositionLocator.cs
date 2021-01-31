using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShipTargetPositionLocator
{ 
    public static float GAPSIZE = 10f;
    static float ZOFFSET = 13f;

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
            z = (row * GAPSIZE * -1f) + ZOFFSET //* Mathf.Sin(Mathf.PI / 3f) 
        };
    }
}
