using ConsoleGameEngine.Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGameEngine.Engine
{
    public interface IUpdateable
    {
        void Update(GameInput input);
    }
}
