using System;

namespace ESP32Display
{
    /// <summary>
    /// Thrown when an element is in an invalid state or is setup incorrectly
    /// </summary>
    public class ElementException : Exception
    {
        public ElementException()
        {
        }

        public ElementException(string message) : base(message)
        {
        }

        public ElementException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
