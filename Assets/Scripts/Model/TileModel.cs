using System;

namespace Model
{
    public class Tile
    {
        // Each tile is assigned one of the numbers from 1 to the total number of tiles
        // This number can be used as an index to an array or as a key to a map of all tiles
        public int index;
        public string type;
        public int number;

        // Indicates the tile's sorting order, where some tiles may have the same
        // sorting order
        public int order;

        public string GetNotation()
        {
            return number <= 0 ? type : type + number;
        }

        public string GetUniqueNotation()
        {
            return index + "-" + GetNotation();
        }
    }
}
