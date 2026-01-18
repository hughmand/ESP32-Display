namespace ESP32Display
{
    public class TextElement : Element, IStateElement
    {
        CharState[] _text;
        public TextElement(CharState[] text)
        {
            _text = text;

            int elementLength = (text.Length * 3) + (text.Length - 1); //Allows for gaps between 3 by 3 
            Pixels = new bool[elementLength][];
            for (int i = 0; i < elementLength; i++)
            {
                Pixels[i] = new bool[3];
            }

            for (int i = 0; i < text.Length; i++)
            {
                var elementName = ("Char" + i);
                var charElement = ChildElements.AddElement(elementName, new ThreeByThreeCharacterElement(_text[i]));
                int xPosition = (i * 3) + i;
                ChildElements.SetElementPosition(charElement, xPosition, 0);
            }
        }

        public void UpdateFromState()
        {
            //Nothing to do, this class just sets state and arranges elements
        }
    }
}
