using System;

namespace ConsoleGameEngine.Engine.Models.graphics
{
    public class Text : Graphics
    {
        public Text(string text, int lineWidth, ConsoleColor color)
        {
            string[] lines = Utils.SplitString(text, lineWidth).ToArray();
            var pixels = Utils.GetPixelsBufferFromCharsBuffer(Utils.GetCharArrayFromStrings(lines), color);
            GraphicData = pixels;
        }
    }
}
