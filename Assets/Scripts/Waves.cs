using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public float scale = 10.0f;
    public float speed = 1.0f;
    public float noiseStrength = 4.0f;
    public float noiseWalk = 1f;

    private Vector3[] baseHeight;

    public MeshFilter meshFilter;
    public Mesh mesh;

    public Transform playerMain;

    private void Start()
    {
        mesh = new Mesh();

        mesh.vertices = meshFilter.sharedMesh.vertices;
        mesh.triangles = meshFilter.sharedMesh.triangles;
        mesh.normals = meshFilter.sharedMesh.normals;
        mesh.uv = meshFilter.sharedMesh.uv;

        meshFilter.sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void Update()
    {
        transform.position = playerMain.position;

        if (baseHeight == null)
            baseHeight = mesh.vertices;

        var vertices = new Vector3[baseHeight.Length];
        for (var i = 0; i < vertices.Length; i++)
        {
            var vertex = transform.TransformPoint(baseHeight[i]);
            vertex.y += Mathf.Sin(Time.time * speed + vertex.x + vertex.y + vertex.z) * scale;
            vertex.y += Mathf.PerlinNoise(vertex.x + noiseWalk, vertex.y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
            vertices[i] = transform.InverseTransformPoint(vertex);
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
   
        //GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public float  GetHeightAtPosition(Vector3 vertex)
    {
        vertex.y += Mathf.Sin(Time.time * speed + vertex.x + vertex.y + vertex.z) * scale;
        vertex.y += Mathf.PerlinNoise(vertex.x + noiseWalk, vertex.y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;

        return vertex.y; 
    }
}
