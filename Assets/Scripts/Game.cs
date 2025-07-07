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

    public int handCount = 13;

    void Awake()
    {
        totalPlayers = playersInOrder.Count;
        currentPlayerIndex = 0;
        currentPlayer = playersInOrder[currentPlayerIndex];
        Debug.Log("Current player index " + currentPlayerIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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

}
