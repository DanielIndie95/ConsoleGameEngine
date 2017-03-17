using ConsoleGameEngine.Models;
using ConsoleGameEngine.Screen.Models;

namespace ConsoleGameEngine
{
    public interface IDrawableScreen
    {
        void SetPixel(Pixel pixel);

        void SetRectangle(int left, int top, PixelData[,] rectangle);

        void Draw();

        void ClearScreen();
    }
}
