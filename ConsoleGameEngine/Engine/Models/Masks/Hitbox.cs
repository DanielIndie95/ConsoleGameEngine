using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGameEngine.Engine.Models.Masks
{
    public class Hitbox : Mask
    {
        private int _offsetX;
        private int _offsetY;
        private int _width;
        private int _height;

        public Hitbox(ICollidable entity) : base(entity)
        {
            _offsetY = 0;
            _offsetX = 0;
            _width = entity.MaskWidth;
            _height = entity.MaskHeight;
        }
        public Hitbox(int offsetX, int offsetY, int width, int height) : base()
        {
            _offsetX = offsetX;
            _offsetY = offsetY;
            _width = width;
            _height = height;
        }

        protected override bool CollideWithMask(Mask other)
        {
            return Parent.MaskX + _offsetX + _width > other.Parent.MaskX
                && Parent.MaskY + _offsetY + _height > other.Parent.MaskY
                && Parent.MaskX + _offsetX < other.Parent.MaskX + other.Parent.MaskWidth
                && Parent.MaskY + _offsetY < other.Parent.MaskY + other.Parent.MaskHeight;
        }
    }
}
