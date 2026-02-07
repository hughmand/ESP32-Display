using System.Threading;

namespace ESP32Display
{
    public class MainWorker : Worker
    {
        MenuItem[] _menuItems;
        MenuScreen _menuScreen = new MenuScreen();
        int _currentItemIndex;

        private class MenuItem
        {
            public Worker Worker;
            public Screen Screen;
            public string Text;
        }

        public MainWorker(WirelessController wirelessController, DisplayManager displayManager, IPulseOutput buzzer, IInputCollection inputCollection) : base(displayManager, null, buzzer, inputCollection)
        {
            _menuItems = new MenuItem[3];
            _menuItems[0] = new MenuItem
            {
                Text = "Clock",
                Worker = null,
                Screen = new ClockScreen()
            };

            _menuItems[1] = new MenuItem
            {
                Text = "Snake",
                Screen = null,
                Worker = new SnakeWorker(_displayManager, this, _buzzer, _inputCollection)
            };
            _menuItems[2] = new MenuItem
            {
                Text = "River",
                Screen = null,
                Worker = new RiverWorker(wirelessController, _displayManager, this, _buzzer, _inputCollection)
            };
            secondRun = false;
        }
        bool secondRun;

        public override void Run()
        {
            Console.WriteLine("Main Menu worker started");

            _inputCollection.UnsubscribeAll();
            _displayManager.Screen = _menuScreen;
            ChangeMenuItem(_currentItemIndex);
            _inputCollection.Subscribe(InputLabel.Enter, ActivateMenuItem);
            _inputCollection.Subscribe(InputLabel.Up, () => ChangeMenuItem(++_currentItemIndex));
            _inputCollection.Subscribe(InputLabel.Down, () => ChangeMenuItem(--_currentItemIndex));
        }

        private void ChangeMenuItem(int index)
        {
            if (index >= _menuItems.Length) index = 0;
            if (index < 0) index = _menuItems.Length-1;
            var item = _menuItems[index];
            _menuScreen.SetText(item.Text);
            _currentItemIndex = index;
            Console.WriteLine("Menu item changed: " + item.Text);
        }

        private void ActivateMenuItem()
        {
            var item = _menuItems[_currentItemIndex];
            Console.WriteLine("Menu item selected: " + item.Text);
            //Depending on what the item is, it may require a further worker - if not, just set the screen.
            if (item.Worker is not null)
            {
                _inputCollection.UnsubscribeAll();
                item.Worker.Run();
            } 
            else
            {
                _inputCollection.UnsubscribeAll();
                _displayManager.Screen = item.Screen;
                _inputCollection.Subscribe(InputLabel.Enter, Run);
            }
        }
    }
}
