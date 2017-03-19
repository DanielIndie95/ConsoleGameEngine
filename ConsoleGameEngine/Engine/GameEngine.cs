using ConsoleGameEngine.Engine.Models;
using System;
using System.Diagnostics;
using System.Threading;
using ConsoleGameEngine.Screen;

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
        public TimeSpan Elapsed { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fps"></param>
        /// <param name="blackAndWhiteMode">black and white is much faster</param>
        public GameEngine(int width, int height, int fps, bool blackAndWhiteMode = true)
        {
            Screen = new BasicConsoleScreen(width, height, blackAndWhiteMode);
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
            Elapsed = TimeSpan.FromMilliseconds(1000 / Fps); // start is the expected fps;
            Stopwatch stopper = new Stopwatch();
            while (!_exit)
            {
                stopper.Start();
                if (World != null)
                    Cycle();
                Thread.Sleep(1000 / Fps);
                Elapsed = stopper.Elapsed;
                stopper.Restart();
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
