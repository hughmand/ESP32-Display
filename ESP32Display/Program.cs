using System;
using System.Threading;

namespace ESP32Display
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                Console.WriteLine("Starting...");
                Console.WriteLine("Setting up output");
                IPulseOutput buzzer = new PulseOutput(Configuration.BuzzerPinNumber, 200);
                IPulseOutput onBoardLED = new PulseOutput(Configuration.OnBoardLEDPinNumber, 200);

                onBoardLED.Pulse(3, 100);

                Console.WriteLine("Setting up wireless");
                WirelessController wirelessController = new WirelessController();
                wirelessController.TryConnect();

                Console.WriteLine("Setting up input");
                IInputCollection inputCollection = new InputCollection(onBoardLED, Configuration.ButtonPinNumbers);

                Console.WriteLine("Setting up display");
                DisplayManager displayManager = new DisplayManager(Configuration.DisplayDataPinNumber, Configuration.DisplayCSPinNumber, Configuration.DisplayCLKPinNumber);

                Console.WriteLine("Setting up worker");
                Worker mainWorker = new MainWorker(wirelessController, displayManager, buzzer, inputCollection);

                Console.WriteLine("Startup completed!");
                Console.WriteLine("-----------------");

                displayManager.StartThread();
                mainWorker.Run();
            }
            catch (Exception e) 
            {
                Console.WriteException(e);
            }
            
        }
    }
}
