using ConsoleGameEngine.Models;

namespace ConsoleGameEngine.Engine.Models.Masks
{
    public class Hitbox : Mask
    {
        private int _width;
        private int _height;
        private int _x;
        private int _y;

        public Hitbox(int width = 1, int height = 1, int x = 0, int y = 0) : base()
        {
            _width = width;
            _height = height;
            _x = x;
            _y = y;
            AddCollisionOption(GetType(), CollideWithHitBox);
        }

        public override void Update()
        {
            base.Update();
            if (Parent != null)
            {
                Parent.MaskHeight = _height;
                Parent.MaskWidth = _width;
                Parent.MaskX = _x;
                Parent.MaskY = _y;
            }
        }


        protected virtual bool CollideWithHitBox(Mask mask)
        {
            Hitbox other = mask as Hitbox;
            return Parent.X + _x + _width > other._x + other.Parent.X
                && Parent.Y + _y + _height > other._y + other.Parent.Y
                && Parent.X + _x < other._x + other.Parent.X + other._width
                && Parent.Y + _y < other._y + other.Parent.Y + other._height;
        }

        protected override bool CollideWithPoint(Point point)
        {
            return Parent.X + _x <= point.X && Parent.X + _x + _width > point.X
                && Parent.Y + _y <= point.Y && Parent.Y + _y + _height > point.Y;
        }
    }
}
