using System.Device.Gpio;
using System.Threading;

namespace ESP32Display
{ 
    /// <summary>
    /// Exposes methods for outputting pulses on single pin outputs
    /// </summary>
    public interface IPulseOutput
    {
        void Pulse(int numberOfBeeps = 1, int beepLength = 0);
    }

    /// <inheritdoc/>
    public class PulseOutput : IPulseOutput
    {
        GpioPin _pin;
        int _defaultBeepLength;

        public PulseOutput(int pinNumber, int defaultBeepLength)
        {
            _pin = new GpioController().OpenPin(pinNumber, PinMode.Output);
            _defaultBeepLength = defaultBeepLength;
        }

        public void Pulse(int numberOfBeeps = 1, int beepLength = 0)
        {
            //TODO: Rewrite, maybe start the pulse on the calling thread and release seperately.
            int selectedBeepLength = beepLength == 0 ? _defaultBeepLength : beepLength;
            Thread beepThread = new Thread(() => {
                for (int i = 0; i < numberOfBeeps; i++)
                {
                    BeepOnce();
                    if (i != numberOfBeeps-1) Thread.Sleep(selectedBeepLength);
                }
            });
            beepThread.Start();
            
            void BeepOnce()
            {
                _pin.Write(PinValue.High);
                Thread.Sleep(selectedBeepLength);
                _pin.Write(PinValue.Low);
            }
        }
    }
}
