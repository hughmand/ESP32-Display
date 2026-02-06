using System;

namespace ESP32Display
{
    class SnakeScreen : Screen
    {
        //Meant to be this empty - not really needed for the game, but fits the architecture
        public SnakeScreen(SnakeGame game)
        {
            var snake = Elements.AddElement("snake", new SnakeElement(game));

            Elements.SetElementPosition(snake, 0, 0);
        }
    }
}
