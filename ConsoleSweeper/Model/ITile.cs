namespace ConsoleSweeper.Model
{
    public interface ITile
    {
        bool IsHit { get; set; }
        bool IsMine { get; set; }
        Position Position { get; set; }
    }
}