using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[DataContract]
public struct SGridData
{
    [DataMember]
    public int index;
    [DataMember]
    public Color color;
    [DataMember]
    public int colorId;
    [DataMember]
    public int depth;
}
[DataContract]
public struct SMapGridData 
{
    [DataMember]
    public int row;
    [DataMember]
    public int col;
    [DataMember]
    public SGridData gridData;
}
[DataContract]
public class ShapeMapData
{
    [DataMember]
    public string shapeName;
    [DataMember]
    public SMapGridData[][] map;
}
