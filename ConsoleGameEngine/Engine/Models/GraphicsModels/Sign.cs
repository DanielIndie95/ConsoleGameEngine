using ConsoleGameEngine.Screen.Models;
using System;

namespace ConsoleGameEngine.Engine.Models.GraphicsModels
{
    public class Sign : Graphics
    {
        public Sign(char sign, ConsoleColor color)
        {
            GraphicData = new[,] { { new PixelData(sign, color) } };
        }
    }
}
