namespace Model
{
    public class Player
    {
        public string playerID;
        public string playerName;

        public Hand hand;

        public Player(string playerID, string playerName)
        {
            this.playerID = playerID;
            this.playerName = playerName;
            this.hand = new();
        }

        public void Draw(Deck deck)
        {
            Tile drawnTile = deck.DrawFromTop();
            hand.Add(drawnTile);
        }
    }
}
