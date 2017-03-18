using ConsoleGameEngine.Engine.Models.GraphicsModels;
using ConsoleGameEngine.Engine.Models.Masks;
using System;

namespace ConsoleGameEngine.Engine.Models.Entities
{
    public class BorderEntity : Entity
    {
        public BorderEntity(int width, int height, ConsoleColor color, char blank = ' ')
        {
            Graphics = new Border(width, height, color, blank);
            MaskWidth = width;
            MaskHeight = height;
            Mask = new PixelMask(this);
        }

        public override void Added()
        {
            base.Added();
            GameEngine.World.SendToBack(this);
        }
    }
}
