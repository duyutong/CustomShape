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

        Vector3[] vertices = new Vector3[3 * UnitCubeClass.unitCube_24.vertexCount];
        Vector2[] uv = new Vector2[3 * UnitCubeClass.unitCube_24.uvCount];
        int[] triangles = new int[3 * UnitCubeClass.unitCube_24.triangleCount];

        SUnitCube cube1 = UnitCubeClass.unitCube_24;
        SUnitCube cube2 = UnitCubeClass.unitCube_24;
        SUnitCube cube3 = UnitCubeClass.unitCube_24;
        for (int i = 0; i < UnitCubeClass.unitCube_24.vertexCount; i++)
            cube2.vertices[i] += Vector3.up;
        for (int i = 0; i < UnitCubeClass.unitCube_24.triangleCount; i++)
            cube2.triangles[i] += UnitCubeClass.unitCube_24.vertexCount;

        for (int i = 0; i < UnitCubeClass.unitCube_24.vertexCount; i++)
            cube3.vertices[i] += Vector3.right;
        for (int i = 0; i < UnitCubeClass.unitCube_24.triangleCount; i++)
            cube3.triangles[i] += 2 * UnitCubeClass.unitCube_24.vertexCount;

        cube1.vertices.CopyTo(vertices, 0);
        cube1.triangles.CopyTo(triangles, 0);
        cube1.uv.CopyTo(uv, 0);

        cube2.vertices.CopyTo(vertices, UnitCubeClass.unitCube_24.vertexCount);
        cube2.triangles.CopyTo(triangles, UnitCubeClass.unitCube_24.triangleCount);
        cube2.uv.CopyTo(uv, UnitCubeClass.unitCube_24.uvCount);

        cube3.vertices.CopyTo(vertices, 2 * UnitCubeClass.unitCube_24.vertexCount);
        cube3.triangles.CopyTo(triangles, 2 * UnitCubeClass.unitCube_24.triangleCount);
        cube3.uv.CopyTo(uv, 2 * UnitCubeClass.unitCube_24.uvCount);

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
