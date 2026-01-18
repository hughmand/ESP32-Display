namespace ESP32Display
{
    public class MainWorker : Worker
    {
        HomeScreen _homeScreen = new HomeScreen();
        ClockScreen _clockScreen = new ClockScreen();

        public MainWorker(IPulseOutput buzzer, IWirelessController wirelessController, IInputCollection inputCollection, DisplayState displayState, SystemState systemState) : base(buzzer, inputCollection, displayState, systemState)
        {  
        }

        public override void Run()
        {
            //TODO: Main functionality:
            //Snake game
            //Variable brightness
            //Wifi control
            //Input symbols and control scheme configuration
            Start();
        }

        private void Start()
        {
            _homeScreen.SetText("Clock");
            _displayState.Screen = _homeScreen;
            _inputCollection.Subscribe(InputLabel.Enter, ClockScreen);
        }

        private void HomeScreen()
        {
            _inputCollection.Unsubscribe(InputLabel.Enter, HomeScreen);
            _displayState.Screen = _homeScreen;
            _homeScreen.SetText("Clock");
            _inputCollection.Subscribe(InputLabel.Enter, ClockScreen);
        }

        private void ClockScreen()
        {
            _displayState.Screen = _clockScreen;
            _inputCollection.Unsubscribe(InputLabel.Enter, ClockScreen);
            _inputCollection.Subscribe(InputLabel.Enter, HomeScreen);
        }
    }
}
