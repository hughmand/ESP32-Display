namespace ESP32Display
{
    /// <summary>
    /// Holds the current state of the display, Pixels, brightness etc.
    /// There is no need for locking on the state objects, because there is only one worker thread - the display thread will never write to this object, only read.
    /// </summary>
    public class DisplayState
    {
        public Screen Screen
        {
            get => _screen;
            set
            {
                if (Screen is not null) LastScreen = Screen;
                _screen = value;
            }
        }
        private Screen _screen;
        public Screen LastScreen { get; private set; }
        public byte Brightness;
    }
}
