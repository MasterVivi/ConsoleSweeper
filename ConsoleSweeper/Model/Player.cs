using ConsoleSweeper.Enums;
using ConsoleSweeper.Settings;

namespace ConsoleSweeper.Model
{
    public class Player : IPlayer
    {
        public int Lives { get; set; } = Constants.NUMBER_OF_LIVES;

        public int Score { get; set; }

        /// <summary>
        /// True if player still alive in game
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return Lives > 0;
            }
        }

        public Position CurrentPosition { get; set; } = new Position(0, Constants.INITIAL_START_ROW);

        public void Scored()
        {
            Score++;
        }

        /// <summary>
        /// Check next movement is valid
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool IsValidMove(InputEnum direction)
        {
            switch (direction)
            {
                case InputEnum.UP:
                    return CurrentPosition.PositionY > 0;
                case InputEnum.LEFT:
                    return CurrentPosition.PositionX > 0;
                case InputEnum.DOWN:
                    return CurrentPosition.PositionY < (Constants.BOARD_HEIGHT - 1);
                case InputEnum.RIGHT:
                    return CurrentPosition.PositionX < (Constants.BOARD_WIDTH - 1);
            }

            return false;
        }

        /// <summary>
        /// Move the current position in the direction specified
        /// </summary>
        /// <param name="direction">W, A, S, D</param>
        /// <returns>True if a mine has been hit</returns>
        public void Move(InputEnum direction)
        {
            switch (direction)
            {
                case InputEnum.UP:
                    CurrentPosition.PositionY--;
                    break;
                case InputEnum.LEFT:
                    CurrentPosition.PositionX--;
                    break;
                case InputEnum.DOWN:
                    CurrentPosition.PositionY++;
                    break;
                case InputEnum.RIGHT:
                    CurrentPosition.PositionX++;
                    break;
            }
        }

        /// <summary>
        /// Update player after collision with mine
        /// </summary>
        public void MineHit()
        {
            Lives--;
        }

        /// <summary>
        /// Reset player for new game
        /// </summary>
        public void Reset()
        {
            Lives = Constants.NUMBER_OF_LIVES;
            Score = 0;

            CurrentPosition.PositionX = 0;
            CurrentPosition.PositionY = Constants.INITIAL_START_ROW;
        }
    }
}