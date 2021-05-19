using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [Header("References")]
    public Transform playerTransform;
    public Material material;

    [Tooltip("Order: \n 0 Back, 1 Front, 2 Top, \n 3 Bottom, 4 Left, 5 Right")]
    public BlockType[] blockTypes;

    [HideInInspector] public Vector3 spawnPosition;

    private Chunk[,] chunks = new Chunk[VoxelData.worldSizeInChunks, VoxelData.worldSizeInChunks];
    private List<ChunkCoord> activeChunks = new List<ChunkCoord>();
    private ChunkCoord playerChunkCoord;
    private ChunkCoord playerLastChunkCoord;

    private void Start()
    {
        spawnPosition = new Vector3(
            x: (VoxelData.worldSizeInChunks * VoxelData.chunkWidth) / 2f,
            y:  VoxelData.chunkHeight + 5f,
            z: (VoxelData.worldSizeInChunks * VoxelData.chunkWidth) / 2f
        );

        GenerateWorld();

        playerLastChunkCoord = GetChunkCoordFromVector3(playerTransform.position);
    }

    private void Update()
    {
        playerChunkCoord = GetChunkCoordFromVector3(playerTransform.position);

        if (!playerChunkCoord.Equals(playerLastChunkCoord))
            CheckViewDistance();
    }

    /// <summary>
    ///  Get the voxel located in a specific position
    ///  
    ///  Obs:
    ///  * The voxels are defined in the editor
    ///  * Voxels can be stone, bedrock, air, etc...
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>The voxel identifier</returns>
    public byte GetVoxel(Vector3 pos)
    {
        if (!IsVoxelInWorld(pos))                     return 0;
        if (pos.y < 2)                                return 1;
        else if (pos.y == VoxelData.chunkHeight - 1)  return 3;
        else                                          return 2;
    }

    /// <summary>
    /// Convert a vector3 position to a chunk coordinate
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>Chunk coordinate</returns>
    private ChunkCoord GetChunkCoordFromVector3(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x / VoxelData.chunkWidth);
        int z = Mathf.FloorToInt(pos.z / VoxelData.chunkWidth);

        return new ChunkCoord(x, z);
    }

    /// <summary>
    /// This makes us only create chunks when they are within the view distance
    /// and also disable the chunks that are outside the view distance.
    /// 
    /// - Get the player's position
    /// 
    /// * When looping the chunks that are in the viewing distance:
    ///  * check if that piece must be in the world, if true:
    ///   - if this chunk is null, create one, otherwise and if the chunk is not active, activate it
    /// 
    /// but in a rudimentar algorithm
    /// </summary>
    private void CheckViewDistance()
    {
        ChunkCoord playerCoord = GetChunkCoordFromVector3(playerTransform.position);
        List<ChunkCoord> previouslyActiveChunks = new List<ChunkCoord>(activeChunks);

        for (int x = playerCoord.x - VoxelData.viewDistanceInChunks; x < playerCoord.x + VoxelData.viewDistanceInChunks; x++) {
            for (int z = playerCoord.z - VoxelData.viewDistanceInChunks; z < playerCoord.z + VoxelData.viewDistanceInChunks; z++) {
                if (IsChunkInWorld(new ChunkCoord(x, z))) {
                    if (chunks[x, z] == null) {
                        CreateNewChunk(x, z);
                    }
                    else if (!chunks[x, z].IsActive) {
                        chunks[x, z].IsActive = true;

                        activeChunks.Add(new ChunkCoord(x, z));
                    }
                }

                for (int i = 0; i < previouslyActiveChunks.Count; i++) {
                    if (previouslyActiveChunks[i].Equals(new ChunkCoord(x, z))) {
                        previouslyActiveChunks.RemoveAt(i);
                    }
                }
            } 
        }
        
        // Deactivate the out of range chunks
        foreach (ChunkCoord c in previouslyActiveChunks) {
            chunks[c.x, c.z].IsActive = false;
        }
    }

    /// <summary>
    /// - Generate the world (only in the view distance)
    /// - And set the player's position
    /// </summary>
    private void GenerateWorld()
    {
        int i = (VoxelData.worldSizeInChunks / 2) - VoxelData.viewDistanceInChunks;
        int j = (VoxelData.worldSizeInChunks / 2) + VoxelData.viewDistanceInChunks;

        for (int x = i; x < j; x++) {
            for (int z = i; z < j; z++) {
                CreateNewChunk(x, z);
            }
        }

        playerTransform.position = spawnPosition;
    }

    /// <summary>
    /// Create a new Chunk
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    private void CreateNewChunk(int x, int z)
    {
        chunks[x, z] = new Chunk(new ChunkCoord(x, z), this);

        activeChunks.Add(new ChunkCoord(x, z));
    }

    /// <summary>
    /// Check if a certain chunk should be in this world
    /// </summary>
    /// <param name="coord"></param>
    /// <returns>true if the chunk should be in this world and false if it shouldn't</returns>
    private bool IsChunkInWorld(ChunkCoord coord) => 
        coord.x > 0                               &&
        coord.x < VoxelData.worldSizeInChunks - 1 &&
        coord.z > 0                               &&
        coord.z < VoxelData.worldSizeInChunks - 1;

    /// <summary>
    /// Check if a certain voxel should be in this world
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>true if the voxel should be in this world and false if it shouldn't</returns>
    private bool IsVoxelInWorld(Vector3 pos) => 
        pos.x >= 0                              &&
        pos.x < VoxelData.WorldSizeInVoxels     &&
        pos.y >= 0                              &&
        pos.y < VoxelData.chunkHeight           &&
        pos.z >= 0                              &&
        pos.z < VoxelData.WorldSizeInVoxels;
}

[System.Serializable] 
public class BlockType
{
    [Header("Info")]
    public string name;
    public bool isSolid;

    [Header("Texture Values")]
    public int    backFaceTexture;
    public int   frontFaceTexture;
    public int     topFaceTexture;
    public int bottomFaceTexture;
    public int    leftFaceTexture;
    public int   rightFaceTexture;
    
    /// <summary>
    /// Set the texture ID 
    /// </summary>
    /// <param name="faceIndex">receive what face the function should return</param>
    /// <returns>Texture ID</returns>
    public int GetTextureID(int faceIndex)
    {
        switch (faceIndex) {
            case 0:
                return backFaceTexture;
            case 1:
                return frontFaceTexture;
            case 2:
                return topFaceTexture;
            case 3:
                return bottomFaceTexture;
            case 4:
                return leftFaceTexture;
            case 5:
                return rightFaceTexture;

            default: 
                Debug.LogError("ERROR::BlockType::GetTextureID; Invalid face index");

                return 0;
        }
    }
}