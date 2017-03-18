using System;

namespace ConsoleGameEngine.Models
{
    public struct Pixel
    {
        public char Character { get; set; }

        public Point Position { get; set; }
        public bool IsForground { get; set; }

        public ConsoleColor Color { get; set; }

        public Pixel(char character, Point position, ConsoleColor color, bool isForground = true)
        {
            Character = character;
            Position = position;
            Color = color;
            IsForground = isForground;

        }
    }
}
