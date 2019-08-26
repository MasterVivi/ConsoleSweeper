using ConsoleSweeper.Enums;

namespace ConsoleSweeper.Model
{
    public interface IPlayer
    {
        int Lives { get; set; }
        int Score { get; set; }
        bool IsAlive { get; }
        Position CurrentPosition { get; }

        void Scored();
        void MineHit();
        void Reset();

        bool IsValidMove(InputEnum direction);
        void Move(InputEnum direction);
    }
}