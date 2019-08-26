using ConsoleSweeper.Model;

namespace ConsoleSweeper.Logic
{
    public interface IBoard
    {
        ITile[,] Tiles { get; }
        ITile GetTile(int x, int y);

        void Create(int rows, int coloumns, Position playerPosition);
        void PopulateMines(int mines, Position playerPosition);
        bool CheckCollision(Position playerPosition);
        void Reset(Position playerPosition);

        void Draw();
    }
}