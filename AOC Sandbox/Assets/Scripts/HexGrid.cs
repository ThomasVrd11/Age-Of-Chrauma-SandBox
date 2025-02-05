using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HexGrid : MonoBehaviour
{
    #region Variables
    // * grid size 

    [field:SerializeField] public HexOrientation Orientation { get; private set; }
    [field:SerializeField] public int Width { get; private set; }
    [field:SerializeField] public int Height { get; private set; }
    [field:SerializeField] public float HexSize { get; private set; }
    [field:SerializeField] public GameObject HexPrefab { get; private set; }
    #endregion

    #region Methods
    
    private void OnDrawGizmos()
    {
        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                Vector3 centrePosition = HexMetrics.Center(HexSize, x, z, Orientation) + transform.position;
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

public enum HexOrientation
{
    FlatTop,
    PointyTop
}
