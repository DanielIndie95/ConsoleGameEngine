using ConsoleGameEngine.Models;
using ConsoleGameEngine.Screen.Models;

namespace ConsoleGameEngine
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
