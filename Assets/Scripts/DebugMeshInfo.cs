using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMeshInfo : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        Mesh mesh = meshFilter.mesh;
        for (int i = 0; i < 10; i++) 
        {
            Debug.Log(mesh.vertices[i] + "  " + mesh.uv[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
