using ConsoleGameEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGameEngine.Engine.Models
{
    public class Entity
    {
        public IDrawableScreen TargetRender { private get; set; }

        public Graphics Graphics;
        public int X { get; set; }
        public int Y { get; set; }
        public int Layer { get; set; }
        public string Type { get; protected set; }
        public bool Active { get; set; } = true;

        public Entity(int x = 0, int y = 0, Graphics graphics = null)
        {
            X = x;
            Y = y;
            Graphics = graphics;
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
