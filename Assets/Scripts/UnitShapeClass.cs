using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnitShapeClass
{
    public static int gridSize { set; get; } = 1;
    public static int mapSize { set; get; } = 16;

    public static SUnitCube GetUnitCube(SShapeIndex shapeIndex, int depth, int frontColorId, int backColorId = -1)
    {
        SUnitCube result = new SUnitCube();
        result.shapeIndex = shapeIndex;
        result.depth = depth;
        result.frontColorId = frontColorId;
        result.backColorId = backColorId;
        return result;
    }
}
public struct SShapeIndex
{
    public int row;
    public int col;
    public int index;
}
public struct SMeshData 
{
    public List<int> triangles;
    public int materialId;
}
public struct SUnitCube
{
    public Vector3[] vertices => GetVertices();
    public int[] triangles => GetTriangles();
    public Vector2[] uv => GetUV();


    public SShapeIndex shapeIndex;
    public int depth;
    public int frontColorId;
    public int backColorId;

    public int vertexCount { get { return vertices.Length; } }
    public int triangleCount { get { return triangles.Length; } }
    public int uvCount { get { return uv.Length; } }

    private Vector3[] GetVertices()
    {
        Vector3[] _vertices = new Vector3[]
            {
            // Front face
            new Vector3(0, 0, 0),   //[0]
            new Vector3(0, 1, 0),   //[1]
            new Vector3(1, 1, 0),   //[2]
            new Vector3(1, 0, 0),   //[3]

            // Back face
            new Vector3(1, 0, 1),   //[4]
            new Vector3(1, 1, 1),   //[5]
            new Vector3(0, 1, 1),   //[6]
            new Vector3(0, 0, 1),   //[7]

            // Left face
            new Vector3(0, 0, 1),   //[8]
            new Vector3(0, 1, 1),   //[9]
            new Vector3(0, 1, 0),   //[10]
            new Vector3(0, 0, 0),   //[11]

            // Right face
            new Vector3(1, 0, 0),   //[12]
            new Vector3(1, 1, 0),   //[13]
            new Vector3(1, 1, 1),   //[14]
            new Vector3(1, 0, 1),   //[15]

            // Top face
           new Vector3(1, 1, 1),    //[16]
           new Vector3(1, 1, 0),    //[17]
           new Vector3(0, 1, 0),    //[18]
           new Vector3(0, 1, 1),    //[19]

            // Bottom face
            new Vector3(1, 0, 0),   //[20]
            new Vector3(1, 0, 1),   //[21]
            new Vector3(0, 0, 1),   //[22]
            new Vector3(0, 0, 0),   //[23]
            };

        int size = UnitShapeClass.gridSize;
        int mapSize = UnitShapeClass.mapSize;

        Vector3 offset = size * new Vector3(shapeIndex.col - 0.5f * mapSize, shapeIndex.row - 0.5f * mapSize, -0.5f * Mathf.Max(depth, 1));
        for (int i = 0; i < _vertices.Length; i++) _vertices[i] += offset;
        return _vertices;
    }
    private int[] GetTriangles()
    {
        int[] _triangles = new int[]
            {
            // Front face
            0, 1, 2,
            3, 0, 2,

            // Back face
            4, 5, 6,
            7, 4, 6,

            // Left face
            8, 9, 10,
            11,8, 10,

            // Right face
            12,13,14,
            15,12,14,

            // Top face
            16,17,18,
            19,16,18,

            // Bottom face
            20,21,22,
            23,20,22,
            };

        for (int i = 0; i < _triangles.Length; i++) _triangles[i] += shapeIndex.index * vertices.Length;
        return _triangles;
    }
    private Vector2[] GetUV()
    {
        #region 用uv实现模型不同部位的不同颜色
        //basecolor贴图为8x8的一张色卡
        //Other face
        //float startU = (frontColorId % 8) * 0.125f;
        //float startV = (frontColorId / 8) * 0.125f;

        //float endU = (startU + 1) * 0.125f;
        //float endV = (startV + 1) * 0.125f;

        ////Back face
        //float _startU = (backColorId % 8) * 0.125f;
        //float _startV = (backColorId / 8) * 0.125f;

        //float _endU = (_startU + 1) * 0.125f;
        //float _endV = (_startV + 1) * 0.125f;

        //Vector2[] _uv = new Vector2[]
        //{
        //    // Front face
        //    new Vector2(startU, startV),
        //    new Vector2(startU, endV),
        //    new Vector2(endU, endV),
        //    new Vector2(endU, startV),

        //    // Back face
        //    new Vector2(_startU, _startV),
        //    new Vector2(_startU, _endV),
        //    new Vector2(_endU, _endV),
        //    new Vector2(_endU, _startV),

        //    // Left face
        //    new Vector2(startU, startV),
        //    new Vector2(startU, endV),
        //    new Vector2(endU, endV),
        //    new Vector2(endU, startV),

        //    // Right face
        //    new Vector2(startU, startV),
        //    new Vector2(startU, endV),
        //    new Vector2(endU, endV),
        //    new Vector2(endU, startV),

        //    // Top face
        //    new Vector2(startU, startV),
        //    new Vector2(startU, endV),
        //    new Vector2(endU, endV),
        //    new Vector2(endU, startV),

        //    // Bottom face
        //    new Vector2(startU, startV),
        //    new Vector2(startU, endV),
        //    new Vector2(endU, endV),
        //    new Vector2(endU, startV),
        //};
        #endregion

        Vector2[] _uv = new Vector2[]
      {
            // Front face
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),

            // Back face
           new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            
            // Left face
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),

