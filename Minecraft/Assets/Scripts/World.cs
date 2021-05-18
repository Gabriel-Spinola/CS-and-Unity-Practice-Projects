﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Material material;
    public BlockType[] blockTypes;
}

[System.Serializable]
public class BlockType
{
    public string name;
    public bool isSolid;

    [Space]
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