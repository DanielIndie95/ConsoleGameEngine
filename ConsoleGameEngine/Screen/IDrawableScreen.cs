﻿using ConsoleGameEngine.Screen.Models;

namespace ConsoleGameEngine.Screen
{
    public interface IDrawableScreen
    {
        void SetPixel(PixelData pixel, int x, int y);
        void SetPixel(PixelData pixel, Point position);

        void SetRectangle(int left, int top, PixelData[,] rectangle);

        void Draw();

        void ClearScreen();
    }
}
