using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

/**
* Deck of tiles not yet distributed to players
*/
public class Deck : MonoBehaviour
{
    [SerializeField]
    private Tile tilePrefab;

    [SerializeField]
    private List<Tile> tiles;

    void Awake()
    {
        LoadStartingDeck();

    }

    public void Shuffle()
    {
        // Fisher-Yates Shuffle. For all but last 
        // item, swap item in place with a random item in the 
        // remainder of the list
        // Results in equal probability for each permutation of the list
        for (int i = 0; i < tiles.Count - 1; i++)
        {
            int swap_index = Random.Range(i, tiles.Count - 1);
            Tile temp = tiles[i];
            tiles[i] = tiles[swap_index];
            tiles[swap_index] = temp;
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

    void LoadStartingDeck()
    {
        TileAttributes[] attrs = LoadAttributeListFromResources();

        foreach (TileAttributes attributes in attrs)
        {
            for (int i = 0; i < attributes.count; i++)
            {
                tiles.Add(Tile.SpawnWithAttributes(tilePrefab, attributes, this.transform));
            }
        }
    }

    TileAttributes[] LoadAttributeListFromResources()
    {
        TileAttributes[] tileAttributes = Resources.LoadAll<TileAttributes>("TileAttributes");
        return tileAttributes;
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
