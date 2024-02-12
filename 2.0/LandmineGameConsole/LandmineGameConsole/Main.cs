using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ApplicationClasses;
using LandmineGameClasses;

// SETUP HOST
IHost host = HostContainer.CreateHostContainer();

// MAIN APP
ILandmineGame landmineGame = host.Services.GetRequiredService<ILandmineGame>(); // Automatically injects dependencies

int[] widthHeight = getBoardWidthAndHeightFromInput(8, 8);
landmineGame.setBoardWidthHeightInitHints(widthHeight[0], widthHeight[1]);

landmineGame.initialise();

while(true)
{
    landmineGame.update();
    Console.WriteLine(landmineGame.getFrameDisplayString());

    if (landmineGame.shouldEnd())
        break;

    movePlayerFromInput(landmineGame);
}

// CONSOLE FUNCTIONS
int[] getBoardWidthAndHeightFromInput(int defaultWidth, int defaultHeight)
{
    int[] widthHeight = new int[2];

    Console.WriteLine("Would you like to choose the game's board width and height (default values are " + defaultWidth.ToString() + " and " + defaultHeight.ToString() + ")?");

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
                widthHeight[0] = defaultWidth;
                widthHeight[1] = defaultHeight;
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

void movePlayerFromInput(ILandmineGame game)
{
    while (true)
    {
        Console.WriteLine("You may move the player Up (U), Down (D), Left (L), or Right (R)\nEnter the letter corresponding to the direction you want to move the player: ");
        string? chosenDirectionString = Console.ReadLine();
        if (chosenDirectionString != null)
        {
            if (chosenDirectionString == "U")
            {
                game.player.move(PlayerMoveDirection.Up);
                break;
            }
            else if (chosenDirectionString == "D")
            {
                game.player.move(PlayerMoveDirection.Down);
                break;
            }
            else if (chosenDirectionString == "L")
            {
                game.player.move(PlayerMoveDirection.Left);
                break;
            }
            else if (chosenDirectionString == "R")
            {
                game.player.move(PlayerMoveDirection.Right);
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