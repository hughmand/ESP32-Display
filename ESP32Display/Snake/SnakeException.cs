using System;

namespace ESP32Display
{
    /// <summary>
    /// Thrown when an element is in an invalid state or is setup incorrectly
    /// </summary>
    public class SnakeException : Exception
    {
        public SnakeException()
        {
        }

        public SnakeException(string message) : base(message)
        {
        }

        public SnakeException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
