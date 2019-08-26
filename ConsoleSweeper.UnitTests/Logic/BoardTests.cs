using ConsoleSweeper.Logic;
using ConsoleSweeper.Model;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleSweeper.UnitTests.Logic
{
    [TestFixture()]
    public class BoardTests
    {
        private Board _board;

        [SetUp]
        public void Setup()
        {
            _board = new Board();
        }

        [TestCase(4, 6)]
        [TestCase(6, 12)]
        public void TestCreateLogicForBoardForSize(int width, int height)
        {
            // When
            _board.Create(width, height, new Position(0, 3));

            // Then
            _board.Tiles.GetLength(0).Should().Be(height);
            _board.Tiles.GetLength(1).Should().Be(width);
        }

        [Test()]
        public void TestResetOfBoard()
        {
            // Given
            var playerPosition = new Position(0, 3);

            // When
            _board.Create(8, 8, playerPosition);

            // Then
            _board.Tiles[7, 7].Should().NotBeNull();
            _board.Tiles[playerPosition.PositionY, playerPosition.PositionX].IsHit.Should().BeTrue();
        }

        [TestCase(10)]
        [TestCase(20)]
        public void TestPopulatingOfMines(int mines)
        {
            // Given
            var playerPosition = new Position(0, 3);
            _board.Create(8, 8, playerPosition);
            int testCount = 0;

            // When
            _board.PopulateMines(mines, playerPosition);

            for (int y = 0; y < _board.Tiles.GetLength(0); y++)
            {
                for (int x = 0; x < _board.Tiles.GetLength(1); x++)
                {
                    if(_board.Tiles[y, x].IsMine)
                    {
                        testCount++;
                    }
                }
            }

            // Then
            testCount.Should().Be(mines);
            _board.GetTile(playerPosition.PositionX, playerPosition.PositionY).IsMine.Should().BeFalse();
        }

        [Test]
        public void TestPositiveCollisionWithMineForPlayerPosition()
        {
            // Given
            var playerPosition = new Position(1, 3);
            _board.Create(8, 8, playerPosition);
            _board.Tiles[playerPosition.PositionY, playerPosition.PositionX].IsMine = true;

            // When
            bool result = _board.CheckCollision(playerPosition);

            // Then
            result.Should().BeTrue();
        }

        [Test]
        public void TestNegativeCollisionWithMineForPlayerPosition()
        {
            // Given
            var playerPosition = new Position(1, 3);
            _board.Create(8, 8, playerPosition);
            _board.Tiles[playerPosition.PositionY, playerPosition.PositionX].IsMine = false;

            // When
            bool result = _board.CheckCollision(playerPosition);

            // Then
            result.Should().BeFalse();
        }

        [TearDown]
        public void Cleanup()
        {
            _board = null;
        }
    }
}