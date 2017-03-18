using System;
using System.IO;
using System.Text;

namespace ConsoleGameEngine.Engine.Models.GraphicsModels
{
    public class AsciiImage : Graphics
    {
        public AsciiImage(string file, ConsoleColor color = ConsoleColor.Gray)
        {
            string[] rows = File.ReadAllLines(file,Encoding.ASCII);
            char[,] buffer = Utils.GetCharArrayFromStrings(rows);
            GraphicData = Utils.GetPixelsBufferFromCharsBuffer(buffer, color);
        }
    }
}
