using System.Collections.Generic;

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

        public Game(int numPlayers, int numTilesPerHand)
        {
            this.numPlayers = numPlayers;
            this.numTilesPerHand = numTilesPerHand;
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
            return players;
        }
    }
}
