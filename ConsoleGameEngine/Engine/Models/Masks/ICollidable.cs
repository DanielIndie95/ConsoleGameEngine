﻿namespace ConsoleGameEngine.Engine.Models.Masks
{
    public interface ICollidable
    {
        int MaskX { get; set; }
        int MaskY { get; set; }
        int MaskWidth { get; set; }
        int MaskHeight { get; set; }
        int X { get; set; }
        int Y { get; set; }
        bool Collidable { get; set; }

    }
}
