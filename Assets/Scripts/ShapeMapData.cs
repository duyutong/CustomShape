using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public struct SGridData
{
    public int index;
    public Color color;
    public int colorId;
    public int depth;
}
public struct SMapGridData 
{
    public int row;
    public int col;
    public SGridData gridData;
}
public class ShapeMapData
{
    public string shapeName;
    public SMapGridData[][] map;
}
