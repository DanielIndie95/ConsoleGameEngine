using ConsoleGameEngine.Engine.Models.Masks;
using ConsoleGameEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGameEngine.Engine.Models
{
    public class Entity : ICollidable
    {
        public IDrawableScreen TargetRender { private get; set; }

        public Graphics Graphics;
        public int X { get; set; }
        public int Y { get; set; }
        public int Layer { get; set; }
        public string Type { get; protected set; }
        public bool Active { get; set; } = true;
        public int MaskX { get; set; }
        public int MaskY { get; set; }
        public int MaskWidth { get; set; }
        public int MaskHeight { get; set; }
        public bool Collidable { get; set; }
        private Mask _mask;

        public Entity(int x = 0, int y = 0, Graphics graphics = null, Mask mask = null)
        {
            MaskX = X = x;
            MaskY = Y = y;
            Graphics = graphics;
            MaskWidth = graphics.Width;
            MaskHeight = graphics.Height;
            Collidable = true;
            _mask = mask;
        }

        public bool Collided(string type)
        {
            if (Collidable && _mask != null)
                foreach (Entity entity in GameEngine.World.GetEntities(type))
                {
                    bool collide = _mask.Collide(entity._mask);
                    if (collide)
                        return true;
                }
            return false;
        }
        public bool Collided()
        {
            if (Collidable && _mask != null)
                foreach (Entity entity in GameEngine.World.GetEntities())
                {
                    bool collide = _mask.Collide(entity._mask);
                    if (collide)
                        return true;
                }
            return false;
        }

        public IEnumerable<Entity> Collide()
        {
            if (Collidable && _mask != null)
                foreach (Entity entity in GameEngine.World.GetEntities())
                {
                    bool collide = _mask.Collide(entity._mask);
                    if (collide)
                        yield return entity;
                }
        }
        public IEnumerable<Entity> Collide(string type)
        {
            if (Collidable && _mask != null)
                foreach (Entity entity in GameEngine.World.GetEntities(type))
                {
                    bool collide = _mask.Collide(entity._mask);
                    if (collide)
                        yield return entity;
                }
        }

        public virtual void Update(GameInput input)
        {

        }

        public virtual void Draw()
        {
            if (Graphics != null && Graphics.Visible)
                Graphics.Draw(TargetRender, X, Y);
        }
    }
}
