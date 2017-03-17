using System;

namespace ConsoleGameEngine.Models
{
    public struct Pixel
    {
        public char Character { get; set; }

        public Point Position { get; set; }

        public ConsoleColor Color { get; set; }
       
        public Pixel(char character , Point position , ConsoleColor color)
        {
            Character = character;
            Position = position;
            Color = color;
        }
    }
}
