/*
 * ======================================================================================
 *                              HexGridEditor Script
 * ======================================================================================
 * This script is a custom editor for the HexGrid component in Unity. It enhances the
 * visualization of the hexagonal grid by displaying coordinate labels in the Scene View.
 *
 * Key Features:
 * - Draws coordinate labels for each hex cell in both offset and cube coordinates.
 * - Works dynamically with the HexGrid component settings.
 * - Uses Unity's Handles to overlay text in the Scene View.
 * ======================================================================================
 */

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexGrid))]
public class HexGridEditor : Editor
{
    void OnSceneGUI()
    {
        HexGrid hexGrid = (HexGrid)target;

        // * Iterate through each cell in the hexagonal grid
        for (int z = 0; z < hexGrid.Height; z++)
        {
            for (int x = 0; x < hexGrid.Width; x++)
            {
                // * Calculate the world position of the hex cell center
                Vector3 centrePosition = HexMetrics.Center(hexGrid.HexSize, x, z, hexGrid.Orientation) + hexGrid.transform.position;
                
                // * Offset coordinates of the hex
                int centerX = x;
                int centerZ = z;
                
                // * Convert offset coordinates to cube coordinates
                Vector3 cubeCoord = HexMetrics.OffsetToCube(centerX, centerZ, hexGrid.Orientation);
                
                // * Display the offset coordinates above the hex
                Handles.Label(centrePosition + Vector3.forward * 0.5f, $"[{centerX}, {centerZ}]");
                
                // * Display the cube coordinates at the center of the hex
                Handles.Label(centrePosition, $"({cubeCoord.x}, {cubeCoord.y}, {cubeCoord.z})");
            }
        }
    }
}
