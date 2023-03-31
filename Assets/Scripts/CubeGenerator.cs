using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        // Create vertices of the cube
        Vector3[] vertices = new Vector3[]
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

        // Create triangles of the cube
        int[] triangles = new int[]
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

        // Create UV coordinates of the cube
        Vector2[] uv = new Vector2[]
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
}
