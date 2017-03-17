using ConsoleGameEngine.Engine;
using System;
using ConsoleGameEngine.Engine.Models;
using ConsoleGameEngine.Engine.Models.graphics;

namespace ConsoleGameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEngine engine = new SpecialGameEngine();
        }
    }

    public class SpecialGameEngine : GameEngine
    {
        SpecialGameWorld world;

        public SpecialGameEngine() : base(20, 20, 30)
        {
        }

        public override void Begin()
        {
            base.Begin();
            world = new SpecialGameWorld();
            GameEngine.World = world;
        }
    }

    public class SpecialGameWorld : GameWorld
    {
        public SpecialGameWorld()
        {
        }

        public override void Begin()
        {
            base.Begin();
            string frame1 = @"  :)  \n =|= \n | | ";
            string frame2 = @"  :(  \n ^|^ \n | | ";
            string frame3 = @"  :|  \n )|( \n | - ";
            string[] anim = {
              frame1 , frame2 , frame3
            };

            AtlasImage atlas = new AtlasImage(anim, 3, ConsoleColor.White, false);
            Add(new BasicEntity(4, 6, 1, atlas));
            Add(new Border(GameEngine.Engine.Width, GameEngine.Engine.Height, ConsoleColor.White), 3);
        }

        public override void Update(GameInput input)
        {
            base.Update(input);
            if (input.Key == ConsoleKey.Escape)
            {
                GameEngine.Engine.Exit();
            }
        }
    }

    public class BasicEntity : Entity
    {
        public BasicEntity(int x, int y, int layer = 0, AtlasImage graphics = null) : base(x, y, graphics)
        {
            Layer = layer;
        }

        public override void Update(GameInput input)
        {
            base.Update(input);
            switch (input.Key)
            {
                case ConsoleKey.W:
                    Y--;
                    break;
                case ConsoleKey.S:
                    Y++;
                    break;
                case ConsoleKey.A:
                    X--;
                    break;
                case ConsoleKey.D:
                    X++;
                    break;
                case ConsoleKey.K:
                    {
                        var atlas = (Graphics as AtlasImage);
                        if (atlas.IsRunning)
                        {
                            atlas.Stop();
                        }
                        else
                        {
                            atlas.Start();
                        }
                        break;
                    }

            }
        }
    }
}