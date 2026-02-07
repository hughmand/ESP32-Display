namespace ESP32Display
{
    public class SnakeGame
    {
        public Orchard Orchard { get; private set;  }
        public Snake Snake { get; private set; }
        public SnakeSettings Settings { get; private set; }

        public bool GameOver;

        public SnakeGame()
        {
            Settings = new SnakeSettings();
            Orchard = new Orchard(Settings);
            Snake = new Snake(Settings);
            GameOver = false;
        }

        public void TryEatApple()
        {
            if (Orchard.AppleConsumed(Snake.Head))
            {
                Console.WriteLine("Apple consumed");
                Snake.Grow();
            }
        }

        public void NextState()
        {
            Console.WriteLine("Game moving into next state");
            Snake.Progress();
            TryEatApple();
            Orchard.TryDropApple();
            GameOver = Snake.CheckForIntersection();
        }

        public void MoveUp()
        {
            Snake.SetDirection(SnakeDirection.Up);
        }

        public void MoveDown()
        {
            Snake.SetDirection(SnakeDirection.Down);
        }

        public void MoveLeft()
        {
            Snake.SetDirection(SnakeDirection.Left);
        }

        public void MoveRight()
        {
            Snake.SetDirection(SnakeDirection.Right);
        }
    }
}
