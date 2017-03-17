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
            char[,] atlasArr = Utils.GetCharArrayFromNewLinesString(atlas);
            ChangeGraphics(ConvertCharArrayToGraphics(atlasArr, frameWidth, frameHeight, color));
            if (start)
            {
                Start();
            }
        }
        public AtlasImage(string[] atlas, int frameWidth, int frameHeight, int fps, ConsoleColor color, bool start) : base(fps, false)
        {
            char[,] atlasArr = Utils.GetCharArrayFromStrings(atlas);
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

        private PixelData[][,] ConvertCharArrayToGraphics(char[,] atlas, int frameWidth, int frameHeight, ConsoleColor color)
        {
            int framesCount = (atlas.GetLength(0) / frameHeight) * (int)Math.Ceiling(atlas.GetLength(1) / (double)frameWidth);
            PixelData[][,] frames = new PixelData[framesCount][,];
            int frameIndex = 0;
            for (int y = 0; y < atlas.GetLength(0); y += frameHeight)
            {
                for (int x = 0; x < atlas.GetLength(1); x += frameWidth)
                {
                    char[,] frame = Utils.GetRectangle(atlas, x, y, frameWidth, frameHeight);
                    PixelData[,] pixelsFrame = Utils.GetPixelsBufferFromCharsBuffer(frame, color);
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
                char[,] frameArr = Utils.GetCharArrayFromNewLinesString(atlas[frame]);
                PixelData[,] pixelsFrame = Utils.GetPixelsBufferFromCharsBuffer(frameArr, color);
                frames[frame] = pixelsFrame;
            }
            return frames;
        }






    }
}
