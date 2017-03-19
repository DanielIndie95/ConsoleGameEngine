using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleGameEngine.Screen.Models;

namespace ConsoleGameEngine.Engine.Models.Masks
{
    public class PixelMask : Mask
    {
        readonly List<Point> _collisionPoints;
        const char IGNORE = ' ';
        public PixelMask(Entity entity) : base(entity)
        {
            _collisionPoints = GetCollisionPoints(entity.Graphics);
            AddCollisionOption(typeof(Hitbox), CollideWithMask);
            AddCollisionOption(typeof(PixelMask), CollideWithPixelMask);
        }

        private List<Point> GetCollisionPoints(Graphics graphics)
        {
            List<Point> points = new List<Point>();
            for (int row = 0; row < graphics.Graphic.GetLength(0); row++)
            {
                for (int col = 0; col < graphics.Graphic.GetLength(1); col++)
                {
                    var pixel = graphics.Graphic[row, col];
                    if (pixel.Character != IGNORE)
                    {
                        points.Add(new Point(col, row));
                    }
                }
            }
            return points;
        }

        protected override bool CollideWithMask(Mask mask)
        {
            foreach (var point in _collisionPoints)
            {
                bool collide = mask.Collide(point);
                if (collide)
                    return true;
            }
            return false;
        }

        private bool CollideWithPixelMask(Mask mask)
        {
            var other = mask as PixelMask;
            if (other == null)
                throw new Exception("Mask was not in the correct type");
            return _collisionPoints.Intersect(other._collisionPoints).Count() != 0;
        }

        protected override bool CollideWithPoint(Point point)
        {
            return _collisionPoints.Any(p => p.Equals(point));
        }
    }
}
