/*
 * ======================================================================================
 *                              HexGrid Script
 * ======================================================================================
 * This script generates a hexagonal grid in Unity. It defines the grid's dimensions,
 * hex size, and orientation, and uses Gizmos to visualize the hexagons in the editor.
 *
 * Key Features:
 * - Defines a hexagonal grid with configurable width, height, and hex size.
 * - Supports both flat-top and pointy-top hex orientations.
 * - Uses Gizmos to render the grid outline in the Unity editor.
 * ======================================================================================
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexGrid : MonoBehaviour
{
    #region Grid Properties
    // * Grid size and properties
    
    [field:SerializeField] public HexOrientation Orientation { get; private set; } // * Orientation of the hexagons (flat-top or pointy-top)
    [field:SerializeField] public int Width { get; private set; } // * Number of hexes in the X-axis
    [field:SerializeField] public int Height { get; private set; } // * Number of hexes in the Z-axis
    [field:SerializeField] public float HexSize { get; private set; } // * Size of each hexagon
    [field:SerializeField] public GameObject HexPrefab { get; private set; } // * Prefab used for hexagonal tiles
    #endregion

    #region Grid Rendering
    
    private void OnDrawGizmos()
    {
        // * Draws the hexagonal grid outline using Gizmos
        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                // * Calculate the center position of the hexagon
                Vector3 centrePosition = HexMetrics.Center(HexSize, x, z, Orientation) + transform.position;
                
                // * Draw each side of the hexagon
                for (int s = 0; s < HexMetrics.Corners(HexSize, Orientation).Length; s++)
                {
                    Gizmos.DrawLine(
                        centrePosition + HexMetrics.Corners(HexSize, Orientation)[s % 6],
                        centrePosition + HexMetrics.Corners(HexSize, Orientation)[(s + 1) % 6]
                    );
                }
            }
        }
    }
    
    #endregion
}

#region Enums
public enum HexOrientation
{
    FlatTop, // * Hexagons are aligned with flat sides on top and bottom
    PointyTop // * Hexagons are aligned with points on top and bottom
}
#endregion
