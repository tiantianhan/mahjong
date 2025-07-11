using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Hand : MonoBehaviour, Tile.ITileClickedHandler
{
    [SerializeField]
    private List<Tile> tiles;

    private Tile selectedTile;

    [SerializeField]
    private UnityEvent<bool, Tile> OnSelectedTileUpdated;

    public void Add(Tile tile)
    {
        tiles.Add(tile);
    }

    public void Discard(Tile tile)
    {
        if (tile == selectedTile)
            UpdateSelectedTile(null);
        tiles.Remove(tile);
    }

    public void Order()
    {
        tiles = tiles.OrderBy(t => t.GetOrder()).ToList();
    }

    public void Layout()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].gameObject.transform.parent = this.transform;
            tiles[i].gameObject.transform.localRotation = Quaternion.identity;
            // TODO: Decouple layout implementation from model of game and game state 
            tiles[i].gameObject.transform.localPosition = Vector3.right * i * 7 / 13f;  // Board size / Number of tiles
            tiles[i].TileSelectedHandler = this;
        }
    }

    public void OnTileClicked(Tile tile)
    {
        tile.ToggleSelected();

        // Track selected tile
        UpdateSelectedTile(tile.IsSelected() ? tile : null);

        // Select only one tile at a time
        if (tile.IsSelected())
        {
            foreach (Tile otherTile in tiles)
            {
                if (otherTile != tile & otherTile.IsSelected())
                    otherTile.SetSelected(false);
            }
        }
    }

    public void SetSelectedTile(Tile tile)
    {
        if (tile)
            tile.SetSelected(true);
        UpdateSelectedTile(tile);
    }

    void UpdateSelectedTile(Tile tile)
    {
        selectedTile = tile;
        OnSelectedTileUpdated.Invoke(tile ? true : false, tile);
    }

    public Tile GetSelectedTile()
    {
        return selectedTile;
    }

    public void ReturnToDeck(Deck deck)
    {
        if (selectedTile)
            SetSelectedTile(null);

        deck.ReturnToDeck(tiles);
        tiles = new List<Tile>();
    }
}
