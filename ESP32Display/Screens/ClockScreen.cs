using System;
using System.Device.Gpio;
using System.Text;

namespace ESP32Display
{
    public class ClockScreen : Screen
    {
        public ClockScreen()
        {
            var clock = Elements.AddElement("clock", new LargeClockElement(Configuration.TwentyFourHourClockFormat));
            
            Elements.SetElementPosition(clock, 0, 0);
        }
    }
}
