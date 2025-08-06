using System.Collections.Generic;

namespace Model
{
    public class Hand
    {
        public List<Tile> tiles;

        public void Add(Tile tile)
        {
            tiles.Add(tile);
        }
    }
}
