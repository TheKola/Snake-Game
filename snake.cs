using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace snakeGame
{
    public class snake
    {
        //Define constants variables
        const int HEAD_INDEX = 0;
        const int BODY_INDEX = 1;
        const int EMPTY_SNAKE_BODY = 0;

        private char snakeBodySymbol;
        ConsoleKey keyPressed, keyPressedTemp;

        private int snakeXPos;
        private int snakeYPos;
        private int[] snakeHeadPosition;

        List<int[]> snakeBody = new List<int[]>(); //List to store the body of the snake

        enum directions
        {
            down,
            up,
            right,
            left
        }

        directions direction;

        //Draw the snake using its symbol
        public void DrawSnake()
        {
            for (int i = HEAD_INDEX; i < snakeBody.Count; i++) //Draw the snake as it grows
            {
                Console.SetCursorPosition(snakeBody[i][globalVar.xAxisIndex], snakeBody[i][globalVar.yAxisIndex]);
                Console.Write(snakeBodySymbol);
            }
        }

        //Move the snake within the layout
        public bool MoveSnake(int[] dimension, string mode)
        {
            Console.CursorVisible = false; // Hide the cursor

            do
            {
                //check if any key is pressed
                if (Console.KeyAvailable)
                {
                    keyPressed = Console.ReadKey(true).Key;  //true - to hide the key pressed from being displayed
                }

                //move the snake UP if snake not moving down
                if (keyPressed == ConsoleKey.UpArrow && direction != directions.down)
                {
                    snakeYPos--;
                    direction = directions.up;
                    break;
                }
                //move the snake DOWN if snake not moving up
                else if (keyPressed == ConsoleKey.DownArrow && direction != directions.up)
                {
                    snakeYPos++;
                    direction = directions.down;
                    break;
                }
                //move the snake RIGHT if snake not moving left
                else if (keyPressed == ConsoleKey.RightArrow && direction != directions.left)
                {
                    snakeXPos++;
                    direction = directions.right;
                    break;
                }
                //move the snake LEFT if snake not moving right
                else if (keyPressed == ConsoleKey.LeftArrow && direction != directions.right)
                {
                    snakeXPos--;
                    direction = directions.left;
                    break;
                }
                else if (keyPressed == ConsoleKey.Escape) //Quit the game in between
                {
                    return true;
                }
                else
                {
                    keyPressed = keyPressedTemp; //load the temporary variable when random key pressed to give a continuous effect
                }
            } while (true);

            keyPressedTemp = keyPressed; //store the key pressed in temporary variable
            snakeBody.Add(snakeHeadPosition);

            if (mode == globalVar.classicMode)
            {
                //Pass through the walls and appear from another end
                if (snakeXPos < globalVar.dimensionStart) { snakeXPos = dimension[globalVar.xAxisIndex] - globalVar.dimensionEndBuffer; }
                if (snakeXPos > dimension[globalVar.xAxisIndex] - globalVar.dimensionEndBuffer) { snakeXPos = globalVar.dimensionStart; }
                if (snakeYPos < globalVar.dimensionStart) { snakeYPos = dimension[globalVar.yAxisIndex] - globalVar.dimensionEndBuffer; }
                if (snakeYPos > dimension[globalVar.yAxisIndex] - globalVar.dimensionEndBuffer) { snakeYPos = globalVar.dimensionStart; }
                return false;
            }
            else if (mode == globalVar.arcadeMode)
            {
                //Return TRUE if the snake touches the boundary
                if (snakeXPos < globalVar.dimensionStart || snakeXPos > (dimension[globalVar.xAxisIndex] - globalVar.dimensionEndBuffer) || snakeYPos < globalVar.dimensionStart || snakeYPos > (dimension[globalVar.yAxisIndex] - globalVar.dimensionEndBuffer))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        //Delete the previous body of the snake after it moves
        public void DeleteOldBodyPath()
        {
            for (int i = HEAD_INDEX; i < snakeBody.Count; i++)
            {
                Console.SetCursorPosition(snakeBody[i][globalVar.xAxisIndex], snakeBody[i][globalVar.yAxisIndex]);
                Console.Write(' ');
            }
            snakeBody.RemoveAt(HEAD_INDEX); //Remove the first element of the list, to give illusion snake is moving
        }

        //Get snake's first position (Head position)
        public int[] GetSnakeHeadPosition()
        {
            snakeHeadPosition = new int[] { snakeXPos, snakeYPos };
            return snakeHeadPosition;
        }

        //Increase the snake size
        public void IncreaseSnakeSize(int[] positionValues)
        {
            snakeBody.Add(positionValues);
        }

        public List<int[]> GetSnakeBody()
        {
            return snakeBody;
        }

        //Check if snake touched itself
        public bool GetSnakeTouchItself(int[] headPosition)
        {
            for (int i = BODY_INDEX; i < snakeBody.Count; i++) //starts from index 1 as in index 0 head positon is stored
            {
                if (headPosition[globalVar.xAxisIndex] == snakeBody[i][globalVar.xAxisIndex] && headPosition[globalVar.yAxisIndex] == snakeBody[i][globalVar.yAxisIndex])
                {
                    return true;
                }
            }
            return false;
        }

        //Set snake's symbol from config file
        public void SetSnakeSymbol(char symbol)
        {
            this.snakeBodySymbol = symbol;
        }

        //Set snake's default position from config file
        public void SetSnakeDefaultPos(int[] position)
        {
            this.snakeXPos = position[globalVar.xAxisIndex];
            this.snakeYPos = position[globalVar.yAxisIndex];
            snakeBody.Add(position);
        }
    }
}