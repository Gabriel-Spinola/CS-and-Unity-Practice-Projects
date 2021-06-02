using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float Get2DPerlin(Vector2 position, float offset, float scale)
    {
        return Mathf.PerlinNoise(
            x: (position.x + 0.1f) / VoxelData.chunkWidth * scale + offset,
            y: (position.y + 0.1f) / VoxelData.chunkWidth * scale + offset
        );
    }
}
