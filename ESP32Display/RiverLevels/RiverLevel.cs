using System;

namespace ESP32Display
{
    public class RiverLevel
    {
        public RiverIdentifier Identifier;
        public RiverLevelReading[] Readings;
    }

    public class RiverLevelReading
    {
        public DateTime ReadingDateTime;
        public float Reading;
    }
}
