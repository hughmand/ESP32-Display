using System;

namespace ESP32Display
{
    public class TextElement : Element, IStateElement
    {
        CharState[] _text;
        IntState _int;

        public TextElement(CharState[] text)
        {
            _text = text;

            ArrangeElements();
        }

        public TextElement(string text)
        {
            var textArray = new CharState[text.Length];
            for (int i = 0; i < textArray.Length; i++)
            {
                textArray[i] = new CharState(text[i]);
            }
            _text = textArray;
            ArrangeElements();
        }

        private void ArrangeElements()
        {
            int elementLength = (_text.Length * 3) + (_text.Length - 1); //Allows for gaps between 3 by 3 
            Pixels = new bool[elementLength][];
            for (int i = 0; i < elementLength; i++)
            {
                Pixels[i] = new bool[3];
            }

            for (int i = 0; i < _text.Length; i++)
            {
                var elementName = ("Char" + i);
                var charElement = ChildElements.AddElement(elementName, new ThreeByThreeCharacterElement(_text[i]));
                int xPosition = (i * 3) + i;

                ChildElements.SetElementPosition(charElement, xPosition, 0);
            }
        }

        public TextElement(IntState number)
        {
            _int = number;
        }

        public void RedrawFromState()
        {
            //Nothing to do, this class just sets state and arranges elements
        }
    }
}
