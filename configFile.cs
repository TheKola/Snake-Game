using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace snakeGame
{
    public class configFile
    {
        const string FILE_NAME = "../../configurationFile.txt"; //Location of the configuration file
        const int FIRST_LINE_INDEX = 0;
        const int CONFIG_VALUE_NAME = 0;
        const int CONFIG_VALUE = 1;
        const int MAX_LAYOUT_PERCENTAGE = 1;
        const float MIN_LAYOUT_PERCENTAGE = 0.3f; 

        private char layoutSymbol, snake, food;
        private int startSpeed, maxSpeed, speedChange, snakeXPos, snakeYPos;
        private float largeLayoutPercentage, mediumLayoutPercentage, smallLayoutPercentage;
        private int noOfHighScores, noOfGameModes, noOfLayoutSizes, noOfMenuOptions, delay;


        //Split the string data into Key-Value pair
        private Dictionary<string, string> configValueFromConfigLine(Dictionary<string, string> valuePair, string data)
        {
            string[] value = data.Split(globalVar.stringDelimiter);
            valuePair[value[CONFIG_VALUE_NAME]] = value[CONFIG_VALUE]; //Store in dictionary (valuePair)
            return valuePair;
        }

        //Load the config file
        public void LoadFile()
        {
            Dictionary<string, string> dictionaryOfValues = new Dictionary<string, string>();
            Console.WriteLine("\nLoading data from configuraion file...\n");

            bool retriveValues = true;

            while (retriveValues)
            {
                if (File.Exists(FILE_NAME))
                {
                    string[] configFile = File.ReadAllLines(FILE_NAME);
                    for (int i = FIRST_LINE_INDEX; i < configFile.Length; i++)
                    {
                        if (string.IsNullOrEmpty(configFile[i]) == true)
                        {
                            continue; //Skip all empty lines
                        }
                        if (!(configFile[i].StartsWith(globalVar.skipLineCharacter))) //Skip all lines beginning with '-'
                        {
                            dictionaryOfValues = configValueFromConfigLine(dictionaryOfValues, configFile[i]); //Split all strings and store in dictinoary
                        }
                    }
                    bool checkIfLoaded = assignValuesExtracedToVariables(dictionaryOfValues);
                    if (checkIfLoaded == false) //if there in error loading configuration file load default values
                    {
                        retriveValues = CreateFileWithDefaultValues(); //Load default values
                        continue;
                    }
                    retriveValues = false;
                }
                else
                {
                    Console.WriteLine("Congifuraion file is missing...\n");
                    retriveValues = CreateFileWithDefaultValues(); //Load default values
                }
            }
        }

        //Create new congif file if error in laoding or file missing
        private bool CreateFileWithDefaultValues()
        {
            Console.WriteLine("Loading default values...\n");
            
            //Default values
            File.WriteAllLines(FILE_NAME, globalVar.configFile); //Write to configuartion file
            return true;
        }

        //Assign the key-value pair to variables
        private bool assignValuesExtracedToVariables(Dictionary<string, string> valuePairs)
        {
            //Load values from configuration file to variables
            try
            {
                this.layoutSymbol = char.Parse(valuePairs[globalVar.layout]);
                this.snake = char.Parse(valuePairs[globalVar.snakeSymbol]);
                this.food = char.Parse(valuePairs[globalVar.foodSymbol]);
                this.snakeXPos = int.Parse(valuePairs[globalVar.xPos]);
                this.snakeYPos = int.Parse(valuePairs[globalVar.yPos]);
                this.startSpeed = int.Parse(valuePairs[globalVar.initialSpeed]);
                this.maxSpeed = int.Parse(valuePairs[globalVar.maxSpeed]);
                this.speedChange = int.Parse(valuePairs[globalVar.speedChange]);
                this.largeLayoutPercentage = float.Parse(valuePairs[globalVar.large]);
                this.mediumLayoutPercentage = float.Parse(valuePairs[globalVar.medium]);
                this.smallLayoutPercentage = float.Parse(valuePairs[globalVar.small]);
                this.noOfHighScores = int.Parse(valuePairs[globalVar.scores]);
                this.noOfGameModes = int.Parse(valuePairs[globalVar.gameModes]);
                this.noOfLayoutSizes = int.Parse(valuePairs[globalVar.layouts]);
                this.noOfMenuOptions = int.Parse(valuePairs[globalVar.menuOptions]);
                this.delay = int.Parse(valuePairs[globalVar.delay]);
                return true;
            }
            catch
            {
                Console.WriteLine("Error loading configuaraion data...\n");
                return false;
            }
        }

        //Functions to return specified values
        public char GetLayoutSymbol() { return this.layoutSymbol; }
        public char GetSnakeSymbol() { return this.snake; }
        public char GetFoodSymbol() { return this.food; }
        public int GetSnakeStartSpeed() { return this.startSpeed; }
        public int GetSnakeMaxSpeed() { return this.maxSpeed; }
        public int GetSpeedChangeBy() { return this.speedChange; }
        public float GetLayoutSize(string size)
        {
            if (size == globalVar.largeLayout)
            {
                if (largeLayoutPercentage > MAX_LAYOUT_PERCENTAGE)
                {
                    return MAX_LAYOUT_PERCENTAGE;
                }
                return largeLayoutPercentage;
            }
            if (size == globalVar.mediumLayout) { return mediumLayoutPercentage; }

            if (size == globalVar.smallLayout)
            {
                if (smallLayoutPercentage < MIN_LAYOUT_PERCENTAGE)
                {
                    return MIN_LAYOUT_PERCENTAGE;
                }
                return smallLayoutPercentage;
            }
            return 0;
        }
        public int GetNoOfHighScores() { return this.noOfHighScores; }
        public int GetNoOfGameModes() { return this.noOfGameModes; }
        public int GetNoOfLayoutSizes() { return this.noOfLayoutSizes; }
        public int GetNoOfMenuOptions() { return this.noOfMenuOptions; }
        public int GetTimeDelay() { return this.delay; }
        public int[] GetSnakeDefualtPos()
        {
            int[] snakeDefaultPos = new int[] { snakeXPos, snakeYPos };
            return snakeDefaultPos;
        }

    }
}
