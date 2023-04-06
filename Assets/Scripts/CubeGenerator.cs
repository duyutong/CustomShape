using D.Unity3dTools;
using UnityEngine;
using UnityEngine.Rendering;

public class CubeGenerator : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshFilter = transform.GetOrAddComponent<MeshFilter>();
        meshRenderer = transform.GetOrAddComponent<MeshRenderer>();

        SUnitCube cube = new SUnitCube();

        // 定义两个材质
        Material material1 = new Material(Shader.Find("Standard"));
        Material material2 = new Material(Shader.Find("Standard"));

        meshRenderer.materials = new Material[] { material1, material2 };

        int[] triangles1 = new int[]
       {
            // Front face
            0, 1, 2,
            3, 0, 2,

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
        int[] triangles2 = new int[] 
        {
             // Back face
            4, 5, 6,
            7, 4, 6,
        };

        Mesh mesh = new Mesh();
        mesh.vertices = cube.vertices;
        mesh.triangles = cube.triangles;
        mesh.uv = cube.uv;
        mesh.RecalculateNormals();

        mesh.subMeshCount = 2; // 设置子网格数量为2
        mesh.SetTriangles(triangles1, 0); // 设置第一个子网格的三角形索引
        mesh.SetTriangles(triangles2, 1); // 设置第二个子网格的三角形索引
        mesh.SetSubMesh(0, new SubMeshDescriptor(0, triangles1.Length)); // 为第一个子网格指定材质1
        mesh.SetSubMesh(1, new SubMeshDescriptor(triangles1.Length, triangles2.Length)); // 为第二个子网格指定材质2

        meshFilter.mesh = mesh;
    }
}
