using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGameEngine.Models;

namespace ConsoleGameEngine.Engine.Models.Masks
{
    public class Hitbox : Mask
    {
        private int _width;
        private int _height;

        public Hitbox(ICollidable entity) : base(entity)
        {
            _width = entity.MaskWidth;
            _height = entity.MaskHeight;
        }
       
        protected override bool CollideWithMask(Mask other)
        {
            return Parent.X + Parent.MaskX + _width > other.Parent.MaskX + other.Parent.X
                && Parent.Y + Parent.MaskY + _height > other.Parent.MaskY + other.Parent.Y
                && Parent.X + Parent.MaskX < other.Parent.MaskX + other.Parent.X + other.Parent.MaskWidth
                && Parent.Y + Parent.MaskY < other.Parent.MaskY + other.Parent.Y + other.Parent.MaskHeight;
        }

        protected override bool CollideWithPoint(Point point)
        {
            return Parent.X + Parent.MaskX <= point.X && Parent.X + Parent.MaskX + _width > point.X
                && Parent.Y + Parent.MaskY <= point.Y && Parent.Y + Parent.MaskY + _height > point.Y;

        }
    }
}
