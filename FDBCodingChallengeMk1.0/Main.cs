using LandmineGameClasses;
using System;
using System.Numerics;

const int DEFAULT_BOARD_WIDTH = 8;
const int DEFAULT_BOARD_HEIGHT = 8;

// Create game
LandmineGame game = new LandmineGame();

// Optional code to set the board's width and height according to user input
int[] widthHeight = getBoardWidthAndHeightFromInput();
game.setBoardWidthHeightInitialiseHints(widthHeight[0], widthHeight[1]);

// Initialise game
game.initialise();

// Game loop
while (true)
{
    game.processFrame();
    Console.WriteLine(game.getFrameDisplayString());

    if (game.shouldEnd())
        break;

    movePlayerFromInput(game);
}

// CONSOLE FUNCTIONS
int[] getBoardWidthAndHeightFromInput()
{
    int[] widthHeight = new int[2];

    Console.WriteLine("Would you like to choose the game's board width and height (default values are 8)?");

    while (true)
    {
        Console.WriteLine("Enter 'Y' for (Y)es, or 'N' for (N)o: ");
        string? answerString = Console.ReadLine();
        if (answerString != null)
        {
            if (answerString == "Y")
            {
                widthHeight[0] = getBoardDimensionFromInput("width");
                widthHeight[1] = getBoardDimensionFromInput("height");
                break;
            }
            else if (answerString == "N")
            {
                widthHeight[0] = DEFAULT_BOARD_WIDTH;
                widthHeight[1] = DEFAULT_BOARD_HEIGHT;
                break;
            }
            else
            {
                Console.WriteLine("Input was not a given option. Please try again and enter one of the given options.");
            }
        }
        else
        {
            Console.WriteLine("No input detected. Please try again and enter one of the given options.");
        }
    }
    return widthHeight;
}

int getBoardDimensionFromInput(string dimensionName)
{
    while (true)
    {
        Console.WriteLine("Enter the board " + dimensionName + " (10 max): ");
        string? dimensionString = Console.ReadLine();
        if (dimensionString != null)
        {
            try
            {
                int dimension = int.Parse(dimensionString);
                if (dimension > 0 && dimension <= 10)
                {
                    return dimension;
                }
                else
                {
                    Console.WriteLine("Input must be greater than zero and less than or equal to 10. Please try again.");
                }
            }
            catch
            {
                Console.WriteLine("Input was not an integer. Please try again and enter an integer input.");
            }
        }
        else
        {
            Console.WriteLine("No input detected. Please try again and enter an integer input.");
        }
    }
}

void movePlayerFromInput(LandmineGame game)
{
    while (true)
    {
        Console.WriteLine("You may move the player Up (U), Down (D), Left (L), or Right (R)\nEnter the letter corresponding to the direction you want to move the player: ");
        string? chosenDirectionString = Console.ReadLine();
        if (chosenDirectionString != null)
        {
            if (chosenDirectionString == "U")
            {
                game.movePlayer(Player.MovementDirection.Up);
                break;
            }
            else if (chosenDirectionString == "D")
            {
                game.movePlayer(Player.MovementDirection.Down);
                break;
            }
            else if (chosenDirectionString == "L")
            {
                game.movePlayer(Player.MovementDirection.Left);
                break;
            }
            else if (chosenDirectionString == "R")
            {
                game.movePlayer(Player.MovementDirection.Right);
                break;
            }
            else
            {
                Console.WriteLine("Input was not one of the given options. Please retry and enter one of the given options.");
            }
        }
        else
        {
            Console.WriteLine("No input detected. Please retry and enter one of the given options.");
        }
    }
}

