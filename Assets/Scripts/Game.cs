using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Player ownPlayer;

    [SerializeField]
    private Deck deck;

    [SerializeField]
    private Discards discards;

    public void DealHand(List<Tile> tiles)
    {
        Debug.Log("Game view deals hand");
        ownPlayer.SetInitialHand(tiles);
    }
}
