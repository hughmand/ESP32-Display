namespace ESP32Display
{
    public class RiverWorker : Worker
    {
        RiverLevelDataProvider _riverLevelDataProvider;

        public RiverWorker(WirelessController wirelessController, DisplayManager displayManager, Worker parentWorker, IPulseOutput buzzer, IInputCollection inputCollection) : base(displayManager, parentWorker, buzzer, inputCollection)
        {
            _riverLevelDataProvider = new RiverLevelDataProvider(wirelessController);
        }

        public override void Run()
        {
            Console.WriteLine("River Levels worker started");
            _riverLevelDataProvider.GetRiverLevel(RiverLevelAPIConfiguration.Rivers[0]);
            Exit();
        }
    }
}
