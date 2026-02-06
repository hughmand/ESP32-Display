namespace ESP32Display
{
    public class NewGameScreen : Screen
    {
        public NewGameScreen()
        {
            var snakeText = Elements.AddElement("snakeText", new TextElement("SNAKE"));
            var exitCross = Elements.AddElement("exitCross", new SymbolElement(StaticSymbol.Cross));
            var playButton = Elements.AddElement("playButton", new SymbolElement(StaticSymbol.RightArrow));

            Elements.SetElementPosition(snakeText, 0, 5);
            Elements.SetElementPosition(exitCross, 5, 0);
            Elements.SetElementPosition(playButton, 0, 0);
        }
    }
}
