using ConsoleGameEngine.Engine.Models;
using System;
using System.Threading;

namespace ConsoleGameEngine.Engine
{
    public class GameEngine : IUpdateable, IDrawable
    {
        private static GameWorld _world;
        public static GameWorld World
        {
            get
            {
                return _world;
            }
            set
            {
                _world = value;
                _world.Begin();
            }
        }
        public static GameEngine Engine;
        public static IDrawableScreen Screen { get; set; }

        private bool _exit;

        protected int Fps
        {
            get; set;
        }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public GameEngine(int width, int height, int fps)
        {
            Screen = new BasicConsoleScreen(width, height);
            Width = width;
            Height = height;

            Engine = this;
            Fps = fps;
            StartGame();
        }

        public void Exit()
        {
            _exit = true;
        }

        private void StartGame()
        {
            _exit = false;
            Begin();
            while (!_exit)
            {
                if (World != null)
                    Cycle();
                Thread.Sleep(1000 / Fps);
            }
        }
        public virtual void Begin()
        {

        }

        private void Cycle()
        {
            GameInput input = GetInput();
            Update(input);
            Draw();
            Thread.Sleep(1000 / Fps);
        }
        private static GameInput GetInput()
        {
            ConsoleKeyInfo key;
            bool hadKey = Console.KeyAvailable;
            while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
            }
            
            GameInput input = hadKey ? new GameInput(key) : GameInput.None;
            return input;
        }

        public void Update(GameInput input)
        {
            World.Update(input);
        }

        public void Draw()
        {
            Screen.ClearScreen();

            World.Draw();

            Screen.Draw();

        }
    }
}
