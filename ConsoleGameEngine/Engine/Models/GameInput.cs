using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGameEngine.Engine.Models
{
    public class GameInput
    {
        private ConsoleKeyInfo InputInfo { get; }
        private static Dictionary<string, ConsoleKey[]> _definitions;

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
        static GameInput()
        {
            _definitions = new Dictionary<string, ConsoleKey[]>();
        }

        public bool Check(string key)
        {
            bool containsDefinition = _definitions.TryGetValue(key, out ConsoleKey[] options);
            if (!containsDefinition)
                return false;
            return options.Contains(Key);
        }
        public bool Check(ConsoleKey key)
        {
            return key == Key;
        }

        public static void Define(string key, params ConsoleKey[] inputs)
        {
            _definitions.Add(key, inputs);
        }
    }
}
