using System;
using ConsoleGameEngine.Screen.Models;

namespace ConsoleGameEngine.Engine.Models.graphics
{
    public class Border : Graphics
    {
        public Border(int width, int height, ConsoleColor color, char blank = ' ')
        {
            GraphicData = GetBorder(width, height, color, blank);
        }

        private PixelData[,] GetBorder(int width, int height, ConsoleColor color, char blank)
        {
            PixelData[,] border = new PixelData[height, width];
            char sign = blank;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (row == 0 || row == height - 1)
                    {
                        sign = '-';
                    }
                    else if (col == 0 || col == width - 1)
                    {
                        sign = '|';
                    }
                    else
                    {
                        sign = blank;
                    }
                    border[row, col] = new PixelData(sign, color);
                }
            }
            return border;
        }
    }
}