            // Right face
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),

            // Top face
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),

            // Bottom face
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
      };
        return _uv;
    }

    public List<SMeshData> GetMeshDatas() 
    {
        List<SMeshData> result = new List<SMeshData>();
        SMeshData meshData1 = new SMeshData();
        meshData1.triangles = new List<int>(triangles);
        meshData1.materialId = frontColorId;
        result.Add(meshData1);
        if (frontColorId != backColorId && backColorId != -1) 
        {
            result.Clear();
            
            int[] frontTriangles = GetFrontTriangles();
            meshData1.triangles = new List<int>(frontTriangles);
            meshData1.materialId = frontColorId;

            SMeshData meshData2 = new SMeshData();
            int[] backTriangles = GetBackTriangles();
            meshData2.triangles = new List<int>(backTriangles);
            meshData2.materialId = backColorId;

            result.Add(meshData1);
            result.Add(meshData2);
        }

        return result;
    }
    public Mesh GetDefaultMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        if (frontColorId != backColorId && backColorId != -1)
        {
            int[] frontTriangles = GetFrontTriangles();
            int[] backTriangles = GetBackTriangles();
            mesh.subMeshCount = 2; // 设置子网格数量为2
            mesh.SetTriangles(frontTriangles, 0); // 设置第一个子网格的三角形索引
            mesh.SetTriangles(backTriangles, 1); // 设置第二个子网格的三角形索引
            mesh.SetSubMesh(0, new SubMeshDescriptor(0, frontTriangles.Length)); // 为第一个子网格指定材质1
            mesh.SetSubMesh(1, new SubMeshDescriptor(frontTriangles.Length, backTriangles.Length)); // 为第二个子网格指定材质2
        }
        return mesh;
    }
    private int[] GetFrontTriangles()
    {
        int[] result = new int[30];
        int[] _triangles = triangles;
        int index = 0;
        for (int i = 0; i < triangleCount; i++)
        {
            if (i > 5 && i < 12) continue;
            result[index] = _triangles[i];
            index++;
        }
        return result;
    }
    private int[] GetBackTriangles()
    {
        int[] result = new int[6];
        int[] _triangles = triangles;
        int index = 0;
        for (int i = 0; i < triangleCount; i++)
        {
            if (i <= 5 || i >= 12) continue;
            result[index] = _triangles[i];
            index++;
        }
        return result;
    }
}
