using ConsoleSweeper.Model;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleSweeper.UnitTests.Model
{
    [TestFixture()]
    public class TileTests
    {
        [Test()]
        public void TestStringReturnOfTileThatHasNotBeenHit()
        {
            // Given
            var tile = new Tile()
            {
                IsHit = false,
                IsMine = false,
            };

            // When
            string result = tile.ToString();

            // Then
            result.Should().Be("O");
        }

        [Test()]
        public void TestStringReturnOfTileThatHasBeenHitAndIsMine()
        {
            // Given
            var tile = new Tile()
            {
                IsHit = true,
                IsMine = true,
            };

            // When
            string result = tile.ToString();

            // Then
            result.Should().Be("B");
        }

        [Test()]
        public void TestStringReturnOfTileThatHasBeenHitAndIsNotMine()
        {
            // Given
            var tile = new Tile()
            {
                IsHit = true,
                IsMine = false,
            };

            // When
            string result = tile.ToString();

            // Then
            result.Should().Be("X");
        }
    }
}