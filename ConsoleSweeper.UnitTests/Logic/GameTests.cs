using System;
using System.Threading.Tasks;
using ConsoleSweeper.Logic;
using ConsoleSweeper.Model;
using ConsoleSweeper.Wrapper;
using Moq;
using NUnit.Framework;

namespace ConsoleSweeper.UnitTests.Logic
{
    [TestFixture()]
    public class GameTests
    {
        private Mock<IConsoleWrapper> _consoleMock;
        private Mock<IBoard> _boardMock;
        private Mock<IPlayer> _playerMock;
        private Game _game;

        [SetUp]
        public void Setup()
        {
            _consoleMock = new Mock<IConsoleWrapper>();
            _boardMock = new Mock<IBoard>();
            _playerMock = new Mock<IPlayer>();
            _game = new Game(_consoleMock.Object, _boardMock.Object, _playerMock.Object);
        }

        [Test()]
        public void TestInitialisationOfGame()
        {
            // Given
            _boardMock.Setup((board) => board.Create(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Position>())).Verifiable();
            _boardMock.Setup((board) => board.PopulateMines(It.IsAny<int>(), It.IsAny<Position>())).Verifiable();

            // When
            var game = new Game(_consoleMock.Object, _boardMock.Object, _playerMock.Object);

            // Then
            _boardMock.Verify();
        }

        [Test()]
        public void TestRunCycleOfGame()
        {
            // Given
            _consoleMock.Setup((console) => console.ReadKey()).Returns(new ConsoleKeyInfo('W', ConsoleKey.W, false, false, false)).Verifiable();
            _boardMock.Setup((board) => board.Draw()).Verifiable();
            _boardMock.Setup((board) => board.CheckCollision(It.IsAny<Position>())).Returns(true).Verifiable();
            _playerMock.Setup((player) => player.IsValidMove(Enums.InputEnum.UP)).Returns(true).Verifiable();
            _playerMock.Setup((player) => player.Move(Enums.InputEnum.UP)).Verifiable();
            _playerMock.Setup((player) => player.MineHit()).Verifiable();
            _playerMock.SetupGet<bool>(p => p.IsAlive).Returns(true);
            _playerMock.SetupGet<Position>(p => p.CurrentPosition).Returns(new Position(0, 3));

            Task.Run(async() =>
            {
                await Task.Delay(TimeSpan.FromSeconds(3));

                _playerMock.SetupGet<bool>(p => p.IsAlive).Returns(false);
            });

            // When
            _game.Run();

            // Then
            _consoleMock.Verify();
            _boardMock.Verify();
            _playerMock.Verify();
        }

        [TestCase('W', Result = Enums.InputEnum.UP)]
        [TestCase('A', Result = Enums.InputEnum.LEFT)]
        [TestCase('S', Result = Enums.InputEnum.DOWN)]
        [TestCase('D', Result = Enums.InputEnum.RIGHT)]
        [TestCase('Y', Result = Enums.InputEnum.YES)]
        [TestCase('Z', Result = Enums.InputEnum.UNKNOWN)]
        public Enums.InputEnum TestIfNextBoardMoveForPlayerGivenADirection(char input)
        {
            // Then
            return _game.ProcessInput(input);
        }

        [TearDown]
        public void Cleanup()
        {
            _consoleMock = null;
            _boardMock = null;
            _playerMock = null;
            _game = null;
        }
    }
}