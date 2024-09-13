using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class LevelGenerator : MonoBehaviour
{

    public Tilemap tilemap;
    public TileBase tile;

    public int width = 10;
    public int height = 10;

    void Start()
    {
        GenerateTilemap();
    }

    void GenerateTilemap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}