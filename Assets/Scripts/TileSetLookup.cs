using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using TileModel = Model.Tile;

/// <summary>
/// Master list of all tiles and used to look up tiles from index
/// </summary>
public class TileSetLookup : MonoBehaviour
{
    [SerializeField]
    private List<Tile> allTiles;

    [SerializeField]
    Tile tilePrefab;

    void Awake()
    {
        LoadTileSet();
    }

    void LoadTileSet()
    {
        allTiles = new();
        List<TileModel> tileModels = TileSetLoader.LoadAllTileModels();
        foreach (TileModel model in tileModels)
        {
            allTiles.Add(Tile.SpawnWithModel(tilePrefab, model, this.transform));
        }
    }

    /// <summary>
    /// Get tile given its index from 1 - total number of tiles
    /// </summary>
    public Tile GetTileForIndex(int index)
    {
        return allTiles[index - 1];
    }

    /// <summary>
    /// Return shallow copy of references to all the tiles
    /// </summary>
    public List<Tile> GetAllTiles()
    {
        return new List<Tile>(allTiles);
    }
}
