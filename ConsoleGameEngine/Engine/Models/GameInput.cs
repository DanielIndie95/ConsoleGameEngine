using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleGameEngine.Engine.Models
{
    public class GameInput
    {
        private ConsoleKeyInfo InputInfo { get; }
        private static readonly Dictionary<string, ConsoleKey[]> Definitions;

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
            Definitions = new Dictionary<string, ConsoleKey[]>();
        }

        public bool Check(string key)
        {
            bool containsDefinition = Definitions.TryGetValue(key, out ConsoleKey[] options);
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
            Definitions.Add(key, inputs);
        }
    }
}
