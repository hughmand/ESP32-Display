using System.Device.Gpio;

namespace ESP32Display
{
    /// <summary>
    /// Controls the hardware interfacing of the LED display, and accessing the display's shift register
    /// </summary>
    public class DisplayHardwareController
    {
        readonly GpioPin _csPin;
        readonly GpioPin _clkPin;
        readonly GpioPin _dataPin;

        public DisplayHardwareController(int dataPinNumber, int csPinNumber, int clkPinNumber)
        {
            GpioController controller = new GpioController();
            _csPin = controller.OpenPin(csPinNumber, PinMode.Output);
            _clkPin = controller.OpenPin(clkPinNumber, PinMode.Output);
            _dataPin = controller.OpenPin(dataPinNumber, PinMode.Output);
            _csPin.Write(PinValue.High); //Closes communication with display
        }
        
        public void SendCommand(byte[] command)
        {
            //Clock pin signals a new bit is being sent
            _clkPin.Write(PinValue.Low);
            //cs pin opens communcation to the display
            _csPin.Write(PinValue.Low);
            foreach (byte b in command)
            {
                for (byte i = 0; i < 8; i++)
                {
                    byte result = (byte)((b << i) & 128);
                    if (result != 0)
                    {
                        _dataPin.Write(PinValue.High);
                    } 
                    else
                    {
                        _dataPin.Write(PinValue.Low);
                    }
                    //Signals a the bit value has been sent
                    _clkPin.Write(PinValue.High);
                    _clkPin.Write(PinValue.Low);
                }
            }
            //ends communication to the display
            _csPin.Write(PinValue.High);
        }
    }
}
