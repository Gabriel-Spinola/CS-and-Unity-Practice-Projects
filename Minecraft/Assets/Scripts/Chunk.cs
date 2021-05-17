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

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();

    private bool[,,] voxelMap = new bool[VoxelData.chunkWidth, VoxelData.chunkHeight, VoxelData.chunkWidth];

    private int vertexIndex = 0;

    private void Start()
    {
        PopulateVoxelMap();
        CreateMeshChunkData();
        CreateMesh();
    }

    /// <summary>
    /// Populate the voxelMap
    /// </summary>
    private void PopulateVoxelMap() 
    {
        for (int y = 0; y < VoxelData.chunkHeight; y++)
            for (int x = 0; x < VoxelData.chunkWidth; x++)
                for (int z = 0; z < VoxelData.chunkWidth; z++) {
                    voxelMap[x, y, z] = true;
                }
    }

    /// <summary>
    /// Set the chunk mesh data (:
    /// Offset the position of each voxel
    /// </summary>
    private void CreateMeshChunkData() 
    {
        for (int y = 0; y < VoxelData.chunkHeight; y++)
            for (int x = 0; x < VoxelData.chunkWidth; x++)
                for (int z = 0; z < VoxelData.chunkWidth; z++) {
                    AddVoxelDataToChunk(new Vector3(x, y, z));
                }
    }

    /// <summary>
    /// Check if we have a voxel in a certain position
    /// </summary>
    private bool hasVoxelThere(Vector3 pos) 
    {
        int x = Mathf.FloorToInt(pos.x),
            y = Mathf.FloorToInt(pos.y),
            z = Mathf.FloorToInt(pos.z);

        // Set the constraints
        if (
            (x < 0 || x > VoxelData.chunkWidth  - 1) ||
            (y < 0 || y > VoxelData.chunkHeight - 1) ||
            (z < 0 || z > VoxelData.chunkWidth  - 1)
        ) {
            return false;
        }

        return voxelMap[x, y, z];
    }

    /// <summary>
    /// * if theres is no voxel againts that face:
    /// Get the current triangle
    /// Add the vertices, uvs and triangles to its list
    /// Set the position of the voxels
    /// </summary>
    private void AddVoxelDataToChunk(Vector3 pos) 
    {
        for (int i = 0; i < VoxelData.voxelTris.GetLength(0); i++) {
            if (!hasVoxelThere(pos + VoxelData.faceChecks[i])) {
                for (int j = 0; j < VoxelData.voxelTris.GetLength(1); j++) {
                    vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[i, j]]);
                    uvs.Add(VoxelData.voxelUvs[j]);
                }

                for (int j = 0; j < 6; j++) {
                    triangles.Add(vertexIndex + VoxelData.vertexOrder[j]);
                }

                vertexIndex += 4;
            }
        }
    }


    /// <summary>
    /// Create the mesh and recalculate the normals
    /// </summary>
    private void CreateMesh() 
    {
        Mesh mesh = new Mesh();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }
}
