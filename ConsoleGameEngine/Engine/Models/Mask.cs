using ConsoleGameEngine.Engine.Models.Masks;
using ConsoleGameEngine.Models;

namespace ConsoleGameEngine.Engine.Models
{
    public abstract class Mask
    {
        public ICollidable Parent;

        public Mask(ICollidable entity)
        {
            Parent = entity;
        }

        public Mask()
        {

        }

        public void AssingTo(ICollidable entity)
        {
            Parent = entity;
            Update();
        }

        public virtual void Update()
        {

        }

        public bool Collide(Mask other)
        {
            if (other == null || !other.Parent.Collidable || !Parent.Collidable)
                return false;
            return CollideWithMask(other) && other.CollideWithMask(this);
        }

        public bool Collide(Point point)
        {
            if (!Parent.Collidable)
                return false;
            return CollideWithPoint(point);
        }

        protected abstract bool CollideWithMask(Mask mask);
        protected abstract bool CollideWithPoint(Point point);

    }
}
