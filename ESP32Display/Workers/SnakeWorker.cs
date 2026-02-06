namespace ESP32Display
{
    public class SnakeWorker : Worker
    {
        SnakeGame _game;
        SnakeScreen _snakeScreen;
        PauseScreen _pauseScreen;
        NewGameScreen _newGameScreen;

        public SnakeWorker(DisplayManager displayManager, SystemState systemState, Worker parentWorker, IPulseOutput buzzer, IInputCollection inputCollection) : base(displayManager, systemState, parentWorker, buzzer, inputCollection)
        {
            _game = new SnakeGame();

            _snakeScreen = new SnakeScreen(_game);
            _newGameScreen = new NewGameScreen();
            _pauseScreen = new PauseScreen();
        }

        public override void Run()
        {
            NewGame();
        }

        private void NewGame()
        {
            _displayManager.Screen = _newGameScreen;
            _game = new SnakeGame();
            _snakeScreen = new SnakeScreen(_game);
            _inputCollection.UnsubscribeAll();
            _inputCollection.Subscribe(InputLabel.Up, Play);
            _inputCollection.Subscribe(InputLabel.Down, Play);
            _inputCollection.Subscribe(InputLabel.Left, Play);
            _inputCollection.Subscribe(InputLabel.Right, Play);
            _inputCollection.Subscribe(InputLabel.Enter, Exit);
        }

        private void Play()
        {
            _inputCollection.UnsubscribeAll();
            _inputCollection.Subscribe(InputLabel.Up, _game.MoveUp);
            _inputCollection.Subscribe(InputLabel.Down, _game.MoveDown);
            _inputCollection.Subscribe(InputLabel.Left, _game.MoveLeft);
            _inputCollection.Subscribe(InputLabel.Right, _game.MoveRight);
            _inputCollection.Subscribe(InputLabel.Enter, Pause);

            //Game needs to be tied to the frames for it to feel playable (because framerate is slow). If the frame rate was higher, I would have a seperate game thread
            _displayManager.SetPreFrameTask(() => 
            {
                _game.NextState();
                if (_game.GameOver) NewGame();
            });
            _displayManager.Screen = _snakeScreen;
        }

        public void Pause()
        {
            _displayManager.ClearFrameTasks();
            _inputCollection.UnsubscribeAll();
            _displayManager.Screen = _pauseScreen;
            _inputCollection.Subscribe(InputLabel.Up, Play);
            _inputCollection.Subscribe(InputLabel.Down, Play);
            _inputCollection.Subscribe(InputLabel.Left, Play);
            _inputCollection.Subscribe(InputLabel.Right, Play);
            _inputCollection.Subscribe(InputLabel.Enter, NewGame);
        }

    }
}
