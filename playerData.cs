using System;

namespace snakeGame
{
    [Serializable]
    public class playerData
    {
        public string name;
        public int points;
        public string gameMode;
        public string layoutSize;

        public playerData(string playerName, int pointsScored, string mode, string size)
        {
            name = playerName;
            points = pointsScored;
            gameMode = mode;
            layoutSize = size;
        }
    }
}
