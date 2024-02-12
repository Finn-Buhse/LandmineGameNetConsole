using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LandmineGameClasses
{
    public enum PlayerMoveDirection
    {
        Up, Down, Left, Right
    }

    public interface IPlayer
    {
        public void initialise(int startingX, int startingY, int startingLives, int boardWidth, int boardHeight);

        public int getX();

        public int getY();

        public int getLives();

        public bool isDead();

        public void move(PlayerMoveDirection movementDirection);

        public void deductLife();

        public void addLife();
    }

    public class Player : IPlayer
    {
        private int boardWidth;
        private int boardHeight;

        private int x;

        private int y;

        private int lives;

        public Player()
        {
            boardWidth = 0;
            boardHeight = 0;
            x = 0;
            y = 0;
            lives = 0;
        }

        public void initialise(int startingX, int startingY, int startingLives, int boardWidth, int boardHeight)
        {
            this.boardWidth = boardWidth;
            this.boardHeight = boardHeight;
            x = startingX;
            y = startingY;
            lives = startingLives;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public int getLives()
        {
            return lives;
        }

        public bool isDead()
        {
            return lives <= 0;
        }

        public void move(PlayerMoveDirection movementDirection)
        {
            switch (movementDirection)
            {
                case PlayerMoveDirection.Up:
                {
                    if(y < boardHeight - 1)
                        y += 1;
                    break;
                }
                case PlayerMoveDirection.Down:
                {
                    if(y > 0)
                        y -= 1;
                    break;
                }
                case PlayerMoveDirection.Left:
                {
                    if(x > 0)
                        x -= 1;
                    break;
                }
                case PlayerMoveDirection.Right:
                {
                    if(x < boardWidth - 1)
                        x += 1;
                    break;
                }
            }
        }

        public void deductLife()
        {
            lives -= 1;
        }

        public void addLife()
        {
            lives += 1;
        }
    }
}
