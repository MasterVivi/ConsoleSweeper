using ConsoleSweeper.Enums;

namespace ConsoleSweeper.Logic
{
    public interface IGame
    {
        bool Finished { get; }

        InputEnum ProcessInput(char input);

        void Run();
    }
}