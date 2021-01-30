using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TriangleNumbers
{
    /// <summary>
    /// Is the provided <paramref name="number"/> triangular?
    /// </summary>
    /// <param name="number"></param>
    /// <returns><see langword="true"/> if <paramref name="number"/> is triangular; otherwise <see langword="false"/> </returns>
    public static bool IsTriangular(int number)
    {
        // Base case 
        if (number < 0)
            return false;

        int sum = 0;
        for (int i = 1; sum <= number; i++)
        {
            sum += i;
            if (sum == number)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Gets the row in which <paramref name="number"/> is found in the triangle
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <remarks><see cref="Nabbed from:" href="https://stackoverflow.com/questions/39515036/how-to-find-row-number-of-a-chosen-number-in-a-triangular-number-sequence"/></remarks>
    public static int GetRowInTriangle(int number)
    {
        return (int)Mathf.Ceil((Mathf.Sqrt(8 * number + 1) - 1) / 2);
    }

    /// <summary>
    /// Gets the <paramref name="n"/>th triangle number
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    static int GetTriangleNumber(int n) => n * (n + 1) / 2;

    /// <summary>
    /// Gets the row and position of the item in the triangle at the provided <paramref name="index"/>
    /// </summary>
    /// <param name="index">The index of the item</param>
    /// <returns>The row and position of the item in the triangle</returns>
    public static Tuple<int, int> GetRowAndPos(int index)
    {
        int row = GetRowInTriangle(index);
        int posInRow = index - GetTriangleNumber(row-1);

        return new Tuple<int, int>(row, posInRow);
    }
}
