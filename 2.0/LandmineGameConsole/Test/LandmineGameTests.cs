using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using ApplicationClasses;
using LandmineGameClasses;

namespace Testing
{
    [TestClass]
    public class LandmineGameTests
    {
        IHost? host = null;
        ILandmineGame? landmineGame = null;

        // GIVENS
        public void givenHostContainerCreated()
        {
            host = HostContainer.CreateHostContainer();
        }

        void givenGameCreated()
        {
            landmineGame = host!.Services.GetRequiredService<ILandmineGame>();
            landmineGame.setBoardWidthHeightInitHints(8, 8);

            // Initialise game but don't randomise the board squares
            landmineGame.initialise(false); 
        }

        void givenHostContainerAndGameCreated()
        {
            givenHostContainerCreated();
            givenGameCreated();
        }

        void givenSomeMinesArePlaced()
        {
            landmineGame!.board.setSquare(0, 0, BoardSquare.Mine);
            landmineGame!.board.setSquare(0, 6, BoardSquare.Mine);
        }

        void givenThePlayerMovesToBelowTheTopRow()
        {
            // Mocks the sequence of actions that would occur the in game loop:
            // - Game is updated
            // - The player is moved in a direction given by user
            // - The cycle repeats

            landmineGame!.update();
            Assert.AreEqual(PlayerStatus.Alive, landmineGame!.getFrame().playerStatus);
            landmineGame!.player.move(PlayerMoveDirection.Up);
            landmineGame!.update();
            Assert.AreEqual(PlayerStatus.Alive, landmineGame!.getFrame().playerStatus);
            landmineGame!.player.move(PlayerMoveDirection.Up);
            landmineGame!.update();
            Assert.AreEqual(PlayerStatus.Alive, landmineGame!.getFrame().playerStatus);
            landmineGame!.player.move(PlayerMoveDirection.Up);
            landmineGame!.update();
            Assert.AreEqual(PlayerStatus.Alive, landmineGame!.getFrame().playerStatus);
            landmineGame!.player.move(PlayerMoveDirection.Up);
            landmineGame!.update();
            Assert.AreEqual(PlayerStatus.Alive, landmineGame!.getFrame().playerStatus);
            landmineGame!.player.move(PlayerMoveDirection.Up);
            landmineGame!.update();
            Assert.AreEqual(PlayerStatus.Alive, landmineGame!.getFrame().playerStatus);
            landmineGame!.player.move(PlayerMoveDirection.Up);
            landmineGame!.update();
            Assert.AreEqual(PlayerStatus.Alive, landmineGame!.getFrame().playerStatus);
        }

        void givenAMineIsPlacedAtXEquals0YEquals7()
        {
            landmineGame!.board.setSquare(0, 7, BoardSquare.Mine);
        }

        void givenAMineIsPlacedAtXAndYEquals0()
        {
            landmineGame!.board.setSquare(0, 0, BoardSquare.Mine);
        }

        void givenALifeIsPlacedOnTheBoardAtXAndYEquals0()
        {
            landmineGame!.board.setSquare(0, 0, BoardSquare.Life);
        }

        void givenThePlayerMovesUp()
        {
            landmineGame!.player.move(PlayerMoveDirection.Up);
        }

        // WHENS
        void whenUpdateIsCalled()
        {
            landmineGame!.update();
        }

        // THENS

        void thenPlayerStatusIsWon()
        {
            Assert.AreEqual(PlayerStatus.Won, landmineGame!.getFrame().playerStatus);
        }

        void thenPlayerStatusIsLost()
        {
            Assert.AreEqual(PlayerStatus.Lost, landmineGame!.getFrame().playerStatus);
        }

        void thenTheBoardSquareIsEmpty()
        {
            Assert.AreEqual(BoardSquare.Empty, landmineGame!.board!.squareAt(0, 0));
        }

        void thenLivesIsEqualTo2()
        {
            Assert.AreEqual(2, landmineGame!.player!.getLives());
        }

        void thenLivesIsEqualTo3()
        {
            Assert.AreEqual(3, landmineGame!.player!.getLives());
        }

        void thenLivesIsEqualTo4()
        {
            Assert.AreEqual(4, landmineGame!.player!.getLives());
        }

        [TestMethod]
        public void PlayerStatusCorrectlyIndicatesWon()
        {
            givenHostContainerAndGameCreated();

            givenSomeMinesArePlaced();
            givenThePlayerMovesToBelowTheTopRow();
            givenThePlayerMovesUp();

            whenUpdateIsCalled();

            thenPlayerStatusIsWon();
        }

        [TestMethod]
        public void PlayerStatusCorrectlyIndicatesLost()
        {
            givenHostContainerAndGameCreated();

            givenSomeMinesArePlaced();
            givenThePlayerMovesToBelowTheTopRow();
            givenAMineIsPlacedAtXEquals0YEquals7();
            givenThePlayerMovesUp();

            whenUpdateIsCalled();

            thenPlayerStatusIsLost();
        }

        [TestMethod]
        public void UpdateGameSetsBoardSquareToEmpty()
        {
            givenHostContainerAndGameCreated();

            givenAMineIsPlacedAtXAndYEquals0();

            whenUpdateIsCalled();

            thenTheBoardSquareIsEmpty();
        }

        [TestMethod]
        public void WhenPlayerIsOnMineUpdateDecrementsLives()
        {
            givenHostContainerAndGameCreated();

            thenLivesIsEqualTo3();

            givenAMineIsPlacedAtXAndYEquals0();

            whenUpdateIsCalled();

            thenLivesIsEqualTo2();
        }

        [TestMethod]
        public void WhenPlayerIsOnLifeUpdateIncrementsLives()
        {
            givenHostContainerAndGameCreated();

            thenLivesIsEqualTo3();

            givenALifeIsPlacedOnTheBoardAtXAndYEquals0();

            whenUpdateIsCalled();

            thenLivesIsEqualTo4();
        }
    }
}
