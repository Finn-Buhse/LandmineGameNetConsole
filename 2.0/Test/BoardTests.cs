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
        

        // DUMMY MINE COORDINATES:
        // (0, 0)
        // (7, 7)
        // (2, 5)
        // (6, 4) 
        const string PLACED_MINES_DISPLAY_STRING_DUMMY = "7 |~ ~ ~ ~ ~ ~ ~ X |\n6 |~ ~ ~ ~ ~ ~ ~ ~ |\n5 |~ ~ X ~ ~ ~ ~ ~ |\n4 |~ ~ ~ ~ ~ ~ X ~ |\n3 |~ ~ ~ ~ ~ ~ ~ ~ |\n2 |~ ~ ~ ~ ~ ~ ~ ~ |\n1 |~ ~ ~ ~ ~ ~ ~ ~ |\n0 |X ~ ~ ~ ~ ~ ~ ~ |\nY\n X 0 1 2 3 4 5 6 7";
        const string PLACED_MINES_JSON_DUMMY = "[[0,0,0,0,0,0,0,1],[0,0,0,0,0,0,0,0],[0,0,1,0,0,0,0,0],[0,0,0,0,0,0,1,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[1,0,0,0,0,0,0,0]]";

        IHost? host;
        IBoard? board;

        // 'Unkown' value will trigger an assertion if the tests fail to properly set this variable
        BoardSquare squareAtResult = BoardSquare.Unkown;
        // Empty values will trigger an assertion if the tests failt to properly set the below strings
        string getJsonResult = "";
        string getDisplayStringResult = "";

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

        public void givenBoardSquareAtXEquals0YEquals7Is(BoardSquare boardSquare)
        {
            board!.setSquare(0, 7, boardSquare);
        }

        public void givenDummyMinesPlaced()
        {
            board!.setSquare(0, 0, BoardSquare.Mine);
            board!.setSquare(7, 7, BoardSquare.Mine);
            board!.setSquare(2, 5, BoardSquare.Mine);
            board!.setSquare(6, 4, BoardSquare.Mine);
        }

        // WHENS
        public void whenSquareAtXEquals0YEquals7Called()
        {
            squareAtResult = board!.squareAt(0, 7);
        }

        public void whenGetJsonCalled()
        {
            getJsonResult = board!.getJson();
        }

        public void whenGetDisplayStringCalled()
        {
            getDisplayStringResult = board!.getDisplayString();
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

        public void thenBoardSquareAtResultIs(BoardSquare boardSquare)
        {
            Assert.AreEqual(boardSquare, squareAtResult);
        }

        public void thenJsonResultIsCorrectForEmptyBoard()
        {
            Assert.AreEqual(EMPTY_JSON_DUMMY, getJsonResult);
        }

        public void thenDisplayStringResultIsCorrectForEmptyBoard()
        {
            Assert.AreEqual(EMPTY_DISPLAY_STRING_DUMMY, getDisplayStringResult);
        }

        public void thenJsonResultIsCorrectForBoardWithMines()
        {
            Assert.AreEqual(PLACED_MINES_JSON_DUMMY, getJsonResult);
        }

        public void thenDisplayStringIsCorrectForBoardWithMines()
        {
            Assert.AreEqual(PLACED_MINES_DISPLAY_STRING_DUMMY, getDisplayStringResult);
        }

        [TestMethod]
        public void CreatedBoardSquaresAreEmpty()
        {
            givenHostContainerAndBoardCreated();

            thenAllBoardSquaresAreEmpty();
        }

        [DataTestMethod]
        [DataRow(BoardSquare.Empty)]
        [DataRow(BoardSquare.Mine)]
        [DataRow(BoardSquare.Life)]
        public void SquareAtReturnsCorrect(BoardSquare boardSquare)
        {
            givenHostContainerAndBoardCreated();

            givenBoardSquareAtXEquals0YEquals7Is(boardSquare);

            whenSquareAtXEquals0YEquals7Called();

            thenBoardSquareAtResultIs(boardSquare);
        }

        [TestMethod]
        public void GetJsonReturnsCorrect()
        {
            givenHostContainerAndBoardCreated();

            // CASE 1
            whenGetJsonCalled();
            
            thenJsonResultIsCorrectForEmptyBoard();

            // CASE 2
            givenDummyMinesPlaced();
            
            whenGetJsonCalled();

            thenJsonResultIsCorrectForBoardWithMines();
        }

        [TestMethod]
        public void GetDisplayStringReturnsCorrect()
        {
            givenHostContainerAndBoardCreated();

            // CASE 1
            whenGetDisplayStringCalled();

            thenDisplayStringResultIsCorrectForEmptyBoard();

            // CASE 2
            givenDummyMinesPlaced();

            whenGetDisplayStringCalled();

            thenDisplayStringIsCorrectForBoardWithMines();
        }
    }
}