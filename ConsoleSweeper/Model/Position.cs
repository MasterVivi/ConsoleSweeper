using System;
namespace ConsoleSweeper.Model
{
    public class Position : IPosition
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public Position(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }
    }
}
