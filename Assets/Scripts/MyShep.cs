using D.Unity3dTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class MyShep : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = transform.GetOrAddComponent<MeshFilter>();
        meshRenderer = transform.GetOrAddComponent<MeshRenderer>();

        SUnitCube cube = new SUnitCube();
      
        Mesh mesh = new Mesh();
        mesh.vertices = cube.vertices;
        mesh.triangles = cube.triangles;
        mesh.uv = cube.uv;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

        Material material = new Material(Shader.Find("Standard"));
        meshRenderer.material = material;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
