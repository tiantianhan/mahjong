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
    private List<Player> playerOrder;

    void Awake()
    {
        totalPlayers = playerOrder.Count;
        currentPlayerIndex = 0;
        currentPlayer = playerOrder[currentPlayerIndex];
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

    public void NextTurn()
    {
        currentPlayerIndex += 1;
        currentPlayerIndex = currentPlayerIndex % totalPlayers;
        currentPlayer = playerOrder[currentPlayerIndex];
        Debug.Log("Current player index " + currentPlayerIndex);
    }

}
