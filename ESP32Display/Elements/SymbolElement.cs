namespace ESP32Display
{
    public enum StaticSymbol
    {
        UpArrow, 
        DownArrow, 
        RightArrow,
        Cross,
        Pause
    }


    class SymbolElement : Element
    {
        StaticSymbol _symbol;

        public SymbolElement(StaticSymbol symbol)
        {
            _symbol = symbol;
            switch (symbol) {
                case StaticSymbol.UpArrow:
                    UpArrow();
                    break;
                case StaticSymbol.DownArrow:
                    DownArrow();
                    break;
                case StaticSymbol.RightArrow:
                    RightArrow();
                    break;
                case StaticSymbol.Cross:
                    Cross();
                    break;
                case StaticSymbol.Pause:
                    Pause();
                    break;
            }
        }

        private void UpArrow()
        {
            Pixels = new bool[2][];
            Pixels[0] = new bool[] { false, true, false };
            Pixels[1] = new bool[] { true, false, true };
            Pixels = RotateArray(Pixels);
        }

        private void DownArrow()
        {
            Pixels = new bool[2][];
            Pixels[0] = new bool[] { true, false, true };
            Pixels[1] = new bool[] { false, true, false };
            Pixels = RotateArray(Pixels);
        }

        private void RightArrow()
        {
            Pixels = new bool[3][];
            Pixels[0] = new bool[] {  true, false};
            Pixels[1] = new bool[] {  false, true };
            Pixels[2] = new bool[] { true, false };
            Pixels = RotateArray(Pixels);
        }

        private void Cross()
        {
            Pixels = new bool[3][];
            Pixels[0] = new bool[] { true, false, true};
            Pixels[1] = new bool[] { false, true, false};
            Pixels[2] = new bool[] { true, false, true};
        }

        private void Pause()
        {
            Pixels = new bool[3][];
            Pixels[0] = new bool[] { true, false, true };
            Pixels[1] = new bool[] { true, false, true };
            Pixels[2] = new bool[] { true, false, true };
            Pixels = RotateArray(Pixels);
        }

    }
}
