namespace ESP32Display
{
    public class LargeNumberElement : Element, IStateElement
    {
        CharState _charState;

        public bool TopBar;
        public bool UpperLeftBar;
        public bool UpperRightBar;
        public bool LowerRightBar;
        public bool LowerLeftBar;
        public bool BottomBar;
        public bool MiddleBar;

        public LargeNumberElement(CharState state)
        {
            _charState = state;
            Pixels = new bool[4][];
            for (int i = 0; i < Pixels.Length; i++)
            {
                Pixels[i] = new bool[8];
            }
            RedrawFromState();
        }

        public void RedrawFromState() {
            TopBar = false;
            BottomBar = false;
            MiddleBar = false;
            UpperLeftBar = false;
            UpperRightBar = false;
            LowerLeftBar = false;
            LowerRightBar = false;


            switch (_charState.Value)
            {
                case '0':
                    TopBar = true;
                    BottomBar = true;
                    LowerLeftBar = true;
                    UpperLeftBar = true;
                    LowerRightBar = true;
                    UpperRightBar = true;
                    break;
                case '1':
                    UpperRightBar = true;
                    LowerRightBar = true;
                    break;
                case '2':
                    TopBar = true;
                    UpperRightBar = true;
                    MiddleBar = true;
                    LowerLeftBar = true;
                    BottomBar = true;
                    break;
                case '3':
                    TopBar = true;
                    MiddleBar = true;
                    BottomBar = true;
                    UpperRightBar = true;
                    LowerRightBar = true;
                    break;
                case '4':
                    UpperRightBar = true;
                    UpperLeftBar = true;
                    MiddleBar = true;
                    LowerRightBar = true;
                    break;
                case '5':
                    TopBar = true;
                    UpperLeftBar = true;
                    MiddleBar = true;
                    LowerRightBar = true;
                    BottomBar = true;
                    break;
                case '6':
                    TopBar = true;
                    UpperLeftBar = true;
                    MiddleBar = true;
                    LowerLeftBar = true;
                    BottomBar = true;
                    LowerRightBar = true;
                    break;
                case '7':
                    TopBar = true;
                    UpperRightBar = true;
                    LowerRightBar = true;
                    break;
                case '8':
                    TopBar = true;
                    BottomBar = true;
                    MiddleBar = true;
                    LowerLeftBar = true;
                    UpperLeftBar = true;
                    LowerRightBar = true;
                    UpperRightBar = true;
                    break;
                case '9':
                    TopBar = true;
                    BottomBar = true;
                    MiddleBar = true;
                    UpperLeftBar = true;
                    LowerRightBar = true;
                    UpperRightBar = true;
                    break;
            }
            SetPixels();
        }
        private void SetPixels()
        {
            //Prevents shared pixels being overwritten
            for(int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Pixels[i][j] = false;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                Pixels[i][7] = TopBar;
            }

            for (int i = 0; i < 4; i++)
            {
                Pixels[i][0] = BottomBar;
            }

            for (int i = 0; i < 4; i++)
            {
                Pixels[i][4] = MiddleBar;
            }

            for (int i = 4; i < 8; i++)
            {
                Pixels[0][i] = Pixels[0][i] || UpperLeftBar;
            }

            for (int i = 4; i < 8; i++)
            {
                Pixels[3][i] = Pixels[3][i] || UpperRightBar;
            }

            for (int i = 0; i < 4; i++)
            {
                Pixels[0][i] = Pixels[0][i] || LowerLeftBar;
            }

            for (int i = 0; i < 4; i++)
            {
                Pixels[3][i] = Pixels[3][i] || LowerRightBar;
            }
        }
    }
}
