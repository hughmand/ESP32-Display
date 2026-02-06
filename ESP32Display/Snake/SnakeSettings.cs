using System;

namespace ESP32Display
{
    public class SnakeSettings
    {
        public int Width = 32;
        public int Height = 8;

        public TimeSpan _timeBetweenAppleDrops = TimeSpan.FromSeconds(5);
    }
}
