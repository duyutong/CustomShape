using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShep : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        Vector3[] vertices = new Vector3[3 * UnitShapeClass.unitCube.vertexCount];
        Vector2[] uv = new Vector2[3 * UnitShapeClass.unitCube.uvCount];
        int[] triangles = new int[3 * UnitShapeClass.unitCube.triangleCount];

        SUnitCube cube1 = UnitShapeClass.unitCube;
        SUnitCube cube2 = UnitShapeClass.unitCube;
        SUnitCube cube3 = UnitShapeClass.unitCube;
        for (int i = 0; i < UnitShapeClass.unitCube.vertexCount; i++)
            cube2.vertices[i] += Vector3.up;
        for (int i = 0; i < UnitShapeClass.unitCube.triangleCount; i++)
            cube2.triangles[i] += UnitShapeClass.unitCube.vertexCount;

        for (int i = 0; i < UnitShapeClass.unitCube.vertexCount; i++)
            cube3.vertices[i] += Vector3.right;
        for (int i = 0; i < UnitShapeClass.unitCube.triangleCount; i++)
            cube3.triangles[i] += 2 * UnitShapeClass.unitCube.vertexCount;

        for (int i = 0; i < UnitShapeClass.unitCube.uvCount; i++)
            cube3.uv[i] *= 0.125f;

        cube1.vertices.CopyTo(vertices, 0);
        cube1.triangles.CopyTo(triangles, 0);
        cube1.uv.CopyTo(uv, 0);

        cube2.vertices.CopyTo(vertices, UnitShapeClass.unitCube.vertexCount);
        cube2.triangles.CopyTo(triangles, UnitShapeClass.unitCube.triangleCount);
        cube2.uv.CopyTo(uv, UnitShapeClass.unitCube.uvCount);

        cube3.vertices.CopyTo(vertices, 2 * UnitShapeClass.unitCube.vertexCount);
        cube3.triangles.CopyTo(triangles, 2 * UnitShapeClass.unitCube.triangleCount);
        cube3.uv.CopyTo(uv, 2 * UnitShapeClass.unitCube.uvCount);

        // Create a mesh object
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        // Set the mesh and material
        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
