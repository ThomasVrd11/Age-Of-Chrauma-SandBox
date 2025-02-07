/*
 * ======================================================================================
 *                          HexGridMeshGenerator Script
 * ======================================================================================
 * This script generates a hexagonal grid mesh in Unity using a procedural approach.
 * It constructs the vertices and triangles dynamically based on the grid size, 
 * hex size, and orientation. It also applies a LayerMask for collision detection.
 *
 * Key Features:
 * - Dynamically generates a hexagonal grid mesh.
 * - Supports different hex orientations (flat-top or pointy-top).
 * - Uses MeshFilter, MeshRenderer, and MeshCollider components.
 * - Assigns a LayerMask for proper physics interactions.
 * ======================================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class HexGridMeshGenerator : MonoBehaviour
{
    #region Grid Properties
    [field: SerializeField] public LayerMask gridLayer { get; private set; } // * Layer mask for the grid
    [field:SerializeField] public HexGrid hexGrid { get; private set; } // * Reference to the HexGrid component
    #endregion

    #region Initialization
    private void Awake()
    {
        // * Ensure the hexGrid reference is set
        if(hexGrid == null)
            hexGrid = GetComponentInParent<HexGrid>();
        if (hexGrid == null)
            Debug.LogError("HexGridMeshGenerator could not find a HexGrid component in its parent or itself.");
    }
    #endregion

    #region Mesh Creation
    public void CreateHexMesh()
    {
        // * Generates the hexagonal grid mesh
        CreateHexMesh(hexGrid.Width, hexGrid.Height, hexGrid.HexSize, hexGrid.Orientation, gridLayer);
    }

    public void CreateHexMesh(HexGrid hexGrid, LayerMask layerMask)
    {
        // * Overload method to allow manual assignment of hexGrid and layerMask
        this.hexGrid = hexGrid;
        this.gridLayer = layerMask;
        CreateHexMesh(hexGrid.Width, hexGrid.Height, hexGrid.HexSize, hexGrid.Orientation, layerMask);
    }

    public void CreateHexMesh(int width, int height, float hexSize, HexOrientation orientation, LayerMask layerMask)
    {
        ClearHexGridMesh(); // * Clears existing mesh before generating a new one
        
        Vector3[] vertices = new Vector3[7 * width * height]; // * Array for storing mesh vertices

        // * Generate the hexagon vertices
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 centrePosition = HexMetrics.Center(hexSize, x, z, orientation);
                vertices[(z * width + x) * 7] = centrePosition;
                for (int s = 0; s < HexMetrics.Corners(hexSize, orientation).Length; s++)
                {
                    vertices[(z * width + x) * 7 + s + 1] = centrePosition + HexMetrics.Corners(hexSize, orientation)[s % 6];
                }
            }
        }

        // * Define triangle indices
        int[] triangles = new int[3 * 6 * width * height];
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int s = 0; s < HexMetrics.Corners(hexSize, orientation).Length; s++)
                {
                    int cornerIndex = s + 2 > 6 ? s + 2 - 6 : s + 2;
                    triangles[3 * 6 * (z * width + x) + s * 3 + 0] = (z * width + x) * 7;
                    triangles[3 * 6 * (z * width + x) + s * 3 + 1] = (z * width + x) * 7 + s + 1;
                    triangles[3 * 6 * (z * width + x) + s * 3 + 2] = (z * width + x) * 7 + cornerIndex;
                }
            }
        }

        // * Create the mesh and assign properties
        Mesh mesh = new Mesh();
        mesh.name = "Hex Mesh";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
        mesh.RecalculateUVDistributionMetrics();

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        int gridLayerIndex = GetLayerIndex(layerMask);
        Debug.Log("Layer Index: " + gridLayerIndex);

        gameObject.layer = gridLayerIndex; // * Assigns the proper layer
    }
    #endregion

    #region Mesh Management
    public void ClearHexGridMesh()
    {
        // * Clears the existing mesh to prevent overlaps
        if (GetComponent<MeshFilter>().sharedMesh == null)
            return;
        GetComponent<MeshFilter>().sharedMesh.Clear();
        GetComponent<MeshCollider>().sharedMesh.Clear();
    }
    #endregion

    #region Utility Functions
    private int GetLayerIndex(LayerMask layerMask)
    {
        // * Retrieves the layer index from the provided LayerMask
        int layerMaskValue = layerMask.value;
        Debug.Log("Layer Mask Value: " + layerMaskValue);
        for (int i = 0; i < 32; i++)
        {
            if (((1 << i) & layerMaskValue) != 0)
            {
                Debug.Log("Layer Index Loop: " + i);
                return i;
            }
        }
        return 0;
    }
    #endregion
}
