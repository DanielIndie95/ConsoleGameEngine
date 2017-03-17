using System;
using ConsoleGameEngine.Screen.Models;

namespace ConsoleGameEngine.Engine.Models.graphics
{
    public class AtlasImage : Animation
    {
        public AtlasImage(char[,] atlas, int frameWidth, int frameHeight, ConsoleColor color, int fps, bool start) : base(fps, false)
        {
            ChangeGraphics(ConvertCharArrayToGraphics(atlas, frameWidth, frameHeight, color));
            if (start)
            {
                Start();
            }
        }
        public AtlasImage(string atlas, int frameWidth, int frameHeight, int fps, ConsoleColor color, bool start) : base(fps, false)
        {
            char[,] atlasArr = GetCharArrayFromString(atlas);
            ChangeGraphics(ConvertCharArrayToGraphics(atlasArr, frameWidth, frameHeight, color));
            if (start)
            {
                Start();
            }
        }
        public AtlasImage(string[] atlas, int frameWidth, int frameHeight, int fps, ConsoleColor color, bool start) : base(fps, false)
        {
            char[,] atlasArr = GetCharArrayFromStrings(atlas);
            PixelData[][,] graphics = ConvertCharArrayToGraphics(atlasArr, frameWidth, frameHeight, color);

            ChangeGraphics(graphics);
            if (start)
            {
                Start();
            }
        }
        public AtlasImage(string[] atlas, int fps, ConsoleColor color, bool start) : base(fps, false)
        {
            ChangeGraphics(ConvertStringFramesToGraphics(atlas, color));
            if (start)
            {
                Start();
            }
        }


        private char[,] GetCharArrayFromString(string atlas)
        {
            string[] byNewLine = atlas.Split(new[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
            return GetCharArrayFromStrings(byNewLine);
        }
        private char[,] GetCharArrayFromStrings(string[] atlas)
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

        private PixelData[][,] ConvertCharArrayToGraphics(char[,] atlas, int frameWidth, int frameHeight, ConsoleColor color)
        {
            int framesCount = (atlas.GetLength(0) / frameHeight) * (int)Math.Ceiling(atlas.GetLength(1) / (double)frameWidth);
            PixelData[][,] frames = new PixelData[framesCount][,];
            int frameIndex = 0;
            for (int y = 0; y < atlas.GetLength(0); y += frameHeight)
            {
                for (int x = 0; x < atlas.GetLength(1); x += frameWidth)
                {
                    char[,] frame = GetRectangle(atlas, x, y, frameWidth, frameHeight);
                    PixelData[,] pixelsFrame = GetPixelsFrameFromRectangle(frame, color);
                    frames[frameIndex] = pixelsFrame;
                    frameIndex++;
                }
            }
            return frames;
        }

        private PixelData[][,] ConvertStringFramesToGraphics(string[] atlas, ConsoleColor color)
        {
            PixelData[][,] frames = new PixelData[atlas.Length][,];

            for (int frame = 0; frame < atlas.Length; frame++)
            {
                char[,] frameArr = GetCharArrayFromString(atlas[frame]);
                PixelData[,] pixelsFrame = GetPixelsFrameFromRectangle(frameArr, color);
                frames[frame] = pixelsFrame;
            }
            return frames;
        }

        private PixelData[,] GetPixelsFrameFromRectangle(char[,] frame, ConsoleColor color)
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

        private char[,] GetRectangle(char[,] buffer, int x, int y, int frameWidth, int frameHeight)
        {
            char[,] rectangle = new char[frameHeight, frameWidth];

            for (int top = y; top < y + frameHeight && top < buffer.GetLength(0); top++)
                for (int left = x; left < x + frameWidth && left < buffer.GetLength(1); left++)
                {
                    rectangle[top - y, left - x] = buffer[top, left];
                }
            return rectangle;
        }


    }
}
