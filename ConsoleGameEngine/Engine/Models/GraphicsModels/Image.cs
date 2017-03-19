using System;
using System.Drawing;
using ConsoleGameEngine.Screen.Models;
using System.Drawing.Drawing2D;

namespace ConsoleGameEngine.Engine.Models.GraphicsModels
{
    public class Image : Graphics
    {
        public Image(string file, int scale = 1)
        {
            Bitmap map = new Bitmap(file);
            int scaledWidth = map.Width / scale;
            int scaledHeight = map.Height / scale;
            Bitmap scaled = new Bitmap(scaledWidth, scaledHeight);
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(scaled))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(map, 0, 0, scaledWidth, scaledHeight);
                GraphicData = ConvertBitmapToGrahpics(scaled);
            }
        }

        private PixelData[,] ConvertBitmapToGrahpics(Bitmap map)
        {

            PixelData[,] data = new PixelData[map.Height, map.Width];
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    Color color = map.GetPixel(x, y);
                    data[y, x] = new PixelData(' ', ClosestConsoleColor(color), false);
                }
            }
            return data;
        }

        /*private ConsoleColor ClosestConsoleColor(byte r, byte g, byte b)
        {
            ConsoleColor ret = 0;
            double rr = r, gg = g, bb = b, delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var n = Enum.GetName(typeof(ConsoleColor), cc);
                var c = System.Drawing.Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
                var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
                if (t == 0.0)
                    return cc;
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }*/

        private ConsoleColor ClosestConsoleColor(Color color)
        {
            int index = (color.R > 128 | color.G > 128 | color.B > 128) ? 8 : 0; // Bright bit
            index |= (color.R > 64) ? 4 : 0; // Red bit
            index |= (color.G > 64) ? 2 : 0; // Green bit
            index |= (color.B > 64) ? 1 : 0; // Blue bit
            return (ConsoleColor)index;
        }

    }
}
