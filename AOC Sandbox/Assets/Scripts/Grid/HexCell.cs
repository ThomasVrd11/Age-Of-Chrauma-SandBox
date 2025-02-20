using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexCell
{
    [Header("Cell Properties")]
    [SerializeField]private HexOrientation orientation;
    [field:SerializeField] public HexGrid Grid { get; set; }
    [field:SerializeField] public float HexSize { get; set; }
    [field:SerializeField] public TerrainType TerrainType { get; private set; }
    [field:SerializeField] public Vector2 OffsetCoordinates { get; set; }
    [field:SerializeField] public Vector3 CubeCoordinates { get; private set; }
    [field:SerializeField] public Vector2 AxialCoordinates { get; private set; }
    [field:NonSerialized] public List<HexCell> Neighbours { get; private set; }

}