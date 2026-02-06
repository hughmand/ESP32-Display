namespace ESP32Display
{
    public class PauseScreen : Screen
    {
        public PauseScreen()
        {
            var pauseText = Elements.AddElement("pauseText", new TextElement("Pause"));
            var exitCross = Elements.AddElement("exitCross", new SymbolElement(StaticSymbol.Cross));
            var pauseButton = Elements.AddElement("pauseButton", new SymbolElement(StaticSymbol.Pause));

            Elements.SetElementPosition(pauseText, 0, 5);
            Elements.SetElementPosition(exitCross, 5, 0);
            Elements.SetElementPosition(pauseText, 0, 0);
        }
    }
}
