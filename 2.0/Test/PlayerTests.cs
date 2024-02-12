using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ApplicationClasses;
using LandmineGameClasses;

namespace Testing
{
    [TestClass]
    public class PlayerTests
    {
        IHost? host = null;
        IPlayer? player = null;

        // GIVENS
        public void givenHostContainerCreated()
        {
            host = HostContainer.CreateHostContainer();
        }

        void givenPlayerCreatedWithXAndYEquals0()
        {
            player = host!.Services.GetRequiredService<IPlayer>();
            player.initialise(0, 0, 3, 8, 8);
        }

        void givenPlayerCreatedWithXAndYEquals7()
        {
            player = host!.Services.GetRequiredService<IPlayer>();
            player.initialise(7, 7, 3, 8, 8);
        }

        // WHENS
        void whenThePlayerMovesUp()
        {
            player!.move(PlayerMoveDirection.Up);
        }
        void whenThePlayerMovesDown()
        {
            player!.move(PlayerMoveDirection.Down);
        }
        void whenThePlayerMovesLeft()
        {
            player!.move(PlayerMoveDirection.Left);
        }
        void whenThePlayerMovesRight()
        {
            player!.move(PlayerMoveDirection.Right);
        }

        // THENS
        void thenPlayerXAndYEquals0()
        {
            Assert.AreEqual(0, player!.getX());
            Assert.AreEqual(0, player!.getY());
        }

        void thenPlayerXAndYEquals7()
        {
            Assert.AreEqual(7, player!.getX());
            Assert.AreEqual(7, player!.getY());
        }

        void thenPlayerXEquals0AndYEquals1()
        {
            Assert.AreEqual(0, player!.getX());
            Assert.AreEqual(1, player!.getY());
        }
        void thenPlayerXEquals7AndYEquals6()
        {
            Assert.AreEqual(7, player!.getX());
            Assert.AreEqual(6, player!.getY());
        }

        void thenPlayerXEquals6AndYEquals7()
        {
            Assert.AreEqual(6, player!.getX());
            Assert.AreEqual(7, player!.getY());
        }

        void thenPlayerXEquals1AndYEquals0()
        {
            Assert.AreEqual(1, player!.getX());
            Assert.AreEqual(0, player!.getY());
        }

        [TestMethod]
        public void PlayerMovesUpToCorrectCoordinates()
        {
            givenHostContainerCreated();
            givenPlayerCreatedWithXAndYEquals0();

            whenThePlayerMovesUp();

            thenPlayerXEquals0AndYEquals1();
        }

        [TestMethod]
        public void PlayerMovesDownToCorrectCoordinates()
        {
            givenHostContainerCreated();
            givenPlayerCreatedWithXAndYEquals7();

            whenThePlayerMovesDown();

            thenPlayerXEquals7AndYEquals6();
        }

        [TestMethod]
        public void PlayerMovesLeftToCorrectCoordinates()
        {
            givenHostContainerCreated();
            givenPlayerCreatedWithXAndYEquals7();

            whenThePlayerMovesLeft();

            thenPlayerXEquals6AndYEquals7();
        }

        [TestMethod]
        public void PlayerMovesRightToCorrectCoordinates()
        {
            givenHostContainerCreated();
            givenPlayerCreatedWithXAndYEquals0();

            whenThePlayerMovesRight();

            thenPlayerXEquals1AndYEquals0();
        }

        [TestMethod]
        public void PlayerDoesNotMoveUpOutOfBoard()
        {
            givenHostContainerCreated();
            givenPlayerCreatedWithXAndYEquals7();

            whenThePlayerMovesUp();

            thenPlayerXAndYEquals7();
        }

        [TestMethod]
        public void PlayerDoesNotMoveDownOutOfBoard()
        {
            givenHostContainerCreated();
            givenPlayerCreatedWithXAndYEquals0();

            whenThePlayerMovesDown();

            thenPlayerXAndYEquals0();
        }

        [TestMethod]
        public void PlayerDoesNotMoveLeftOutOfBoard()
        {
            givenHostContainerCreated();
            givenPlayerCreatedWithXAndYEquals0();

            whenThePlayerMovesLeft();

            thenPlayerXAndYEquals0();
        }

        [TestMethod]
        public void PlayerDoesNotMoveRightOutOfBoard()
        {
            givenHostContainerCreated();
            givenPlayerCreatedWithXAndYEquals7();

            whenThePlayerMovesRight();

            thenPlayerXAndYEquals7();
        }
    }
}
