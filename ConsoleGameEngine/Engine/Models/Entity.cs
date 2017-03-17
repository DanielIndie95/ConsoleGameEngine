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

        public Graphics Graphics { get; protected set; }
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
        protected Mask Mask;

        public Entity(int x = 0, int y = 0, Graphics graphics = null, Mask mask = null)
        {
            X = x;
            Y = y;
            MaskX = MaskY = 0;
            Graphics = graphics;
            if (graphics != null)
            {
                MaskWidth = graphics.Width;
                MaskHeight = graphics.Height;
            }
            Collidable = true;
            Mask = mask;
        }

        public virtual void Added()
        {

        }

        public bool Collided(string type)
        {
            if (Collidable && Mask != null)
                foreach (Entity entity in GameEngine.World.GetEntities(type))
                {
                    bool collide = Mask.Collide(entity.Mask);
                    if (collide)
                        return true;
                }
            return false;
        }
        public bool Collided()
        {
            if (Collidable && Mask != null)
                foreach (Entity entity in GameEngine.World.GetEntities())
                {
                    bool collide = Mask.Collide(entity.Mask);
                    if (collide)
                        return true;
                }
            return false;
        }

        protected void SetHitBoxToGrahpics()
        {
            Mask = new Hitbox(this);
        }

        public bool Collided(int virtualX, int virtualY)
        {
            int previousX = X, previousY = Y;
            X = virtualX;
            Y = virtualY;
            if (Collidable && Mask != null)
                foreach (Entity entity in GameEngine.World.GetEntities())
                {
                    if (entity != this)
                    {
                        bool collide = Mask.Collide(entity.Mask);
                        if (collide)
                        {
                            X = previousX;
                            Y = previousY;
                            return true;
                        }
                    }
                }
            X = previousX;
            Y = previousY;
            return false;
        }

        public IEnumerable<Entity> Collide()
        {
            if (Collidable && Mask != null)
                foreach (Entity entity in GameEngine.World.GetEntities())
                {
                    bool collide = Mask.Collide(entity.Mask);
                    if (collide)
                        yield return entity;
                }
        }
        public IEnumerable<Entity> Collide(string type)
        {
            if (Collidable && Mask != null)
                foreach (Entity entity in GameEngine.World.GetEntities(type))
                {
                    bool collide = Mask.Collide(entity.Mask);
                    if (collide)
                        yield return entity;
                }
        }

        public IEnumerable<Entity> Collide(int virtualX, int virtualY)
        {
            int previousX = MaskX, previousY = MaskY;
            MaskX = virtualX;
            MaskY = virtualY;
            if (Collidable && Mask != null)
                foreach (Entity entity in GameEngine.World.GetEntities())
                {
                    bool collide = Mask.Collide(entity.Mask);
                    if (collide)
                        yield return entity;
                }

            MaskX = previousX;
            MaskY = previousY;
        }

        public IEnumerable<Entity> Collide(string type, int virtualX, int virtualY)
        {
            int previousX = MaskX, previousY = MaskY;
            MaskX = virtualX;
            MaskY = virtualY;
            if (Collidable && Mask != null)
                foreach (Entity entity in GameEngine.World.GetEntities(type))
                {
                    bool collide = Mask.Collide(entity.Mask);
                    if (collide)
                        yield return entity;
                }

            MaskX = previousX;
            MaskY = previousY;
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
