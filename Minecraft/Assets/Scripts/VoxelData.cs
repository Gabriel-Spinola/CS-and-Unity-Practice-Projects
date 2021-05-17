using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Store the data that we will need to use when rendering the voxels
public static class VoxelData
{
    public static readonly int chunkWidth  = 16;
    public static readonly int chunkHeight = 16;

    // Store the voxels vertices locations
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

    // Get the coordinates to check the faces of the voxel
    public static readonly Vector3[] faceChecks = new Vector3[6] {
        new Vector3( 0.0f,  0.0f, -1.0f), // Check Back   Face
        new Vector3( 0.0f,  0.0f,  1.0f), // Check Front  Face
        new Vector3( 0.0f,  1.0f,  0.0f), // Check Bottom Face
        new Vector3( 0.0f, -1.0f,  0.0f), // Check Top    Face
        new Vector3(-1.0f,  0.0f,  0.0f), // Check Left   Face
        new Vector3( 1.0f,  0.0f,  0.0f), // Check Right  Face
    };

    // Store the voxels triangles ("faces") 
    public static readonly int[,] voxelTris = new int[6, 4] {
        // 0 1 2 2 1 3
        { 0, 3, 1, 2 }, // Back   Face
        { 5, 6, 4, 7 }, // Front  Face
        { 3, 7, 2, 6 }, // Top    Face
        { 1, 5, 0, 4 }, // Bottom Face
        { 4, 7, 0, 3 }, // Left   Face
        { 1, 2, 5, 6 }, // Right  Face
    };

    // Store the voxels uvs positions
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
