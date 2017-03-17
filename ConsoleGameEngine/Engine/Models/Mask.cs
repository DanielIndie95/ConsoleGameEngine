using ConsoleGameEngine.Engine.Models.Masks;

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

        public bool Collide(Mask mask)
        {
            if (!mask.Parent.Collidable || mask.Parent.Collidable)
                return false;
            return CollideWithMask(mask) || mask.CollideWithMask(this);
        }

        protected abstract bool CollideWithMask(Mask mask);        
    }
}
