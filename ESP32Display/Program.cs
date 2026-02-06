using System;
using System.Threading;

namespace ESP32Display
{
    public class Program
    {
        public static void Main()
        {
            IPulseOutput buzzer = new PulseOutput(Configuration.BuzzerPinNumber, 200);
            IPulseOutput onBoardLED = new PulseOutput(Configuration.OnBoardLEDPinNumber, 200);
            onBoardLED.Pulse(3, 100);

            var systemState = new SystemState();

            WirelessController wirelessController = new WirelessController();

            IInputCollection inputCollection = new InputCollection(onBoardLED, Configuration.ButtonPinNumbers);
            
            DisplayManager displayManager = new DisplayManager(Configuration.DisplayDataPinNumber, Configuration.DisplayCSPinNumber, Configuration.DisplayCLKPinNumber);
            Worker mainWorker = new MainWorker(displayManager, systemState, buzzer, inputCollection);

            displayManager.StartThread();
            mainWorker.Run();
        }
    }
}
