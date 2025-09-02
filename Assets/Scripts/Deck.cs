using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using Unity.Properties;
using UnityEngine;
using TileModel = Model.Tile;

/**
* Deck of tiles not yet distributed to players
* TODO: No longer track list of tiles, instead track number of tiles in the deck
*/
public class Deck : MonoBehaviour
{
    [SerializeField]
    private Tile tilePrefab;

    [SerializeField]
    private List<Tile> tiles;

    [SerializeField]
    private TileSetLookup tileDeckLookup;

    void Start()
    {
        // We get tiles in Start because tile set is first loaded in Awake
        GetAllTiles();
    }

    void GetAllTiles()
    {
        tiles = tileDeckLookup.GetAllTiles();

        // Move tile view physically
        foreach (Tile tile in tiles)
        {
            tile.gameObject.transform.parent = this.transform;
        }
    }

    public Tile DrawFromTop()
    {
        Tile tile = tiles[0];
        tiles.RemoveAt(0);
        return tile;
    }

    public Tile DrawFromBottom()
    {
        Tile tile = tiles[tiles.Count - 1];
        tiles.RemoveAt(tiles.Count - 1);
        return tile;
    }

    public void ReturnToDeck(List<Tile> tiles)
    {
        foreach (Tile tile in tiles)
        {
            tile.MoveToContainer(this.transform);
            this.tiles.Add(tile);
        }
    }
}
