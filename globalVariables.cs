using System;

namespace snakeGame
{
    static class globalVar
    {
        //Constant variables used by multiple classes
        public static int xAxisIndex = 0;
        public static int yAxisIndex = 1;

        public static string classicMode = "Classic";
        public static string arcadeMode = "Arcade";

        public static string largeLayout = "Large";
        public static string mediumLayout = "Medium";
        public static string smallLayout = "Small";

        public static int dimensionStart = 1;
        public static int dimensionEndBuffer = 1;

        public static char stringDelimiter = ':';
        public static string skipLineCharacter = "-";

        public static string displayPrompt = "Designed and Developed with Love by Arslaan Kola";

        public static string[] configFile =
        {
            "-- This file consist of requried data to play the game smoothly",
            "-- WARNING : DO NOT CHANGE ANY VALUES OR NAMES WITHOUT THE ADMINS APPROVAL",
            "------------------------------------------------------------------------\n",
            "gameLayout:*",
            "--This symbol is how layout of the game will look.",
            "snakeBody:*",
            "--This symbol is how the snake looks.\n",
            "food:F",
            "--This symbol is how the food will look.\n",
            "snakeXPosition:5",
            "--This is the snake's initial X position(Default = 5)",
            "snakeYPosition: 5",
            "--This is the snake's initial Y position(Default = 5)\n",
            "snakeInitialSpeed:80",
            "--This is the inital speed of the snake(Default value = 120)",
            "snakeMaxSpeed:20",
            "--This is the max speed of the snake(Default value = 20)",
            "snakeSpeedIncreasedBy:5",
            "--This is the speed by which the speed of the snake will reduce(Default value = 5)\n",
            "-- Please Note: The speed is in decreasing order, more the speed slower the snake.\n",
            "noOfLayoutSizes:3",
            "--This is the number of Layout sizes available(Default value = 3 (Large, Medium, Small))\n",
            "largeLayout:1",
            "--This is the percentage amount of the screen when playing large size(Default value = 1)",
            "mediumLayout:0.7",
            "--This is the percentage amount of the screen when playing medium size(Default value = 0.7)",
            "smallLayout:0.3",
            "--This is the percentage amount of the screen when playing small size(Default value = 0.3)\n",
            "displayHighScores:5",
            "--This is the number of high scores to display(Default value = 5)\n",
            "noOfGameModes:2",
            "--This is the number of game modes(Default value = 2 (Claasic, Arcade))\n",
            "noOfMenuOptions:3",
            "--This is the number of options in the game menu(Default value = 3 (Play game, High scores, Exit))\n",
            "delay:1000",
            "--This is the time (milliseconds) delay after the game is over(Default value = 1000)\n",
            "------------------------------------------------------------------------"
        };

        public static string layout = "gameLayout";
        public static string snakeSymbol = "snakeBody";
        public static string foodSymbol = "food";
        public static string xPos = "snakeXPosition";
        public static string yPos = "snakeYPosition";
        public static string initialSpeed = "snakeInitialSpeed";
        public static string maxSpeed = "snakeMaxSpeed";
        public static string speedChange = "snakeSpeedIncreasedBy";
        public static string layouts = "noOfLayoutSizes";
        public static string large = "largeLayout";
        public static string medium = "mediumLayout";
        public static string small = "smallLayout"; 
        public static string gameModes = "noOfGameModes";
        public static string menuOptions = "noOfMenuOptions";
        public static string delay = "delay";
        public static string scores = "displayHighScores";
    }
}