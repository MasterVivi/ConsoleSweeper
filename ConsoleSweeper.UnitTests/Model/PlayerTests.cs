using ConsoleSweeper.Enums;
using ConsoleSweeper.Model;
using ConsoleSweeper.Settings;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleSweeper.UnitTests.Model
{
    [TestFixture()]
    public class PlayerTests
    {
        private Player _player;

        [SetUp]
        public void Setup()
        {
            _player = new Player();
        }

        [Test()]
        public void TestInitialVariablesSetCorrectlyForNewInstance()
        {
            // Given
            var startingPosition = new Position(0, Constants.INITIAL_START_ROW);

            // Then
            _player.Lives.Should().Be(Constants.NUMBER_OF_LIVES);
            _player.Score.Should().Be(0);
            _player.IsAlive.Should().BeTrue();
            _player.CurrentPosition.Should().BeEquivalentTo(startingPosition);
        }

        [TestCase(2, Result = true)]
        [TestCase(0, Result = false)]
        public bool TestIsAliveLogicForGivenLives(int lives)
        {
            // When
            _player.Lives = lives;

            // Then
            return _player.IsAlive;
        }

        [Test()]
        public void TestStateUpdateOnPlayerScore()
        {
            // When
            _player.Scored();

            // Then
            _player.Score.Should().Be(1);
        }

        [Test()]
        public void TestStateUpdateOnPlayerMineHit()
        {
            // When
            _player.MineHit();

            // Then
            _player.Lives.Should().Be(Constants.NUMBER_OF_LIVES - 1);
        }

        [Test()]
        public void TestVariablesAfterResetOfPlayer()
        {
            // Given
            _player.Lives = 0;
            _player.Score = 8;
            _player.CurrentPosition.PositionX = 7;
            _player.CurrentPosition.PositionY = 4;

            var startingPosition = new Position(0, Constants.INITIAL_START_ROW);

            // When
            _player.Reset();

            // Then
            _player.Lives.Should().Be(Constants.NUMBER_OF_LIVES);
            _player.Score.Should().Be(0);
            _player.IsAlive.Should().BeTrue();
            _player.CurrentPosition.Should().BeEquivalentTo(startingPosition);
        }

        [TestCase(InputEnum.UP, 0, 0, Result = false)]
        [TestCase(InputEnum.UP, 0, 1, Result = true)]
        [TestCase(InputEnum.LEFT, 0, 0, Result = false)]
        [TestCase(InputEnum.LEFT, 1, 0, Result = true)]
        [TestCase(InputEnum.RIGHT, (Constants.BOARD_WIDTH - 1), 0, Result = false)]
        [TestCase(InputEnum.RIGHT, (Constants.BOARD_WIDTH - 2), 0, Result = true)]
        [TestCase(InputEnum.DOWN, 0, (Constants.BOARD_HEIGHT - 1), Result = false)]
        [TestCase(InputEnum.DOWN, 0, (Constants.BOARD_HEIGHT - 2), Result = true)]
        [TestCase(InputEnum.UNKNOWN, 2, 2, Result = false)]
        public bool TestIfNextBoardMoveIsAllowedForPlayerGivenADirection(InputEnum direction, int xLocation, int yLocation)
        {
            // Given
            _player.CurrentPosition.PositionX = xLocation;
            _player.CurrentPosition.PositionY = yLocation;

            // When
            return _player.IsValidMove(direction);
        }

        [TestCase(InputEnum.UP, 2, 1)]
        [TestCase(InputEnum.LEFT, 1, 2)]
        [TestCase(InputEnum.RIGHT, 3, 2)]
        [TestCase(InputEnum.DOWN, 2, 3)]
        public void TestIfNextBoardMoveForPlayerGivenADirection(InputEnum direction, int targetXLocation, int targetYLocation)
        {
            // Given
            _player.CurrentPosition.PositionX = 2;
            _player.CurrentPosition.PositionY = 2;

            var targetPosition = new Position(targetXLocation, targetYLocation);

            // When
            _player.Move(direction);

            // Then
            _player.CurrentPosition.Should().BeEquivalentTo(targetPosition);
        }

        [TearDown]
        public void Cleanup()
        {
            _player = null;
        }
    }
}
