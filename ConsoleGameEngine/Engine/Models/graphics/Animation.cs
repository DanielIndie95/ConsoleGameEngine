using ConsoleGameEngine.Screen.Models;
using System.Threading;

namespace ConsoleGameEngine.Engine.Models.graphics
{
    public abstract class Animation : Graphics
    {
        public bool IsRunning { get; private set; }

        private PixelData[][,] _frames;
        private int _frame;
        private int _fps;
        private Timer _timer;

        internal Animation(PixelData[][,] frames, int fps, bool start) : base(frames[0])
        {
            _frames = frames;
            _frame = 0;
            _fps = fps;
            if (start)
            {
                Start();
            }
        }
        internal Animation(int fps, bool start) : base()
        {
            _frame = 0;
            _fps = fps;
            if (start)
            {
                Start();
            }
        }

        public void Start(bool restart = false)
        {
            _timer = new Timer(FrameCycle, null, 0, 1000 / _fps);
            IsRunning = true;
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, _fps);
            IsRunning = false;
        }

        private void FrameCycle(object data)
        {
            _frame = (_frame + 1) % _frames.Length;
            var nextFrame = _frames[_frame];
            GraphicData = nextFrame;
        }

        protected void ChangeGraphics(PixelData[][,] graphics)
        {
            _frames = graphics;
            GraphicData = _frames[_frame];
        }
    }
}
