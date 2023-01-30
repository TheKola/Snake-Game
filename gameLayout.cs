using System;
using System.Threading;

namespace snakeGame
{
    class gameLayout
    {
        const int DIMENSION_START = 0;
        const int DEFAULT_SCORE = 0;

        private int layoutWidth;
        private int layoutHeight;

        public void DrawLayout(char boundarySymbol, int score = DEFAULT_SCORE)
        {
            //Draw top horizontal line
            for (int i = DIMENSION_START; i < layoutWidth; i++)
            {
                Console.SetCursorPosition(i, DIMENSION_START);
                Console.Write(boundarySymbol);
            }

            //Draw left vertical line
            for (int i = DIMENSION_START; i < layoutHeight; i++)
            {
                Console.SetCursorPosition(DIMENSION_START, i);
                Console.Write(boundarySymbol);
            }

            //Draw right vertical line
            for (int i = DIMENSION_START; i < layoutHeight; i++)
            {
                Console.SetCursorPosition(layoutWidth, i);
                Console.Write(boundarySymbol);
            }

            //Draw bottom horizontal line
            for (int i = DIMENSION_START; i < layoutWidth; i++)
            {
                Console.SetCursorPosition(i, layoutHeight);
                Console.Write(boundarySymbol);
            }

            Console.Write("\n Score : {0}", score);

            //Print the prompt the centre of the screen at the bottom
            Console.SetCursorPosition((int)(Console.WindowWidth / 2 - globalVar.displayPrompt.Length / 2), Console.WindowHeight - globalVar.dimensionEndBuffer); //2 is to divide the length into half
            Console.Write(globalVar.displayPrompt);
        }

        public int[] GetDimensions()
        {
            int[] dimensions = new int[] { layoutWidth, layoutHeight };
            return dimensions;
        }

        //Set the dimenions of the layout according to the user choice
        public void SetDimensions(float dimensionPercentage)
        {
            this.layoutWidth = (int)((Console.WindowWidth - globalVar.dimensionEndBuffer) * dimensionPercentage);
            this.layoutHeight = (int)((Console.WindowHeight - globalVar.dimensionEndBuffer - globalVar.dimensionEndBuffer) * dimensionPercentage); //Subtract twice so that the boundary is above the prompt line.
        }
    }
}

