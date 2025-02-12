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

    void Start()
    {
        AdjustHexGridToTerrain();
    }
    #region Grid Rendering
    
    private void OnDrawGizmos()
    {
        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                // Compute base position
                Vector3 basePosition = HexMetrics.Center(HexSize, x, z, Orientation) + transform.position;

                // Get the terrain height for the hex center
                RaycastHit hit;
                if (Physics.Raycast(basePosition + Vector3.up * 10f, Vector3.down, out hit, 20f, LayerMask.GetMask("Terrain")))
                {
                    basePosition.y = hit.point.y;
                }

                // Draw each side of the hexagon
                for (int s = 0; s < 6; s++)
                {
                    Vector3 start = basePosition + HexMetrics.Corners(HexSize, Orientation)[s];
                    Vector3 end = basePosition + HexMetrics.Corners(HexSize, Orientation)[(s + 1) % 6];

                    // Adjust each corner to follow the terrain
                    if (Physics.Raycast(start + Vector3.up * 10f, Vector3.down, out hit, 20f, LayerMask.GetMask("Terrain")))
                    {
                        start.y = hit.point.y;
                    }
                    if (Physics.Raycast(end + Vector3.up * 10f, Vector3.down, out hit, 20f, LayerMask.GetMask("Terrain")))
                    {
                        end.y = hit.point.y;
                    }

                    Gizmos.DrawLine(start, end);
                }
            }
        }
    }

    #endregion
    #region Grid to Terrain
    void AdjustHexGridToTerrain()
    {
        foreach (Transform hex in transform)
        {
            RaycastHit hit;
            if (Physics.Raycast(hex.position + Vector3.up * 10f, Vector3.down, out hit, 20f, LayerMask.GetMask("Terrain")))
            {
                hex.position = new Vector3(hex.position.x, hit.point.y, hex.position.z);
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
