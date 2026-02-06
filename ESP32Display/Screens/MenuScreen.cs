using System.Diagnostics;

namespace ESP32Display
{
    public class MenuScreen : Screen
    {
        CharState[] _text;

        public MenuScreen()
        {
            _text = new CharState[8]; //Cannot be longer than 8 characters
            for (int i = 0; i < 8; i++)
            {
                _text[i] = new CharState();
            }
            var clock = Elements.AddElement("clock", new SmallClockElement(Configuration.TwentyFourHourClockFormat));
            var text = Elements.AddElement("text", new TextElement(_text));
            var upArrow = Elements.AddElement("upArrow", new SymbolElement(StaticSymbol.UpArrow));
            var downArrow = Elements.AddElement("downArrow", new SymbolElement(StaticSymbol.DownArrow));

            Elements.SetElementPosition(clock, 0, 5);
            Elements.SetElementPosition(text, 0, 0);
            Elements.SetElementPosition(upArrow, 28, 5);
            Elements.SetElementPosition(downArrow, 28, 1);
        }

        public void SetText(string text)
        {
            text = text.ToUpper();
            //Excess characters are just discarded
            for (int i = 0; i < _text.Length; i++)
            {
                if (i >= text.Length)
                {
                    _text[i].SetValue(' ');
                } else
                {
                    _text[i].SetValue(text[i]);
                }
            }
        }
    }
}
