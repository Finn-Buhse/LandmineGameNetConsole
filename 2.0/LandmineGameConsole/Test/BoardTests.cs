using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using ApplicationClasses;
using LandmineGameClasses;

namespace Testing
{
    [TestClass]
    public class BoardTests
    {
        const string EMPTY_DISPLAY_STRING_DUMMY = "7 |~ ~ ~ ~ ~ ~ ~ ~ |\n6 |~ ~ ~ ~ ~ ~ ~ ~ |\n5 |~ ~ ~ ~ ~ ~ ~ ~ |\n4 |~ ~ ~ ~ ~ ~ ~ ~ |\n3 |~ ~ ~ ~ ~ ~ ~ ~ |\n2 |~ ~ ~ ~ ~ ~ ~ ~ |\n1 |~ ~ ~ ~ ~ ~ ~ ~ |\n0 |~ ~ ~ ~ ~ ~ ~ ~ |\nY\n X 0 1 2 3 4 5 6 7";
        const string EMPTY_JSON_DUMMY = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]";
        

        // Mine coordinates are:
        // (0, 0)
        // (7, 7)
        // (2, 5)
        // (6, 4) 
        const string PLACED_MINES_DISPLAY_STRING_DUMMY = "7 |~ ~ ~ ~ ~ ~ ~ X |\n6 |~ ~ ~ ~ ~ ~ ~ ~ |\n5 |~ ~ X ~ ~ ~ ~ ~ |\n4 |~ ~ ~ ~ ~ ~ X ~ |\n3 |~ ~ ~ ~ ~ ~ ~ ~ |\n2 |~ ~ ~ ~ ~ ~ ~ ~ |\n1 |~ ~ ~ ~ ~ ~ ~ ~ |\n0 |X ~ ~ ~ ~ ~ ~ ~ |\nY\n X 0 1 2 3 4 5 6 7";
        const string PLACED_MINES_JSON_DUMMY = "[[0,0,0,0,0,0,0,1],[0,0,0,0,0,0,0,0],[0,0,1,0,0,0,0,0],[0,0,0,0,0,0,1,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[1,0,0,0,0,0,0,0]]";

        IHost? host;
        IBoard? board;

        // GIVENS
        public void givenHostContainerCreated()
        {
            host = HostContainer.CreateHostContainer();
        }

        public void givenBoardCreatedWithWidthAndHeightEqualTo8()
        {
            board = host!.Services.GetRequiredService<IBoard>();
            board!.initialise(8, 8);
        }

        public void givenHostContainerAndBoardCreated()
        {
            givenHostContainerCreated();
            givenBoardCreatedWithWidthAndHeightEqualTo8();
        }

        public void givenMinePlacedAtXEquals0YEquals7()
        {
            board!.setSquare(0, 7, BoardSquare.Mine);
        }

        public void givenLifePlacedAtXEquals0YEquals7()
        {
            board!.setSquare(0, 7, BoardSquare.Life);
        }

        public void givenSomeMinesPlaced()
        {
            board!.setSquare(0, 0, BoardSquare.Mine);
            board!.setSquare(7, 7, BoardSquare.Mine);
            board!.setSquare(2, 5, BoardSquare.Mine);
            board!.setSquare(6, 4, BoardSquare.Mine);
        }

        // THENS

        public void thenAllBoardSquaresAreEmpty()
        {
            bool notEmptyFound = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    notEmptyFound |= board!.squareAt(i, j) != BoardSquare.Empty;
                }
            }
            Assert.IsFalse(notEmptyFound);
        }

        public void thenBoardSquareAtXEquals0YEquals7IsEmpty()
        {
            Assert.AreEqual(BoardSquare.Empty, board!.squareAt(0, 7));
        }

        public void thenBoardSqaureAtXEquals0YEquals7IsMine()
        {
            Assert.AreEqual(BoardSquare.Mine, board!.squareAt(0, 7));
        }

        public void thenBoardSqaureAtXEquals0YEquals7IsLife()
        {
            Assert.AreEqual(BoardSquare.Life, board!.squareAt(0, 7));
        }

        public void thenJsonIsCorrectForEmptyBoard()
        {
            Assert.AreEqual(EMPTY_JSON_DUMMY, board!.getJson());
        }

        public void thenDisplayStringIsCorrectForEmptyBoard()
        {
            Assert.AreEqual(EMPTY_DISPLAY_STRING_DUMMY, board!.getDisplayString());
        }

        public void thenJsonIsCorrectForBoardWithMines()
        {
            Assert.AreEqual(PLACED_MINES_JSON_DUMMY, board!.getJson());
        }

        public void thenDisplayStringIsCorrectForBoardWithMines()
        {
            Assert.AreEqual(PLACED_MINES_DISPLAY_STRING_DUMMY, board!.getDisplayString());
        }

        [TestMethod]
        public void CreatedBoardSquaresAreEmpty()
        {
            givenHostContainerAndBoardCreated();

            thenAllBoardSquaresAreEmpty();
        }

        [TestMethod]
        public void SquareAtReturnsCorrect()
        {
            givenHostContainerAndBoardCreated();

            thenBoardSquareAtXEquals0YEquals7IsEmpty();


            givenMinePlacedAtXEquals0YEquals7();

            thenBoardSqaureAtXEquals0YEquals7IsMine();


            givenLifePlacedAtXEquals0YEquals7();

            thenBoardSqaureAtXEquals0YEquals7IsLife();
        }

        [TestMethod]
        public void GetJsonReturnsCorrect()
        {
            givenHostContainerAndBoardCreated();
            
            thenJsonIsCorrectForEmptyBoard();

            givenSomeMinesPlaced();

            thenJsonIsCorrectForBoardWithMines();
        }

        [TestMethod]
        public void GetDisplayStringReturnsCorrect()
        {
            givenHostContainerAndBoardCreated();

            thenDisplayStringIsCorrectForEmptyBoard();

            givenSomeMinesPlaced();

            thenDisplayStringIsCorrectForBoardWithMines();
        }
    }
}