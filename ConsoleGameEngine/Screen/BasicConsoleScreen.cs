using System;
using ConsoleGameEngine.Screen.Models;

namespace ConsoleGameEngine.Screen
{
    public class BasicConsoleScreen : IDrawableScreen
    {
        protected PixelData[,] PixelsScreen;
        private readonly int _height;
        private readonly int _width;
        protected PixelData DefaultEntity { get; set; }
        readonly Action _drawFunc;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="blackAndWhiteMode">Black and white is faster</param>
        public BasicConsoleScreen(int width, int height, bool blackAndWhiteMode = true)
        {
            PixelsScreen = new PixelData[height, width];
            _height = height;
            _width = width;
            DefaultEntity = new PixelData(' ', ConsoleColor.Gray);
            InitiallizeScreenWithDefaults();
            Console.CursorVisible = false;
            if (blackAndWhiteMode)
                _drawFunc = DrawBlackAndWhite;
            else
                _drawFunc = DrawColor;
        }

        public virtual void Draw()
        {
            _drawFunc();
        }

        public void DrawBlackAndWhite()
        {
            string buffer = "";
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    buffer += PixelsScreen[y, x].Character;
                }
                buffer += Environment.NewLine;
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(buffer);
        }

        /// <summary>
        /// This is much slower
        /// </summary>
        public void DrawColor()
        {
            int x = 1;
            PixelData lastPixel = PixelsScreen[0, 0];
            string batch = lastPixel.Character + "";
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < _height; y++)
            {
                for (; x < _width; x++)
                {
                    PixelData pixel = PixelsScreen[y, x];
                    if (pixel.Color == lastPixel.Color && pixel.IsForground == lastPixel.IsForground)
                    {
                        batch += pixel.Character;
                    }
                    else
                    {
                        DrawBatch(batch, lastPixel);
                        batch = pixel.Character + "";
                    }
                    lastPixel = pixel;
                }
                batch += Environment.NewLine;
                x = 0;
            }
            DrawBatch(batch, lastPixel);

        }

        private void DrawBatch(string batch, PixelData lastPixel)
        {
            var oldForColor = Console.ForegroundColor;
            var oldBackColor = Console.BackgroundColor;

            if (lastPixel.IsForground)
                Console.ForegroundColor = lastPixel.Color;
            else
                Console.BackgroundColor = lastPixel.Color;

            Console.Write(batch);
            Console.ForegroundColor = oldForColor;
            Console.BackgroundColor = oldBackColor;
        }
        protected virtual void DrawPixel(PixelData pixel)
        {
            DrawBatch(pixel.Character + "", pixel);
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
