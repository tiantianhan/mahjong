using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Game
    {
        public enum State
        {
            Waiting,
            Started,
        }

        private State state = State.Waiting;

        private int numPlayers;

        private int numTilesPerHand;

        private Deck drawPile = new();
        private Deck discardPile = new();
        private Player currentPlayer;

        private List<Player> players = new();

        public class Save
        {
            public List<Tile> drawPile;
        }

        public Game(int numPlayers, int numTilesPerHand)
        {
            this.numPlayers = numPlayers;
            this.numTilesPerHand = numTilesPerHand;
        }

        public Save GetSave()
        {
            Save newSave = new();
            newSave.drawPile = drawPile.GetTiles();
            return newSave;
        }

        public void LoadTiles(List<Tile> tiles)
        {
            Debug.Log("Load Model Tiles");
            drawPile.Add(tiles);
        }

        public bool isGameFull()
        {
            return players.Count == numPlayers;
        }

        public string GetNextPlayerID()
        {
            return isGameFull() ? null : (players.Count + 1).ToString();
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void Start()
        {
            currentPlayer = players[0];
            state = State.Started;
        }

        public void Shuffle()
        {
            drawPile.Shuffle();
        }

        public void Deal()
        {
            for (int i = 0; i < numTilesPerHand; i++)
            {
                foreach (Player player in players)
                {
                    player.Draw(drawPile);
                }
            }
        }

        public List<Player> GetPlayers()
        {
            return new List<Player>(players);
        }
    }
}
