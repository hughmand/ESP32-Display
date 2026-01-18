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

            var displayState = new DisplayState();
            var systemState = new SystemState();

            WirelessController wirelessController = new WirelessController();
            //wirelessController.Connect();

            IInputCollection inputCollection = new InputCollection(onBoardLED, Configuration.ButtonPinNumbers);
            
            Worker mainWorker = new MainWorker(buzzer, wirelessController, inputCollection, displayState, systemState);

            StartDisplayThread(displayState);
            mainWorker.Run();
        }

        public static void StartDisplayThread(DisplayState displayState)
        {
            TimeSpan refreshDelay = TimeSpan.FromMilliseconds(Configuration.RefreshDelay);
            DisplaySoftwareController displaySoftwareController = new DisplaySoftwareController(
                   dataPinNumber: Configuration.DisplayDataPinNumber,
                   csPinNumber: Configuration.DisplayCSPinNumber,
                   clkPinNumber: Configuration.DisplayCLKPinNumber,
                   displayState);

            displaySoftwareController.ConfigureHardware();
            Thread displayThread = new Thread(() =>
            {
                DateTime lastUpdate = DateTime.UtcNow;
                while (true)
                {
                    //Makes the device feel more responsive, because this thread is heavily limited by the rate at which the display can physically update, not how often these methods run.
                    bool refreshDelayPassed = DateTime.UtcNow - lastUpdate > refreshDelay;

                    if (!refreshDelayPassed) Thread.Sleep(refreshDelay);
                    else if (displayState.Screen is not null)
                    {
                        displayState.Screen.NewFrame();
                        displaySoftwareController.SendToDisplay();
                        lastUpdate = DateTime.UtcNow;
                    } 
                }
            });
            displayThread.Start();
        }
    }
}
