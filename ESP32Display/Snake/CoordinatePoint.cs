namespace ESP32Display
{
    public struct CoordinatePoint
    {
        public CoordinatePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;

        //This avoids overring object.Equals, don't want to provide GetHashCode (extra work, not necessary here)
        public bool Equals(CoordinatePoint other)
        {
            bool equals = true;
            equals &= other.X == X;
            equals &= other.Y == Y;
            return equals;
        }
    }
}
