using System;

namespace ESP32Display
{
    public enum Intensity : byte
    {
        MinimumBrightness = 0,
        MaximumBrightness = 0xf,
    }

    public enum DisplayRefreshPattern
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
    public static class Configuration
    {
        public static bool DebugConsole = true;
        public static string LoggingTimeFormat = "[HH:mm:ss]";
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

        public static string WifiSsid = "";
        public static string WifiPassword = "";

    }

    public struct RiverIdentifier
    {
        public string ShortName;
        public string Name;
        public string Guid;
    }

    public static class RiverLevelAPIConfiguration
    {
        public static string BaseService = "https://environment.data.gov.uk/hydrology/id/measures";
        public static string ReadingsSuffix = "readings";
        public static string TimeSeriesSuffix = "level-i-900-m-qualified"; //TODO: extend to support more time series, variable by river

        public static RiverIdentifier[] Rivers = new RiverIdentifier[] {
            new RiverIdentifier
            {
                ShortName = "Dart",
                Name = "River Dart",
                Guid = "e6f51f9c-14c9-4ee4-8bbe-9a934236492a"
            }
        };
    }
}
