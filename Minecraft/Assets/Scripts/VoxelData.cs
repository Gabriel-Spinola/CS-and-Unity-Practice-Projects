﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Store the data that we will need to use when rendering the voxels
// Face Order: Back, Front, Top, Bottom, Left, Right
public static class VoxelData
{
    public static readonly int chunkWidth  = 8;
    public static readonly int chunkHeight = 8;
    public static readonly int worldSizeInChunks = 100;

    public static readonly int textureAtlasSizeInBlocks = 4;

    public static readonly int viewDistanceInChunks = 5;
    
    public static int WorldSizeInVoxels {
        get { return worldSizeInChunks * chunkWidth; }
    }

    public static float NormalizedBlockTextureSize {
        get { return 1f / (float) textureAtlasSizeInBlocks; }
    }

    [Tooltip("Store voxel vertex locations")]
    public static readonly Vector3[] voxelVerts = new Vector3[8] {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 1.0f, 1.0f),
        new Vector3(0.0f, 1.0f, 1.0f),
    };

    [Tooltip("Store the position of the voxel faces")]
    public static readonly Vector3[] faceChecks = new Vector3[6] {
        new Vector3( 0.0f,  0.0f, -1.0f), // Check Back   Face
        new Vector3( 0.0f,  0.0f,  1.0f), // Check Front  Face
        new Vector3( 0.0f,  1.0f,  0.0f), // Check Bottom Face
        new Vector3( 0.0f, -1.0f,  0.0f), // Check Top    Face
        new Vector3(-1.0f,  0.0f,  0.0f), // Check Left   Face
        new Vector3( 1.0f,  0.0f,  0.0f), // Check Right  Face
    };

    [Tooltip("Store the voxels triangles (\"who create the faces\")")] 
    public static readonly int[,] voxelTris = new int[6, 4] {
        // 0 1 2 2 1 3
        { 0, 3, 1, 2 }, // Back   Face
        { 5, 6, 4, 7 }, // Front  Face
        { 3, 7, 2, 6 }, // Top    Face
        { 1, 5, 0, 4 }, // Bottom Face
        { 4, 7, 0, 3 }, // Left   Face
        { 1, 2, 5, 6 }, // Right  Face
    };

    [Tooltip("Store the voxel uvs positions")]
    public static readonly Vector2[] voxelUvs = new Vector2[4] {
        new Vector2(0.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(1.0f, 1.0f),
    };

    public static readonly int[] vertexOrder = new int[6] { 
        0, 1, 2, 2, 1, 3
    };
}
