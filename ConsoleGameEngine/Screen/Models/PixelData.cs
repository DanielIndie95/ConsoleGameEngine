using System;

namespace ConsoleGameEngine.Screen.Models
{
    public struct PixelData
    {
        public ConsoleColor Color { get; set; }
        public char Character { get; set; }
        public PixelData(char character, ConsoleColor color)
        {
            Character = character;
            Color = color;
        }
    }
}
