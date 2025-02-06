/*
 * ======================================================================================
 *                              HexMetrics Utility Class
 * ======================================================================================
 * This static class provides mathematical utilities for working with hexagonal grids.
 * It defines functions to calculate hexagon properties such as size, corners, and positions.
 *
 * Key Features:
 * - Calculates outer and inner radius of a hex based on a given size.
 * - Provides utility functions to determine hex corner positions.
 * - Computes the center position of a hexagon in a grid.
 * ======================================================================================
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class HexMetrics
{
    #region Hex Size Calculations
    
    // * Returns the outer radius of a hexagon (equivalent to the hex size)
    public static float OuterRadius(float hexSize)
    {
        return hexSize;
    }

    // * Returns the inner radius of a hexagon (calculated as outer radius * sqrt(3)/2)
    public static float InnerRadius(float hexSize)
    {
        return hexSize * 0.866025404f; // Approximation of sqrt(3)/2
    }
    
    #endregion

    #region Hex Corner Calculations
    
    // * Returns an array of 6 corner positions for a hexagon given its size and orientation
    public static Vector3[] Corners(float hexSize, HexOrientation orientation)
    {
        Vector3[] corners = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            corners[i] = Corner(hexSize, orientation, i);
        }
        return corners;
    }
    
    // * Computes the position of a single corner of a hexagon given its size, orientation, and corner index
    public static Vector3 Corner(float hexSize, HexOrientation orientation, int index)
    {
        float angle = 60f * index;
        if (orientation == HexOrientation.PointyTop)
        {
            angle += 30f;
        }
        return new Vector3(
            hexSize * Mathf.Cos(angle * Mathf.Deg2Rad),
            0f,
            hexSize * Mathf.Sin(angle * Mathf.Deg2Rad)
        );
    }
    
    #endregion

    #region Hex Grid Positioning
    
    // * Calculates the world-space center position of a hex tile in the grid based on its coordinates
    public static Vector3 Center(float hexSize, int x, int z, HexOrientation orientation)
    {
        Vector3 centerPosition;
        if (orientation == HexOrientation.PointyTop)
        {
            centerPosition.x = (x + z * 0.5f - z / 2) * (InnerRadius(hexSize) * 2f);
            centerPosition.y = 0f;
            centerPosition.z = z * (OuterRadius(hexSize) * 1.5f);
        }
        else
        {
            centerPosition.x = x * (OuterRadius(hexSize) * 1.5f);
            centerPosition.y = 0f;
            centerPosition.z = (z + x * 0.5f - x / 2) * (InnerRadius(hexSize) * 2f);
        }
        return centerPosition;
    }
    
    #endregion
}
