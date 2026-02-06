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

        public MainWorker(DisplayManager displayManager, SystemState systemState, IPulseOutput buzzer, IInputCollection inputCollection) : base(displayManager, systemState, null, buzzer, inputCollection)
        {
            _menuItems = new MenuItem[2];
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
                Worker = new SnakeWorker(_displayManager, systemState, this, _buzzer, _inputCollection)
            };
        }

        public override void Run()
        {
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
        }

        private void ActivateMenuItem()
        {
            var item = _menuItems[_currentItemIndex];
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

        protected override void Exit()
        {
            _inputCollection.UnsubscribeAll();
            return;
        }
    }
}
