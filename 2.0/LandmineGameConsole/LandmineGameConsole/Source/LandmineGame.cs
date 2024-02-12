using RandomClasses;

namespace LandmineGameClasses
{
    public enum PlayerStatus
    {
        Alive,
        Lost,
        Won
    }

    public struct GameInfo
    {
        public BoardSquare playerVisitedSquare;
        public PlayerStatus playerStatus;

        public GameInfo(BoardSquare playerVisitedSquare, PlayerStatus playerStatus)
        {
            this.playerVisitedSquare = playerVisitedSquare;
            this.playerStatus = playerStatus;
        }

        public bool gameShouldEnd()
        {
            // If not Alive, the player will either have Won or Lost, in which case the game should end
            return playerStatus != PlayerStatus.Alive;
        }
    }

    public interface ILandmineGame
    {
        public IBoard board { get; set; }
        public IPlayer player { get; set; }

        public void setBoardWidthHeightInitHints(int boardWidth, int boardHeight);

        public void initialise(bool randomiseBoardSquares = true);

        public void update();

        public GameInfo getFrame();

        public string getFrameJson();

        public string getFrameDisplayString();

        public bool shouldEnd();
    }

    public class LandmineGame : ILandmineGame
    {
        private int boardWidth = 8;
        private int boardHeight = 8;

        public IBoard board { get; set; }

        public IPlayer player { get; set; }

        private IRandomGenerator randomGenerator;

        private GameInfo frameGameInfo = new GameInfo(BoardSquare.Empty, PlayerStatus.Alive);

        public LandmineGame(IBoard board, IPlayer player, IRandomGenerator randomGenerator)
        {
            this.board = board;
            this.player = player;
            this.randomGenerator = randomGenerator;
        }

        public void setBoardWidthHeightInitHints(int boardWidth, int boardHeight)
        {
            this.boardWidth = boardWidth;
            this.boardHeight = boardHeight;
        }

        public void initialise(bool randomiseBoardSquares)
        {
            // Create board
            board.initialise(boardWidth, boardHeight);

            // Create player
            player.initialise(0, 0, 3, boardWidth, boardHeight);

            if(randomiseBoardSquares)
                placeRandomMinesAndLives();
        }

        public void update()
        {
            frameGameInfo = processFrame();
        }

        public GameInfo getFrame()
        {
            return frameGameInfo;
        }

        public string getFrameJson()
        {
            return generateFrameJSON(frameGameInfo);
        }

        public string getFrameDisplayString()
        {
            return generateFrameDisplayString(frameGameInfo);
        }

        public bool shouldEnd()
        {
            return frameGameInfo.gameShouldEnd();
        }

        public GameInfo processFrame()
        {
            GameInfo gameInfo = new GameInfo();

            // Obtain the square the player is currently on
            gameInfo.playerVisitedSquare = board.squareAt(player.getX(), player.getY());

            // Process the player's lives, and whether they have won or lost
            gameInfo.playerStatus = processPlayerStatus(gameInfo.playerVisitedSquare);

            // Clear the square the player has visited
            if (gameInfo.playerVisitedSquare != BoardSquare.Empty)
                board.setSquare(player.getX(), player.getY(), BoardSquare.Empty);

            return gameInfo;
        }

        // If the player has no lives left and hits a mine on the top row they could be considered to have won or lost,
        // for this game the player loses in this scenario
        public PlayerStatus processPlayerStatus(BoardSquare playerVisitedSquare)
        {
            // If player hits a mine deduct a life
            if (playerVisitedSquare == BoardSquare.Mine)
            {
                player.deductLife();

            } // If player hits a life add one to the player's lives
            else if (playerVisitedSquare == BoardSquare.Life)
            {
                player.addLife();
            }

            // If player lives equals zero they have lost
            if (player.isDead())
            {
                return PlayerStatus.Lost;

            } // If player has reached the top row of the board they have won
            else if (player.getY() == board.getHeight() - 1)
            {
                return PlayerStatus.Won;
            }

            return PlayerStatus.Alive;
        }

        private string generateFrameJSON(GameInfo gameInfo)
        {
            string json = "{";

            json += "\"board\":" + board.getJson() + ",";
            json += "\"playerX\":" + player.getX().ToString() + ",";
            json += "\"playerY\":" + player.getY().ToString() + ",";
            json += "\"playerStatus\":" + ((int)gameInfo.playerStatus).ToString() + ",";
            json += "\"playerVisitedSquare\":" + ((int)gameInfo.playerVisitedSquare).ToString();

            json += "}";
            return json;
        }

        private string generateFrameDisplayString(GameInfo gameInfo)
        {
            string result = "";
            if (gameInfo.playerStatus == PlayerStatus.Won)
            {
                result += "You won!\n";
            }
            else if (gameInfo.playerStatus == PlayerStatus.Lost)
            {
                result += "You lost all your lives! Game over!\n";
            }
            else
            {
                result +=
                    "Board:\n" + board.getDisplayString() +
                    "\nPlayer coordinates: (" + player.getX().ToString() + ", " + player.getY().ToString() + ")\n";

                if (gameInfo.playerVisitedSquare == BoardSquare.Mine)
                    result += "You hit a mine!";
                else if (gameInfo.playerVisitedSquare == BoardSquare.Life)
                    result += "You gained a life!";
                else
                    result += "You did not hit any mines.";
            }
            return result;
        }

        private void placeRandomMinesAndLives()
        {
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    // Value can be 0, 1, or 2, giving
                    // a 1/3 probability of the square being empty
                    // a 1/3 probability of the square having a mine
                    // a 1/3 probability of the square having a life
                    int randomInt = randomGenerator.getRandom(0, 3);

                    // 0 corressponds to an empty square but nothing has to be done in this case since the board starts with empty squares
                    if (randomInt == 1)
                    {
                        board.setSquare(x, y, BoardSquare.Mine);
                    }
                    else if (randomInt == 2)
                    {
                        board.setSquare(x, y, BoardSquare.Life);
                    }
                }
            }
        }
    }
}
