using System.Diagnostics;

namespace LandmineGameClasses
{
    public enum BoardSquare
    {
        Empty,
        Mine,
        Life,
        NBoardSquareTypes,
        Unkown
    }

    public interface IBoard
    {
        public void initialise(int width, int height);

        public int getWidth();

        public int getHeight();

        public void setSquare(int x, int y, BoardSquare square);

        public BoardSquare squareAt(int x, int y);

        public string getJson();

        public string getDisplayString();
    }

    public class Board : IBoard
    {
        // Board array accessed using 'y' to get rows going from top to bottom,
        // then each element in the row is accessed from left to right using 'x'

        // In order to make coords (0, 0) the bottom left square, the 'y' value should be inverted
        // boardArray[boardArray.Count() - 1 - y][x]

        private static readonly string[] BOARD_SQUARE_STRINGS = { "~ ", "X ", "+ " };
        private static readonly string BORDER_STRING = "|";

        private int width = 0;
        private int height = 0;
        internal BoardSquare[][]? squareArray;

        public Board()
        {

        }

        public void initialise(int width, int height) {

            Debug.Assert(width > 0 && width <= 10 && height > 0 && height <= 10, "[ERROR] Max board width and height is 10, minimum is 1"); // Max board width and height of 10, otherwise displaying the board becomes more complex

            this.width = width;
            this.height = height;

            squareArray = new BoardSquare[height][];
            for (int i = 0; i < height; i++)
            {
                squareArray[i] = new BoardSquare[width];
                for (int j = 0; j < width; j++)
                {
                    squareArray[i][j] = BoardSquare.Empty;
                }
            }
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        public void setSquare(int x, int y, BoardSquare boardSquare)
        {
            if(squareArray != null)
                squareArray[squareArray.Count() - y - 1][x] = boardSquare;
        }

        public BoardSquare squareAt(int x, int y)
        {
            if (squareArray != null)
                return squareArray[squareArray.Count() - y - 1][x];

            Debug.Fail("Attempted to get square from board when it has not been initialised");
            return BoardSquare.Unkown;
        }

        public string getJson()
        {
            if(squareArray != null)
            {
                // Generate JSON array containing board square values
                string json = "[";
                for (var i = 0; i < height; i++)
                {
                    string row = "[";
                    for (var j = 0; j < width; j++)
                    {
                        row += ((int)squareArray[i][j]).ToString();
                        if (j != width - 1)
                        {
                            row += ",";
                        }
                    }
                    row += "]";
                    if (i != height - 1)
                    {
                        row += ",";
                    }
                    json += row;
                }
                json += "]";
                return json;
            }
            Debug.Fail("Attempted to invoke getJSON on board which has not been initialised");
            return "";
        }

        public string getDisplayString()
        {
            if (squareArray != null)
            {
                string displayString = "";
                for (int i = 0; i < height; i++)
                {
                    // Y axis and left border
                    string rowString = (height - i - 1).ToString() + " " + BORDER_STRING;

                    for (int j = 0; j < width; j++)
                    {
                        // Convert enum to integer giving 0 for an empty square, 1 for a mine, and 2 for a life
                        rowString += BOARD_SQUARE_STRINGS[(int)squareArray[i][j]];
                    }
                    // Right border
                    displayString += rowString + BORDER_STRING + "\n";
                }

                // X axis
                displayString += "Y\n X ";
                for (int i = 0; i < width; i++)
                {
                    displayString += i.ToString();
                    if (i != width - 1)
                        displayString += " ";
                }

                return displayString;
            }
            Debug.Fail("Attempted to invoke getDisplayString on board which has not been initialised");
            return "";
        }
    }
}
