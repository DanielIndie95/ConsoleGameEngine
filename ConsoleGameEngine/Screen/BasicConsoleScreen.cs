using System;
using ConsoleGameEngine.Models;
using ConsoleGameEngine.Screen.Models;

namespace ConsoleGameEngine
{
    public class BasicConsoleScreen : IDrawableScreen
    {
        protected PixelData[,] PixelsScreen;
        private int _height;
        private int _width;
        protected PixelData DefaultEntity { get; set; }

        public BasicConsoleScreen(int width, int height)
        {
            PixelsScreen = new PixelData[height, width];
            _height = height;
            _width = width;
            DefaultEntity = new PixelData(' ', ConsoleColor.Gray);
            InitiallizeScreenWithDefaults();
            Console.CursorVisible = false;
        }

        public virtual void Draw()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    DrawPixel(PixelsScreen[y, x], x, y);
                }
            }
        }

        public void SetPixel(PixelData pixel, int x, int y)
        {
            if (!PointOutOfBound(x, y))
                PixelsScreen[y, x] = pixel;
        }
        public void SetPixel(PixelData pixel, Point position)
        {
            SetPixel(pixel, position.X, position.Y);
        }

        private bool PointOutOfBound(int x, int y)
        {
            return x < 0 || x >= _width || y < 0 || y >= _height;
        }
        private bool PointOutOfBound(Point position)
        {
            return PointOutOfBound(position.X, position.Y);
        }

        public virtual void ClearScreen()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    SetPixel(DefaultEntity, x, y);
                }
            }
        }

        protected virtual void DrawPixel(PixelData pixel, int x, int y)
        {
            var oldForColor = Console.ForegroundColor;
            var oldBackColor = Console.BackgroundColor;

            Console.SetCursorPosition(x, y);

            if (pixel.IsForground)
                Console.ForegroundColor = pixel.Color;
            else
                Console.BackgroundColor = pixel.Color;

            Console.Write(pixel.Character);
            Console.ForegroundColor = oldForColor;
            Console.BackgroundColor = oldBackColor;
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

                    SetPixel(data, x, y);
                }
            }
        }
    }
}
