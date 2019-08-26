using System;
using ConsoleSweeper.Model;

namespace ConsoleSweeper.Logic
{
    public class Board : IBoard
    {
        private int _creationWidth;
        private int _creationHeight;

        public ITile[,] Tiles { get; private set; }

        public ITile GetTile(int x, int y)
        {
            return Tiles[y, x];
        }

        /// <summary>
        /// Setup board based on configuration passed
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="playerPosition"></param>
        public void Create(int width, int height, Position playerPosition)
        {
            Tiles = new Tile[height, width];
            _creationWidth = width;
            _creationHeight = height;

            Reset(playerPosition);
        }

        /// <summary>
        /// Reset instances of Tiles to starting state
        /// </summary>
        /// <param name="playerPosition"></param>
        public void Reset(Position playerPosition)
        {
            for (int y = 0; y < Tiles.GetLength(0); y++)
            {
                for (int x = 0; x < Tiles.GetLength(1); x++)
                {
                    var newTile = new Tile
                    {
                        Position = new Position(x, y),
                    };

                    Tiles[y, x] = newTile;
                }
            }

            // Set initial location on board
            var tile = GetTile(playerPosition.PositionX, playerPosition.PositionY);
            tile.IsHit = true;
        }

        /// <summary>
        /// Generate mines on the board
        /// </summary>
        /// <param name="mines"></param>
        /// <param name="playerPosition"></param>
        public void PopulateMines(int mines, Position playerPosition)
        {
            var mineCount = 0;
            var random = new Random();
            while (mineCount < mines)
            {
                int randomColumn = random.Next(0, _creationWidth);
                int randomRow = random.Next(0, _creationHeight);

                // Skip for starting position
                if (randomRow == playerPosition.PositionY && randomColumn == playerPosition.PositionX)
                    continue;

                // Already created
                if (Tiles[randomRow, randomColumn].IsMine)
                    continue;

                Tiles[randomRow, randomColumn].IsMine = true;
                mineCount++;
            }
        }

        /// <summary>
        /// Check if collision has occurred between a tile and a new player position
        /// </summary>
        /// <param name="playerPosition"></param>
        /// <returns></returns>
        public bool CheckCollision(Position playerPosition)
        {
            var tile = GetTile(playerPosition.PositionX, playerPosition.PositionY);
            tile.IsHit = true;

            if (tile.IsMine)
                return true;

            return false;
        }

        /// <summary>
        /// Draw the game board
        /// </summary>
        public void Draw()
        {
            for (int y = 0; y < Tiles.GetLength(0); y++)
            {
                for (int x = 0; x < Tiles.GetLength(1); x++)
                {
                    Console.Write(Tiles[y, x] + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("            ");
            Console.WriteLine("Keyboard Mapping: W = UP | A = LEFT | S = DOWN | D = RIGHT ");
            Console.WriteLine("            ");
            Console.WriteLine("Select your move");
            Console.WriteLine("            ");
        }
    }
}