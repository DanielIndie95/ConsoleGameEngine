using System;
using System.Collections.Generic;
using ConsoleGameEngine.Screen.Models;

namespace ConsoleGameEngine.Engine.Models
{
    public abstract class Mask
    {
        public Entity Parent;
        Dictionary<Type, Func<Mask, bool>> _collisionAlgorithemOptions;

        protected Mask(Entity entity)
        {
            Parent = entity;
            InitDictionary();
        }

        private void InitDictionary()
        {
            _collisionAlgorithemOptions = new Dictionary<Type, Func<Mask, bool>>
            {
                { typeof(Mask), CollideWithMask }
            };
        }

        protected Mask()
        {
            InitDictionary();
        }

        public virtual void AssingTo(Entity entity)
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
            if (_collisionAlgorithemOptions.TryGetValue(other.GetType(), out Func<Mask, bool> collisionFunc))
            {
                return collisionFunc(other);
            }
            if (other._collisionAlgorithemOptions.TryGetValue(GetType(), out Func<Mask, bool> otherCollisionFunc))
            {
                return otherCollisionFunc(this);
            }
            return _collisionAlgorithemOptions[typeof(Mask)](other);
        }

        public bool Collide(Point point)
        {
            return Parent.Collidable && CollideWithPoint(point);
        }

        protected virtual bool CollideWithMask(Mask other)
        {
            return Parent.X + Parent.MaskX + Parent.MaskWidth > other.Parent.MaskX + other.Parent.X
                && Parent.Y + Parent.MaskY + Parent.MaskHeight > other.Parent.MaskY + other.Parent.Y
                && Parent.X + Parent.MaskX < other.Parent.MaskX + other.Parent.X + other.Parent.MaskWidth
                && Parent.Y + Parent.MaskY < other.Parent.MaskY + other.Parent.Y + other.Parent.MaskHeight;
        }
        protected abstract bool CollideWithPoint(Point point);

        protected void AddCollisionOption(Type otherType, Func<Mask, bool> collisionAlgorithem)
        {
            if (_collisionAlgorithemOptions.ContainsKey(otherType))
            {
                _collisionAlgorithemOptions[otherType] = collisionAlgorithem;
            }
            else
            {
                _collisionAlgorithemOptions.Add(otherType, collisionAlgorithem);
            }
        }

    }
}
