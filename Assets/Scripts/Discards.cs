using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Discards : MonoBehaviour
{
    [SerializeField]
    List<Tile> discards;

    public void AddToDiscards(Tile tile)
    {
        discards.Add(tile);
        tile.MoveToContainer(this.transform);
    }

    public void ReturnToDeck(Deck deck)
    {
        deck.ReturnToDeck(discards);
        discards = new List<Tile>();
    }
}
