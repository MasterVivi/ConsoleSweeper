namespace ConsoleSweeper.Model
{
    public class Tile : ITile
    {
        public bool IsMine { get; set; }

        public bool IsHit { get; set; }

        public Position Position { get; set; }

        /// <summary>
        /// Text represenatation of tile state
        /// </summary>
        /// <returns>State of tile</returns>
        public override string ToString()
        {
            if (IsHit)
            {
                return IsMine ? "B" : "X";
            }

            return "O";
        }
    }
}