using System;
using ConsoleSweeper.Enums;
using ConsoleSweeper.Model;
using ConsoleSweeper.Settings;
using ConsoleSweeper.Wrapper;

namespace ConsoleSweeper.Logic
{
    public class Game : IGame
    {
        private readonly IConsoleWrapper _console;
        private readonly IBoard _board;
        private readonly IPlayer _player;

        /// <summary>
        /// End check for game loop
        /// </summary>
        public bool Finished => !_player.IsAlive || _player.CurrentPosition.PositionX == (Constants.BOARD_WIDTH - 1);

        public Game(IConsoleWrapper console, IBoard board, IPlayer player)
        {
            _console = console;
            _board = board;
            _player = player;

            // Configure the board
            _board.Create(Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT, _player.CurrentPosition);
            _board.PopulateMines(Constants.NUMBER_OF_MINES, _player.CurrentPosition);
        }

        /// <summary>
        /// Main game loop
        /// </summary>
        public void Run()
        {
            while (!Finished)
            {
                Draw();

                var movement = ProcessInput(_console.ReadKey().KeyChar);
                if (movement != InputEnum.UNKNOWN && _player.IsValidMove(movement))
                {
                    _player.Move(movement);

                    if (_board.CheckCollision(_player.CurrentPosition))
                        _player.MineHit();
                    else
                        _player.Scored();
                }
            }

            if (Finished)
            {
                Draw();

                _console.WriteLine(_player.IsAlive ? "Congratulations Game Complete" : "Game Over");
                _console.WriteLine("Restart Game? (Y)");

                var restart = ProcessInput(_console.ReadKey().KeyChar);
                if (restart == InputEnum.YES)
                {
                    _console.Clear();

                    _player.Reset();
                    _board.Reset(_player.CurrentPosition);
                    _board.PopulateMines(Constants.NUMBER_OF_MINES, _player.CurrentPosition);

                    Run();
                }
            }
        }

        /// <summary>
        /// Process user input from the command line
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public InputEnum ProcessInput(char input)
        {
            switch (Char.ToUpper(input))
            {
                case 'W':
                    return InputEnum.UP;
                case 'A':
                    return InputEnum.LEFT;
                case 'S':
                    return InputEnum.DOWN;
                case 'D':
                    return InputEnum.RIGHT;
                case 'Y':
                    return InputEnum.YES;
            }

            return InputEnum.UNKNOWN;
        }

        /// <summary>
        /// Draw the game screen
        /// </summary>
        private void Draw()
        {
            PrintHeader();
            _board.Draw();
        }

        /// <summary>
        /// Printing game stats
        /// </summary>
        private void PrintHeader()
        {
            _console.SetCursorPosition(0, 0);
            _console.WriteLine("=== Traverse the Board to WIN ===");
            _console.WriteLine("            ");
            _console.WriteLine("Score: {0}", _player.Score);
            _console.WriteLine("            ");
            _console.WriteLine("Current Position: ");
            _console.WriteLine("Row:    {0}", _player.CurrentPosition.PositionX);
            _console.WriteLine("Column: {0}", _player.CurrentPosition.PositionY);
            _console.WriteLine("            ");
            _console.WriteLine("Lives: {0}", _player.Lives);
            _console.WriteLine("            ");
        }
    }
}