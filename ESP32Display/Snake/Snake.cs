using System;

namespace ESP32Display
{
    public class Snake
    {
        SnakeSettings _settings;
        SnakeDirection _direction;
        /// <summary>
        /// This includes the head
        /// </summary>
        public int Length; 
        public int TailLength => Length - 1;
        public CoordinatePoint Head;
        public CoordinatePoint[] Tail;
        private bool _growing; //Simple lock to prevent Grow method being called twice in a single frame.
        private bool _changingDirection; //Simple lock to prevent direction change method being called twice in a single frame.

        public Snake(SnakeSettings settings)
        {
            _settings = settings;
            _direction = SnakeDirection.Right;
            _changingDirection = false;
            Tail = new CoordinatePoint[0];
            Head = new CoordinatePoint(0, 0);
            Length = 1;
        }

        public void Grow()
        {
            if (_growing) throw new SnakeException("Cannot call grow method twice in a single frame");
            Length++;
            _growing = true;
        }

        public void SetDirection(SnakeDirection direction)
        {
            if (!_changingDirection && (int)direction % 2 != (int)_direction % 2) //Prevents the same and opposite direction being set
            {
                _direction = direction;
                _changingDirection = true;
            }
        }

        public void Progress()
        {
            var newSnakeHead = GetNewSnakeHead();
            if (TailLength > 0)
            {
                var newTail = new CoordinatePoint[TailLength]; //Annoying to allocate a new array every time, but not really a performance-limiting factor here
                if (Tail is not null && Tail.Length > 0)
                {
                    for (int i = 1; i < TailLength; i++)
                    {
                        newTail[i] = Tail[i - 1];
                    }
                }
                newTail[0] = Head;
                Tail = newTail;
            }
            Head = newSnakeHead;

            _growing = false;
            _changingDirection = false;
        }

        public bool CheckForIntersection()
        {
            //Not performant, but doesn't matter - snake never gets long enough for this to be a limiting factor
            for (int i = 0; i < Tail.Length; i++)
            {
                var tailPoint = Tail[i];
                if (tailPoint.Equals(Head)) return true;
                for (int j = 0; j < Tail.Length; j++)
                {
                    if (i == j) continue;
                    if (tailPoint.Equals(Tail[j])) return true;
                }
            }
            return false;
        }

        private Velocity GetVelocity()
        {
            switch (_direction)
            {
                case SnakeDirection.Up:
                    return new Velocity { X = 0, Y = 1 };
                case SnakeDirection.Down:
                    return new Velocity { X = 0, Y = -1 };
                case SnakeDirection.Left:
                    return new Velocity { X = -1, Y = 0 };
                case SnakeDirection.Right:
                    return new Velocity { X = 1, Y = 0 };
            }
            throw new SnakeException("Unknown snake direction");
        }

        private CoordinatePoint GetNewSnakeHead()
        {
            var velocity = GetVelocity();
            var newHead = Head;
            newHead.X += velocity.X;
            newHead.Y += velocity.Y;

            //Now need to wrap around if we've gone off the edge of the screen
            if (newHead.X < 0) newHead.X = _settings.Width - 1;
            if (newHead.Y < 0) newHead.Y = _settings.Height - 1;
            if (newHead.X >= _settings.Width) newHead.X = 0;
            if (newHead.Y >= _settings.Height) newHead.Y = 0;
            return newHead;
        }

        private class Velocity
        {
            public int X;
            public int Y;
        }
    }

    public enum SnakeDirection
    {
        Right = 0, Up = 1, Left = 2, Down = 3
    }
}
