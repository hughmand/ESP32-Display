using System.Runtime.CompilerServices;

namespace ESP32Display
{
    public enum AnimatedSymbol
    {
        Loading,
        FlashingDot,
        LargeFlashingDot
    }

    /// <summary>
    /// Element is animated, and progresses into a new state everytime a new frame is drawn
    /// </summary>
    public class AnimatedElement : Element, IAnimatedElement, IStateElement
    {
        int _state = 0;
        AnimatedSymbol _symbol;

        public AnimatedElement(AnimatedSymbol symbol) 
        {
            _symbol = symbol;
        }

        public void NextFrame()
        {
            _state++;
        }

        public void UpdateFromState()
        {
            switch (_symbol)
            {
                case AnimatedSymbol.Loading:
                    LoadingSymbol();
                    break;
                case AnimatedSymbol.FlashingDot:
                    FlashingDot();
                    break;
                case AnimatedSymbol.LargeFlashingDot:
                    LargeFlashingDot();
                    break;
            }
        }

        private void LoadingSymbol()
        {
            int modState = _state % 4;
            Pixels = new bool[3][];
            Pixels[0] = new bool[] { modState == 0, modState == 1, modState == 2 };
            Pixels[1] = new bool[] { modState == 3, true, modState == 3 };
            Pixels[2] = new bool[] { modState == 2, modState == 1, modState == 0 };
        }

        private void FlashingDot()
        {
            bool evenState = _state % 2 == 0;
            Pixels = new bool[1][];
            Pixels[0] = new bool[1];
            Pixels[0][0] = evenState;
        }
        private void LargeFlashingDot()
        {
            bool evenState = _state % 2 == 0;
            Pixels = new bool[2][];
            for (int i = 0; i < 2; i++)
            {
                Pixels[i] = new bool[2];
                for (int j = 0; j < 2; j++)
                {
                    Pixels[i][j] = evenState;
                }
            }
        }
    }
}
