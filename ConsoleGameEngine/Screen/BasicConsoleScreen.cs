using System;
using ConsoleGameEngine.Models;
using ConsoleGameEngine.Screen.Models;

namespace ConsoleGameEngine
{
    public class BasicConsoleScreen : IDrawableScreen
    {
        protected Pixel[,] PixelsScreen;
        private int _height;
        private int _width;
        protected Pixel DefaultEntity { get; set; }

        public BasicConsoleScreen(int width, int height)
        {
            PixelsScreen = new Pixel[height, width];
            _height = height;
            _width = width;
            DefaultEntity = new Pixel(' ', new Point(), ConsoleColor.Gray);
            InitiallizeScreenWithDefaults();
        }

        public virtual void Draw()
        {
            Console.CursorVisible = false;
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    DrawPixel(PixelsScreen[y, x]);
                }
            }
        }

        public void SetPixel(Pixel pixel)
        {
            if (!PixelOutOfBound(pixel))
                PixelsScreen[pixel.Position.Y, pixel.Position.X] = pixel;
        }

        private bool PixelOutOfBound(Pixel pixel)
        {
            return pixel.Position.X < 0 || pixel.Position.X >= _width || pixel.Position.Y < 0 || pixel.Position.Y >= _height;
        }

        public virtual void ClearScreen()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    SetPixel(new Pixel(DefaultEntity.Character, new Point(x, y), DefaultEntity.Color));
                }
            }
        }

        protected virtual void DrawPixel(Pixel pixel)
        {
            var oldColor = Console.ForegroundColor;
            Console.SetCursorPosition(pixel.Position.X, pixel.Position.Y);
            if (pixel.IsForground)
                Console.ForegroundColor = pixel.Color;
            else
                Console.BackgroundColor = pixel.Color;
            Console.Write(pixel.Character);
            Console.ForegroundColor = oldColor;
        }

        private void InitiallizeScreenWithDefaults()
        {
            ClearScreen();
        }

        public void SetRectangle(int left, int top, PixelData[,] rectangle)
        {
            for (int y = top; y < top + rectangle.GetLength(0); y++)
            {
                for (int x = left; x < left + rectangle.GetLength(1); x++)
                {
                    PixelData data = rectangle[y - top, x - left];

                    SetPixel(new Pixel(data.Character, new Point(x, y), data.Color, data.IsForground));
                }
            }
        }
    }
}
