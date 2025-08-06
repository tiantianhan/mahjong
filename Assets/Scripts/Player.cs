using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Hand hand;

    public delegate void PlayerJoined(Player player);
    public static event PlayerJoined OnPlayerJoined;

    void Start()
    {
        OnPlayerJoined(this);
    }

    public Tile Draw(Deck deck)
    {
        Tile drawnTile = deck.DrawFromTop();
        hand.Add(drawnTile);
        hand.Order();
        hand.Layout();

        return drawnTile;
    }

    public void DrawAndSelect(Deck deck)
    {
        Tile drawnTile = Draw(deck);
        hand.SetSelectedTile(drawnTile);
    }

    public void Discard(Discards discardPile)
    {
        Tile selectedTile = hand.GetSelectedTile();
        Debug.Log("Discarding selected " + selectedTile.name);
        if (selectedTile)
        {
            hand.Discard(selectedTile);
            discardPile.AddToDiscards(selectedTile);
            hand.Layout();
        }
    }

    public void ReturnHandToDeck(Deck deck)
    {
        hand.ReturnToDeck(deck);
    }
}
