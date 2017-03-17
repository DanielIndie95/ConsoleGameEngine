using ConsoleGameEngine.Screen.Models;
using System;

namespace ConsoleGameEngine.Engine.Models
{
    public class Graphics
    {
        public bool Visible { get; set; } = true;
        public int Width => GraphicData.GetLength(1);
        public int Height => GraphicData.GetLength(0);

        protected PixelData[,] GraphicData;
        
        public Graphics()
        {

        }

        public Graphics(PixelData[,] graphics)
        {
            GraphicData = graphics;
        }

        internal void Draw(IDrawableScreen targetRender , int offsetX, int offsetY)
        {
            targetRender.SetRectangle(offsetX, offsetY, GraphicData);
        }
    }
}