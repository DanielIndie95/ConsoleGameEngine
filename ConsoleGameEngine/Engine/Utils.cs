using ConsoleGameEngine.Screen.Models;
using System;
using System.Collections.Generic;

namespace ConsoleGameEngine.Engine
{
    public static class Utils
    {
        public static PixelData[,] GetPixelsBufferFromCharsBuffer(char[,] frame, ConsoleColor color)
        {
            PixelData[,] data = new PixelData[frame.GetLength(0), frame.GetLength(1)];
            for (int y = 0; y < frame.GetLength(0); y++)
            {
                for (int x = 0; x < frame.GetLength(1); x++)
                {
                    data[y, x] = new PixelData(frame[y, x], color);
                }
            }
            return data;
        }
        public static char[,] GetRectangle(char[,] buffer, int x, int y, int width, int height)
        {
            char[,] rectangle = new char[height, width];

            for (int top = y; top < y + height && top < buffer.GetLength(0); top++)
                for (int left = x; left < x + width && left < buffer.GetLength(1); left++)
                {
                    rectangle[top - y, left - x] = buffer[top, left];
                }
            return rectangle;
        }
        public static char[,] GetCharArrayFromNewLinesString(string atlas)
        {
            return GetCharArrayFromNewLinesString(atlas, "\\n", "\n");
        }
        public static char[,] GetCharArrayFromNewLinesString(string atlas, params string[] rowSeperators)
        {
            string[] byNewLine = atlas.Split(rowSeperators, StringSplitOptions.RemoveEmptyEntries);
            return GetCharArrayFromStrings(byNewLine);
        }
        public static char[,] GetCharArrayFromStrings(string[] atlas)
        {
            char[,] atlasArr = new char[atlas.Length, atlas[0].Length];
            for (int row = 0; row < atlas.Length; row++)
            {
                for (int col = 0; col < atlas[row].Length; col++)
                {
                    try
                    {
                        atlasArr[row, col] = atlas[row][col];
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        throw new Exception("The string was not formed as a 2d rectangle, pls insert a valid string", e);
                    }
                }
            }
            return atlasArr;
        }
        public static List<string> SplitString(string str, int iterateCount)
        {
            var words = new List<string>();

            for (int i = 0; i < str.Length; i += iterateCount)
                if (str.Length - i >= iterateCount)
                    words.Add(str.Substring(i, iterateCount));
                else
                    words.Add(str.Substring(i, str.Length - i));

            return words;
        }
    }
}
