using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Player currentPlayer;

    private int currentPlayerIndex = 0;
    private int totalPlayers = 0;

    [SerializeField]
    private List<Player> playersInOrder;

    [SerializeField]
    private Deck deck;

    [SerializeField]
    private Discards discards;

    [SerializeField]

    public int handCount = 13;

    void Awake()
    {
        totalPlayers = playersInOrder.Count;
        ResetCurrentPlayer();
        Debug.Log("Current player index " + currentPlayerIndex);
    }

    public void DrawHands()
    {
        for (int i = 0; i < handCount; i++)
        {
            foreach (Player player in playersInOrder)
            {
                player.Draw(deck);
            }
        }
    }

    public void NextTurn()
    {
        currentPlayerIndex += 1;
        currentPlayerIndex = currentPlayerIndex % totalPlayers;
        currentPlayer = playersInOrder[currentPlayerIndex];
        Debug.Log("Current player index " + currentPlayerIndex);
    }

    public void Draw()
    {
        currentPlayer.DrawAndSelect(deck);
    }

    public void Discard()
    {
        currentPlayer.Discard(discards);
    }

    public void Restart()
    {
        ResetCurrentPlayer();
        discards.ReturnToDeck(deck);

        foreach (Player player in playersInOrder)
        {
            player.ReturnHandToDeck(deck);
        }
    }

    void ResetCurrentPlayer()
    {
        currentPlayerIndex = 0;
        currentPlayer = playersInOrder[currentPlayerIndex];
    }
}
