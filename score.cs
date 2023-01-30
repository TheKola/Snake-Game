using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace snakeGame
{
    public class score
    {
        const string FILE_NAME = "../../scoreFile.bin"; //Location of score file
        const int FIRST_SCORE_INDEX = 0;

        //Check if score file exisits and deserialise the data
        public void checkScoreFile(int noOfScores)
        {
            List<playerData> playerInfo = new List<playerData>();
            try
            {
                if (File.Exists(FILE_NAME))
                {
                    playerInfo = deserialiseData();
                    DisplayHighScore(playerInfo, noOfScores);
                }
                else
                {
                    Console.WriteLine("\nNo Scores Found...");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("\nError loading the file");
            }
        }

        //Function to display highscores
        public void DisplayHighScore(List<playerData> info, int noOfScoresToDisplay)
        {
            //Code to arrange the list in desending order by property in the object taken from
            //"How to Sort a List<T> by a property in the object", stackoverflow
            //https://stackoverflow.com/questions/3309188/how-to-sort-a-listt-by-a-property-in-the-object
            //[Accessed 11-12-2022]

            List<playerData> desendingList = info.OrderByDescending(x => x.points).ToList();

            Console.WriteLine("\nHigh scores:\n");

            for (int i = FIRST_SCORE_INDEX; i < desendingList.Count && i < noOfScoresToDisplay; i++)
            {
                playerData player = desendingList[i];
                Console.WriteLine("{0} : {1} [{2} mode, {3} layout size]", player.name, player.points, player.gameMode, player.layoutSize);
            }

        }

        //Write scores to the file
        public void WriteScores(List<playerData> info)
        {
            List <playerData> playerInfo = new List<playerData>();
            try
            {
                if (File.Exists(FILE_NAME))
                {
                    playerInfo = deserialiseData();
                    playerInfo.AddRange(info); //Add the new player details to score list
                    serialseData(playerInfo);
                }
                else
                {
                    serialseData(info);
                }

            }
            catch (IOException)
            {
                Console.WriteLine("\nError loading the file");
            }
        }

        //Serialise the file
        private void serialseData(List<playerData> listToSerialse)
        {
            using (Stream stream = File.Open(FILE_NAME, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, listToSerialse);
            }
        }

        //Deserialise the file
        private List<playerData> deserialiseData()
        {
            List<playerData> player = new List<playerData>();

            using (Stream stream = File.Open(FILE_NAME, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                player = (List<playerData>)bin.Deserialize(stream);
            }

            return player;
        }
    }
}


