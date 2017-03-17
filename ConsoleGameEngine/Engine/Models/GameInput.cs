using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGameEngine.Engine.Models
{
    public class GameInput
    {
        private ConsoleKeyInfo InputInfo { get; }
        public ConsoleKey Key => InputInfo.Key;
        public ConsoleModifiers Modifiers => InputInfo.Modifiers;

        public static GameInput None => new GameInput();

        public GameInput(ConsoleKeyInfo info)
        {
            InputInfo = info;
        }
        public GameInput()
        {
        }
    }
}
