using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public ChunkCoord coord;

    private GameObject chunkObject;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private World world;

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();

    [Tooltip("Store what voxel its occuping certain space")]
    private byte[,,] voxelMap = new byte[VoxelData.chunkWidth, VoxelData.chunkHeight, VoxelData.chunkWidth];

    private int vertexIndex = 0;

    public Vector3 Position {
        get { return chunkObject.transform.position; }
    }
    
    public bool IsActive {
        get { return chunkObject.activeSelf; }
        set { chunkObject.SetActive(value); }
    }

    public Chunk(ChunkCoord _coord, World _world)
    {
        coord = _coord;
        world = _world;

        chunkObject = new GameObject();

        meshFilter = chunkObject.AddComponent<MeshFilter>();
        meshRenderer = chunkObject.AddComponent<MeshRenderer>();

        chunkObject.transform.SetParent(world.transform);
        chunkObject.transform.position = new Vector3(coord.x * VoxelData.chunkWidth, 0f, coord.z * VoxelData.chunkWidth);
        chunkObject.name = $"Chunk { coord.x }, { coord.z }";

        meshRenderer.material = world.material;

        PopulateVoxelMap();
        CreateMeshChunkData();
        CreateMesh();
    }

    /// <summary>
    /// Populate the voxelMap with the generated voxels
    /// </summary>
    private void PopulateVoxelMap() 
    {
        for (int y = 0; y < VoxelData.chunkHeight; y++)
          for (int x = 0; x < VoxelData.chunkWidth; x++)
            for (int z = 0; z < VoxelData.chunkWidth; z++)
              voxelMap[x, y, z] = world.GetVoxel(new Vector3(x, y, z) + Position);
    }

    /// <summary>
    /// Set the chunk mesh data
    /// Offset the position of each voxel
    /// </summary>
    private void CreateMeshChunkData() 
    {
        for (int y = 0; y < VoxelData.chunkHeight; y++)
          for (int x = 0; x < VoxelData.chunkWidth; x++)
            for (int z = 0; z < VoxelData.chunkWidth; z++)
              AddVoxelDataToChunk(new Vector3(x, y, z));
    }

    /// <summary>
    /// Check if there's a voxel in a certain position
    /// Only returns true if that block its solid
    /// </summary>
    /// <param name="pos">that "certain position"</param>
    /// <returns>true if there's actually a voxel in that position</returns>
    private bool HasVoxelThere(Vector3 pos) 
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
            return world.blockTypes[world.GetVoxel(pos + Position)].isSolid;
        }

        return world.blockTypes[voxelMap[x, y, z]].isSolid;
    }

    /// <summary>
    /// * if theres is no voxel againts that face:
    /// Get the current triangle
    /// Add the vertices, uvs and triangles to its list
    /// Set the position of the voxels
    /// </summary>
    /// <param name="pos"></param>
    private void AddVoxelDataToChunk(Vector3 pos) 
    {
        for (int i = 0; i < VoxelData.voxelTris.GetLength(0); i++) {
            if (!HasVoxelThere(pos + VoxelData.faceChecks[i])) {
                byte blockID = voxelMap[(int) pos.x, (int) pos.y, (int) pos.z];

                for (int j = 0; j < VoxelData.voxelTris.GetLength(1); j++)
                    vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[i, j]]);
                for (int j = 0; j < VoxelData.voxelTris.GetLength(0); j++)
                    triangles.Add(vertexIndex + VoxelData.vertexOrder[j]);

                AddTexture(world.blockTypes[blockID].GetTextureID(i));

                vertexIndex += 4;
            }
        }
    }

    /// <summary>
    /// Create the mesh and recalculate the uvs normals
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

    /// <summary>
    /// Normalize the textures
    /// Add and offset the textures in the uv
    /// </summary>
    /// <param name="textureID"></param>
    private void AddTexture(int textureID)
    {
        // Using the Block.png texture as reference
        float y = textureID / VoxelData.textureAtlasSizeInBlocks;
        float x = textureID - (y * VoxelData.textureAtlasSizeInBlocks);

        x *= VoxelData.NormalizedBlockTextureSize;
        y *= VoxelData.NormalizedBlockTextureSize;
        y = 1f - y - VoxelData.NormalizedBlockTextureSize;

        uvs.Add(new Vector2(x, y));
        uvs.Add(new Vector2(x, y + VoxelData.NormalizedBlockTextureSize));
        uvs.Add(new Vector2(x + VoxelData.NormalizedBlockTextureSize, y));
        uvs.Add(new Vector2(x + VoxelData.NormalizedBlockTextureSize, y + VoxelData.NormalizedBlockTextureSize));
    }
}

public class ChunkCoord
{
    public int x;
    public int z;

    public ChunkCoord(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    /// <summary>
    /// Check if a particular chunk is the same as another previous chunk
    /// The metric used to identify each chunk, its the Chunk Coordinate
    /// </summary>
    /// <param name="other">the recent chunk</param>
    /// <returns>true if that chunk already exists, if not false</returns>
    public bool Equals(ChunkCoord other)
    {
        if (other == null) return false;
        else if (other.x == x && other.z == z) return true;
        else return false;
    }
}