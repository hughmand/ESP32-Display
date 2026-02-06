using System;
using System.Text;

namespace ESP32Display
{
    public enum Intensity : byte
    {
        MinimumBrightness = 0,
        MaximumBrightness = 0xf,
    }

    enum DisplayRefreshPattern
    {
        TopToBottom, 
        BottomToTop,
        SplitInwards,
        SplitOutwards
    }

    /// <summary>
    /// Contains values used for configuration that cannot be changed during operation.
    /// Easier to have here than in a separate configuration file, as all need to be deployed to the flash memory together.
    /// </summary>
    static class Configuration
    {
        /// <summary>
        /// Changes the order in which the rows on the LED display update.
        /// </summary>
        public static DisplayRefreshPattern RefreshPattern = DisplayRefreshPattern.SplitOutwards;
        /// <summary>
        /// Only send update to the display for each row, if any pixels on the row have changed - reduces screen refresh time.
        /// </summary>
        public static bool RefreshOptimisation = true;
        /// <summary>
        /// Minimum time between attempting to refresh the screen, in milliseconds
        /// </summary>
        public static int RefreshDelay = 200; 

        public static byte Brightness = (byte)Intensity.MaximumBrightness;
        /// <summary>
        /// Default length of time in millisconds for pulse output classes
        /// </summary>
        public static int DefaultPulseLength = 50;

        //Assumed in order Up, Down, Left, Right, Enter
        public static int[] ButtonPinNumbers = new int[] { 25, 26, 14, 27, 33 };
        public static int BuzzerPinNumber = 4;
        public static int OnBoardLEDPinNumber = 2;
        public static int DisplayDataPinNumber = 19;
        public static int DisplayCLKPinNumber = 21;
        public static int DisplayCSPinNumber = 18;

        public static bool TwentyFourHourClockFormat = true;
    }
}
