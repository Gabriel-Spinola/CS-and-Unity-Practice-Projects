using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Transform playerTransform;
    public Material material;
    public BlockType[] blockTypes;

    public Vector3 spawnPosition;

    private Chunk[,] chunks = new Chunk[VoxelData.worldSizeInChunks, VoxelData.worldSizeInChunks];
    private List<ChunkCoord> activeChunks = new List<ChunkCoord>();
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
        CheckViewDistance();
    }

    public byte GetVoxel(Vector3 pos)
    {
        if (!IsVoxelInWorld(pos))                     return 0;
        if (pos.y < 2)                                return 1;
        else if (pos.y == VoxelData.chunkHeight - 1)  return 3;
        else                                          return 2;
    }

    private ChunkCoord GetChunkCoordFromVector3(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x / VoxelData.chunkWidth);
        int z = Mathf.FloorToInt(pos.z / VoxelData.chunkWidth);

        return new ChunkCoord(x, z);
    }

    private void CheckViewDistance()
    {
        ChunkCoord coord = GetChunkCoordFromVector3(playerTransform.position);
        List<ChunkCoord> previouslyActiveChunks = new List<ChunkCoord>(activeChunks);

        for (int x = coord.x - VoxelData.viewDistanceInChunks; x < coord.x + VoxelData.viewDistanceInChunks; x++) {
            for (int z = coord.z - VoxelData.viewDistanceInChunks; z < coord.z + VoxelData.viewDistanceInChunks; z++) {
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

        foreach (ChunkCoord c in previouslyActiveChunks) {
            chunks[c.x, c.z].IsActive = false;
        }
    }

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

    private void CreateNewChunk(int x, int z)
    {
        chunks[x, z] = new Chunk(new ChunkCoord(x, z), this);

        activeChunks.Add(new ChunkCoord(x, z));
    }

    private bool IsChunkInWorld(ChunkCoord coord) => 
        coord.x > 0                               &&
        coord.x < VoxelData.worldSizeInChunks - 1 &&
        coord.z > 0                               &&
        coord.z < VoxelData.worldSizeInChunks - 1;

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

    // Order: Back, Front, Bottom, Top, Left, Right
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