using System;

namespace ESP32Display
{
    public class ClockElement : Element, IStateElement
    {
        protected CharState[] _hours = new CharState[2];
        protected CharState[] _minutes = new CharState[2];
        protected CharState[] _seconds = new CharState[2];

        bool _twentyFourHourTime;
        public ClockElement(bool twentyFourHourTime)
        {
            for (int i = 0; i < 2; i++)
            {
                _hours[i] = new CharState();
                _minutes[i] = new CharState();
                _seconds[i] = new CharState();
            }
            _twentyFourHourTime = twentyFourHourTime;
        }

        public void UpdateFromState()
        {
            var hourString = DateTime.UtcNow.ToString(_twentyFourHourTime ? "HH" : "hh");
            var minuteString = DateTime.UtcNow.ToString("mm");
            var secondString = DateTime.UtcNow.ToString("ss");

            for (int i = 0; i < 2; i++)
            {
                _hours[i].SetValue(hourString[i]);
                _minutes[i].SetValue(minuteString[i]);
                _seconds[i].SetValue(secondString[i]);
            }
        }
    }
}
