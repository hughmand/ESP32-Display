using System.Diagnostics;

namespace ESP32Display
{
    public class HomeScreen : Screen
    {

        CharState[] _text;

        public HomeScreen()
        {
            _text = new CharState[8]; //Cannot be longer than 8 characters
            for (int i = 0; i < 8; i++)
            {
                _text[i] = new CharState();
            }
            var clock = Elements.AddElement("clock", new SmallClockElement(Configuration.TwentyFourHourClockFormat));
            var text = Elements.AddElement("text", new TextElement(_text));
            //var leftArrow = Elements.AddElement("leftArrow",);

            Elements.SetElementPosition(clock, 0, 5);
            Elements.SetElementPosition(text, 0, 0);
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
