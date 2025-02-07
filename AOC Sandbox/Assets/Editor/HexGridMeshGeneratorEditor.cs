using UnityEngine;
using UnityEditor;

/*
 * ======================================================================================
 *                        HexGridMeshGenerator Editor Script
 * ======================================================================================
 * Custom editor for the HexGridMeshGenerator component. This script adds buttons to the
 * Unity Inspector to easily generate and clear the hexagonal mesh grid.
 *
 * Key Features:
 * - Provides an interface in the Unity Inspector to generate and clear hex meshes.
 * - Uses GUILayout buttons for user interaction.
 * ======================================================================================
 */

[CanEditMultipleObjects]
[CustomEditor(typeof(HexGridMeshGenerator))]
public class HexGridMeshGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // * Draws default inspector properties
        DrawDefaultInspector();
        
        // * Reference to the target HexGridMeshGenerator component
        HexGridMeshGenerator hexGridMeshGenerator = (HexGridMeshGenerator)target;
        
        // * Button to generate the hex mesh
        if (GUILayout.Button("Generate Hex Mesh"))
        {
            hexGridMeshGenerator.CreateHexMesh();
        }

        // * Button to clear the hex mesh
        if (GUILayout.Button("Clear Hex Mesh"))
        {
            hexGridMeshGenerator.ClearHexGridMesh();
        }
    }
}
