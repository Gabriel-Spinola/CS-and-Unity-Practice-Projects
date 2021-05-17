using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    private void Awake() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int vertexIndex = 0;

        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();

        for (int i = 0; i < VoxelData.voxelTris.GetLength(0); i++) {
            for (var j = 0; j < VoxelData.voxelTris.GetLength(1); j++) {
                int triangleIndex = VoxelData.voxelTris[i, j];

                vertices.Add(VoxelData.voxelVerts[triangleIndex]);
                triangles.Add(vertexIndex);
                uvs.Add(Vector2.zero);
                
                vertexIndex++;
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }
}
