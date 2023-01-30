using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace snakeGame
{
    class Program
    {
        const int LEAST_OPTION = 1;

        static Random randomNumber = new Random();

        //Displaying food within the layout and not on the snake
        static int[] displayFood(int[] layout, List<int[]> snakeBody, char symbol)
        {
            const int SNAKE_START = 0;

            bool snakeOnFood = true;
            int locationXAxis, locationYAxis;

            do
            {
                //Generate food location within the layout 
                locationXAxis = randomNumber.Next(globalVar.dimensionStart, layout[globalVar.xAxisIndex] - globalVar.dimensionEndBuffer);
                locationYAxis = randomNumber.Next(globalVar.dimensionStart, layout[globalVar.yAxisIndex] - globalVar.dimensionEndBuffer);

                for (int i = SNAKE_START; i < snakeBody.Count; i++)
                {
                    if (locationXAxis == snakeBody[i][globalVar.xAxisIndex] && locationYAxis == snakeBody[i][globalVar.yAxisIndex]) //Generate new random location of the food if its loacted on the snake 
                    {
                        continue;
                    }
                }

                snakeOnFood = false;
            } while (snakeOnFood);

            Console.SetCursorPosition(locationXAxis, locationYAxis);
            Console.Write(symbol);

            int[] location = new int[] { locationXAxis, locationYAxis };
            return location;
        }

        //check if user have entered valid inputs
        static int validInput(int limit)
        {
            int option;
            bool checkFlag;

            do
            {
                Console.Write("Please type in your choice : ");
                checkFlag = int.TryParse(Console.ReadLine(), out option); //Check if intergers are entered
            } while (option < LEAST_OPTION || option > limit || checkFlag == false); //Repeat till the input is within the range

            return option;
        }

        static int gameMenu(int noOfOptions)
        {
            Console.WriteLine("\nWelcome to the Snake Game!\n\n1. Play Game\n2. High Scores\n3. Exit\n");
            int choice = validInput(noOfOptions);
            return choice;
        }

        static void Main()
        {
            //Define constants variables
            const int SCORE_OPTION = 2;
            const int EXIT_OPTION = 3;

            configFile gameValues = new configFile();
            gameLayout layout = new gameLayout();
            score newScore = new score();

            Console.CursorVisible = false; // Hide the cursor

            ConsoleKeyInfo keyPressed;
            string playerName;
            int choiceFromMenu;

            while (true)
            {
                Console.Clear();

                snake newSnake = new snake();
                var newPlayer = new List<playerData>();
                gameValues.LoadFile(); //Load all the configuration values
                Thread.Sleep(gameValues.GetTimeDelay());

                int snakeSpeed = gameValues.GetSnakeStartSpeed();
                int snakeReduceSpeedBy = gameValues.GetSpeedChangeBy();
                int snakeMaxSpeed = gameValues.GetSnakeMaxSpeed();
                newSnake.SetSnakeSymbol(gameValues.GetSnakeSymbol()); //Get the snake symbol from the configFile class and set it in snake class

                bool boundaryTouch = false;
                bool snakeTouchItself = false;
                int points = 0;

                Console.Clear();

                newSnake.SetSnakeDefaultPos(gameValues.GetSnakeDefualtPos());//Snake's Default Position
                
                choiceFromMenu = gameMenu(gameValues.GetNoOfMenuOptions());
                while (choiceFromMenu == SCORE_OPTION)
                {
                    Console.Clear();
                    newScore.checkScoreFile(gameValues.GetNoOfHighScores()); //Display High scores based on number of high scores to display from config file
                    do
                    {
                        Console.Write("\nPress Enter key to go back to the main Menu...");
                        keyPressed = Console.ReadKey(true);
                    } while (keyPressed.Key != ConsoleKey.Enter); //Returing back to main menu when ENTER is pressed

                    Console.Clear();
                    choiceFromMenu = gameMenu(gameValues.GetNoOfMenuOptions());
                }

                if (choiceFromMenu == EXIT_OPTION)
                {
                    Console.WriteLine("\nExiting the game");
                    Thread.Sleep(gameValues.GetTimeDelay());
                    break;
                }

                Console.Clear();
                Console.WriteLine();

                do
                {
                    Console.Write("Please enter your name (Only Letters) : ");
                    playerName = Console.ReadLine();
                } while (!(playerName.All(Char.IsLetter)) || String.IsNullOrEmpty(playerName)); //Saving player's name (only letters allowed), check if every character is a letter and is not empty

                Console.Clear();

                Console.WriteLine("\nChoose the mode of the game:\n\n1. Classic - Pass through the walls\n2. Arcade - Do not hit the walls\n");
                string gameMode = validInput(gameValues.GetNoOfGameModes()) == LEAST_OPTION ? globalVar.classicMode : globalVar.arcadeMode; //Check which mode of the game player wants to play (Classic mode is the least mode)
                Console.Clear();

                Console.WriteLine("\nChoose the size of the layout:\n\n1. Large\n2. Medium\n3. Small\n");
                int layoutSizeOption = validInput(gameValues.GetNoOfLayoutSizes()); //Check which size of the layout player wants to play
                Console.Clear();

                Console.WriteLine("\nPress Arrow keys to control and move the snake.");
                Thread.Sleep(gameValues.GetTimeDelay());
                Console.WriteLine("\nPress Escape (Esc) to quit the game. All the best :)");
                Thread.Sleep(gameValues.GetTimeDelay()); 
                Thread.Sleep(gameValues.GetTimeDelay()); //Longer delay to read the instructions
                Console.Clear();

                //Store the layout name according to the prefered choice; Small (3) > Medium (2) > Large (1)
                string layoutSize;
                if (layoutSizeOption == LEAST_OPTION)
                {
                    layoutSize = globalVar.largeLayout;
                }
                else if (layoutSizeOption == gameValues.GetNoOfLayoutSizes())
                {
                    layoutSize = globalVar.smallLayout;
                }
                else
                {
                    layoutSize = globalVar.mediumLayout;
                }
                
                layout.SetDimensions(gameValues.GetLayoutSize(layoutSize)); //Set the layout dimensions in gameLayout class according to the players choice
                Console.Clear();

                int[] layoutDimensions = layout.GetDimensions(); //Array to store layout dimensions, used to detect if snake has hit the boundary or no
                int[] snakeRealTimePos = new int[newSnake.GetSnakeHeadPosition().Length];//Array to store snake's x & y location, used to find out if snake has eaten the food or no

                layout.DrawLayout(gameValues.GetLayoutSymbol());//Draw the game layout with the symbol from config file

                newSnake.DrawSnake(); //Draw snake's first loaction, start of the game

                int[] foodLocation = displayFood(layoutDimensions, newSnake.GetSnakeBody(), gameValues.GetFoodSymbol());

                while (!boundaryTouch && !snakeTouchItself)
                {
                    boundaryTouch = newSnake.MoveSnake(layoutDimensions, gameMode); //Move the snake and check if it has touced the boundary
                    newSnake.DeleteOldBodyPath(); //Delete the old body of the snake, to give moving effect
                    snakeRealTimePos = newSnake.GetSnakeHeadPosition(); //Get snake realtime position (of head) to compare it with food
                    newSnake.DrawSnake();
                    snakeTouchItself = newSnake.GetSnakeTouchItself(snakeRealTimePos);
                    if (Enumerable.SequenceEqual(foodLocation, snakeRealTimePos)) //If snake has eaten the food
                    {
                        newSnake.IncreaseSnakeSize(snakeRealTimePos);
                        foodLocation = displayFood(layoutDimensions, newSnake.GetSnakeBody(), gameValues.GetFoodSymbol()); //Change food location
                        if (snakeSpeed > snakeMaxSpeed) { snakeSpeed -= snakeReduceSpeedBy; } //Increase the speed of the snake by reducing the time delay
                        points++;
                        layout.DrawLayout(gameValues.GetLayoutSymbol(), points);
                    }

                    Thread.Sleep(snakeSpeed);
                }
                Console.Clear();

                newPlayer.Add(new playerData(playerName, points, gameMode, layoutSize)); //Save the player details
                newScore.WriteScores(newPlayer);
                Console.WriteLine("\nYour score is : {0}", points); //Display the final score
                Thread.Sleep(gameValues.GetTimeDelay());
                Console.WriteLine("\nRedirecting to the Main menu...");
                Thread.Sleep(gameValues.GetTimeDelay());
            }
        }
    }
}
