namespace ESP32Display
{
    class SnakeElement : Element, IStateElement
    {
        Snake _snake; 
        Orchard _orchard;

        public SnakeElement(SnakeGame snakeGame)
        {
            _snake = snakeGame.Snake;
            _orchard = snakeGame.Orchard;
            Pixels = new bool[32][];
            for (int i = 0; i < 32; i++)
            {
                Pixels[i] = new bool[8];
            }
            RedrawFromState();
        }

        public void RedrawFromState()
        {
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Pixels[i][j] = false;
                }
            }

            if (_snake.Tail is not null && _snake.Tail.Length > 0)
            {
                for (int i = 0; i < _snake.Tail.Length; i++)
                {
                    var point = _snake.Tail[i];        
                    Pixels[point.X][point.Y] = true;
                }
            }
            
            Pixels[_snake.Head.X][_snake.Head.Y] = true;
            
            for (int i = 0; i < _orchard.Apples.Length; i++)
            {
                var point = _orchard.Apples[i];
                Pixels[point.X][point.Y] = true;
            }
        }
    }
}
