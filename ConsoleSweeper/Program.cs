using System;
using ConsoleSweeper.Logic;
using ConsoleSweeper.Model;
using ConsoleSweeper.Wrapper;

namespace ConsoleSweeper
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Setup.Launch();
            var console = Setup.Container.GetInstance<IConsoleWrapper>();
            var board = Setup.Container.GetInstance<IBoard>();
            var player = Setup.Container.GetInstance<IPlayer>();

            IGame game = new Game(console, board, player);
            game.Run();
        }
    }
}