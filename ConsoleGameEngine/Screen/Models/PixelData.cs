using System;

namespace ConsoleGameEngine.Screen.Models
{
    public struct PixelData
    {
        public ConsoleColor Color { get; set; }
        public bool IsForground { get; set; }
        public char Character { get; set; }
        public PixelData(char character, ConsoleColor color, bool isForground = true)
        {
            Character = character;
            Color = color;
            IsForground = isForground;
        }
    }
}
