using ConsoleGameEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGameEngine.Engine.Models.Masks
{
    public class Grid : Hitbox
    {
        private bool[,] _collisionTable;
        private List<Point> _collisionPoints;
        private int _tileWidth;
        private int _tileHeight;

        public Grid(int width, int height, int tileWidth, int tileHeight) : base()
        {
            _collisionTable = new bool[width, height];
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _collisionPoints = new List<Point>();
            AddCollisionOption(typeof(PixelMask), CollideWithPixelMask);
            AddCollisionOption(typeof(Hitbox), CollideWithHitBox);
        }

        private bool CollideWithPixelMask(Mask arg)
        {
            foreach (var point in _collisionPoints)
            {
                bool collide = arg.Collide(point);
                if (collide)
                    return true;
            }
            return false;
        }
        protected override bool CollideWithPoint(Point point)
        {
            return _collisionPoints.Contains(point);
        }

        protected override bool CollideWithHitBox(Mask mask)
        {
            Hitbox hitbox = mask as Hitbox;
            int x = hitbox.Parent.X + hitbox.Parent.MaskX;
            int y = hitbox.Parent.Y + hitbox.Parent.MaskY;
            int width = hitbox.Parent.MaskWidth;
            int height = hitbox.Parent.MaskHeight;
            return GetTilesFromRect(x, y, width, height).All(p => _collisionTable[p.Y, p.X]);
        }

        /// <summary>
        /// 1 for solid
        /// </summary>
        public void LoadFromString(string grid, string rowSeperator = "\n", string colSeperator = ",")
        {
            string[] rows = grid.Split(new[] { rowSeperator }, StringSplitOptions.RemoveEmptyEntries);
            for (int row = 0; row < rows.Length; row++)
            {
                string[] cols = rows[row].Split(new[] { colSeperator }, StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < cols.Length; col++)
                {
                    bool solid = int.Parse(cols[col]) != 0 ? true : false;
                    SetTile(col, row, solid);
                }
            }
        }

        public void SetTile(int col, int row, bool solid = true)
        {
            if (solid)
            {
                List<Point> points = GetPointsFromTile(row, col);
                if (_collisionTable[row, col])
                    _collisionPoints.RemoveAll(p => points.Contains(p));
                else
                    _collisionPoints.AddRange(points);
            }

            _collisionTable[row, col] = solid;
        }

        public bool GetTile(int col, int row)
        {
            return _collisionTable[row, col];
        }

        public void ClearTile(int col, int row)
        {
            SetTile(col, row, false);
        }

        public void SetRect(int column, int row, int width, int height, bool solid = true)
        {
            for (int rowOff = 0; rowOff < height; rowOff++)
            {
                for (int colOff = 0; colOff < width; colOff++)
                {
                    SetTile(column + colOff, row + rowOff, solid);
                }
            }
        }
        public void ClearRect(int column, int row, int width, int height)
        {
            SetRect(column, row, width, height);
        }

        public bool GetTileFromPosition(int physicalX, int physicalY)
        {
            int col = physicalX / _tileWidth;
            int row = physicalY / _tileHeight;

            return GetTile(col, col);
        }

        private List<Point> GetTilesFromRect(int physicalX, int physicalY, int width, int height)
        {
            List<Point> tiles = new List<Point>();
            int startTileCol = physicalX / _tileWidth;
            int startTileRow = physicalY / _tileHeight;
            int endTileCol = (physicalX + width) / _tileWidth;
            int endTileRow = (physicalY + height) / _tileHeight;
            for (; startTileRow <= endTileRow; startTileRow++)
                for (; startTileCol <= endTileCol; startTileCol++)
                    tiles.Add(new Point(startTileCol, startTileRow));

            return tiles;
        }

        private List<Point> GetPointsFromTile(int row, int col)
        {
            List<Point> points = new List<Point>();
            int startX = col * _tileWidth;
            int startY = row * _tileHeight;
            for (int y = 0; y < _tileHeight; y++)
            {
                for (int x = 0; x < _tileWidth; x++)
                {
                    points.Add(new Point(startX + x, startY + y));
                }
            }
            return points;
        }
    }
}
