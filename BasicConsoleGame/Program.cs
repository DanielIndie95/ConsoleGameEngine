using ConsoleGameEngine.Engine;
using System;
using ConsoleGameEngine.Engine.Models;
using ConsoleGameEngine.Engine.Models.GraphicsModels;
using ConsoleGameEngine.Engine.Models.Entities;
using System.IO;

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

        public SpecialGameEngine() : base(30, 20, 50)
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
            string text = "hello";
            string frame1 = @" :)\n=|=\n| |";
            string frame2 = @" :(\n^|^\n| |";
            string frame3 = @" :|\n)|(\n| -";
            string[] anim = {
              frame1 , frame2 , frame3
            };

            /*AtlasImage atlas = new AtlasImage(anim, 3, ConsoleColor.White, false);
            Add(new BasicEntity(4, 6, 1, atlas));
            Add(new BorderEntity(GameEngine.Engine.Width - 8, GameEngine.Engine.Height, ConsoleColor.White)
            {
                Layer = 3
            });
            Add(new Text(text, 1, ConsoleColor.Red), GameEngine.World.Layers, 24, 3);*/
            
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
            SetHitBoxToGrahpics();
        }

        public override void Update(GameInput input)
        {
            base.Update(input);
            int nextX = X, nextY = Y;
            switch (input.Key)
            {
                case ConsoleKey.W:
                    nextY = Y - 1;
                    break;
                case ConsoleKey.S:
                    nextY = Y + 1;
                    break;
                case ConsoleKey.A:
                    nextX = X - 1;
                    break;
                case ConsoleKey.D:
                    nextX = X + 1;
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
            if (!Collided(nextX, nextY))
            {
                X = nextX;
                Y = nextY;
            }
        }
    }
}