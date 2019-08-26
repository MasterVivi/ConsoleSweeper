using ConsoleSweeper.Model;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleSweeper.UnitTests.Model
{
    [TestFixture()]
    public class PositionTests
    {
        [Test()]
        public void TestPositionConstructorSetsVariablesCorrect()
        {
            // When
            var position = new Position(1, 2);

            // Then
            position.PositionX.Should().Be(1);
            position.PositionY.Should().Be(2);
        }
    }
}