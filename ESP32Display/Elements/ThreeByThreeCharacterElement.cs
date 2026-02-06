namespace ESP32Display
{
    public class ThreeByThreeCharacterElement : Element, IStateElement
    {
        CharState _charState;

        public ThreeByThreeCharacterElement(CharState state) 
        {
            _charState = state;
            RedrawFromState();
        }

        public void RedrawFromState()
        {
            Pixels = new bool[3][];
            switch (_charState.Value)
            {
                case 'A':
                    Pixels[0] = new bool[] { false, true, true };
                    Pixels[1] = new bool[] { true, false, true };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case 'B':
                    Pixels[0] = new bool[] { true, false, false };
                    Pixels[1] = new bool[] { true, true, true };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case 'C':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { true, false, false };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case 'D':
                    Pixels[0] = new bool[] { false, false, true };
                    Pixels[1] = new bool[] { true, true, true };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case 'E':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { true, true, false };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case 'F':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { true, true, false };
                    Pixels[2] = new bool[] { true, false, false };
                    break;
                case 'G':
                    Pixels[0] = new bool[] { true, true, false };
                    Pixels[1] = new bool[] { true, false, true };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case 'H':
                    Pixels[0] = new bool[] { true, false, true };
                    Pixels[1] = new bool[] { true, true, true };
                    Pixels[2] = new bool[] { true, false, true };
                    break;
                case 'I':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { false, true, false };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case 'J':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { false, true, false };
                    Pixels[2] = new bool[] { true, true, false };
                    break;
                case 'K':
                    Pixels[0] = new bool[] { true, false, true };
                    Pixels[1] = new bool[] { true, true, false };
                    Pixels[2] = new bool[] { true, false, true };
                    break;
                case 'L':
                    Pixels[0] = new bool[] { true, false, false };
                    Pixels[1] = new bool[] { true, false, false };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case 'M':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { true, true, true };
                    Pixels[2] = new bool[] { true, false, true };
                    break;
                case 'N':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { true, false, true };
                    Pixels[2] = new bool[] { true, false, true };
                    break;
                case 'O':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { true, false, true };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case 'P':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { true, true, true };
                    Pixels[2] = new bool[] { true, false, false };
                    break;
                case 'Q':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { true, true, true };
                    Pixels[2] = new bool[] { false, false, true };
                    break;
                case 'R':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { true, true, false };
                    Pixels[2] = new bool[] { true, false, true };
                    break;
                case 'S':
                    Pixels[0] = new bool[] { false, true, true };
                    Pixels[1] = new bool[] { false, true, false };
                    Pixels[2] = new bool[] { true, true, false };
                    break;
                case 'T':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { false, true, false };
                    Pixels[2] = new bool[] { false, true, false };
                    break;
                case 'U':
                    Pixels[0] = new bool[] { true, false, true };
                    Pixels[1] = new bool[] { true, false, true };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case 'V':
                    Pixels[0] = new bool[] { true, false, true };
                    Pixels[1] = new bool[] { true, false, true };
                    Pixels[2] = new bool[] { false, true, false };
                    break;
                case 'W':
                    Pixels[0] = new bool[] { true, false, true };
                    Pixels[1] = new bool[] { true, true, true };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case 'X':
                    Pixels[0] = new bool[] { true, false, true };
                    Pixels[1] = new bool[] { false, true, false };
                    Pixels[2] = new bool[] { true, false, true };
                    break;
                case 'Y':
                    Pixels[0] = new bool[] { true, false, true };
                    Pixels[1] = new bool[] { false, true, false };
                    Pixels[2] = new bool[] { false, true, false };
                    break;
                case 'Z':
                    Pixels[0] = new bool[] { true, true, false };
                    Pixels[1] = new bool[] { false, true, false };
                    Pixels[2] = new bool[] { false, true, true };
                    break;
                case '0':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { true, false, true };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case '1':
                    Pixels[0] = new bool[] { false, false, true };
                    Pixels[1] = new bool[] { false, false, true };
                    Pixels[2] = new bool[] { false, false, true };
                    break;
                case '2':
                    Pixels[0] = new bool[] { true, true, false };
                    Pixels[1] = new bool[] { false, true, false };
                    Pixels[2] = new bool[] { false, true, true };
                    break;
                case '3':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { false, true, true };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case '4':
                    Pixels[0] = new bool[] { true, false, true };
                    Pixels[1] = new bool[] { false, true, true };
                    Pixels[2] = new bool[] { false, false, true };
                    break;
                case '5':
                    Pixels[0] = new bool[] { false, true, true };
                    Pixels[1] = new bool[] { false, true, false };
                    Pixels[2] = new bool[] { true, true, false };
                    break;
                case '6':
                    Pixels[0] = new bool[] { true, false, false };
                    Pixels[1] = new bool[] { true, true, true };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case '7':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { false, false, true };
                    Pixels[2] = new bool[] { false, false, true };
                    break;
                case '8':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { true, true, true };
                    Pixels[2] = new bool[] { true, true, true };
                    break;
                case '9':
                    Pixels[0] = new bool[] { true, true, true };
                    Pixels[1] = new bool[] { true, true, true };
                    Pixels[2] = new bool[] { false, false, true };
                    break;
                default:
                    for (int i = 0; i < Pixels.Length; i++)
                    {
                        Pixels[i] = new bool[3];
                    }
                    break;
            }
            Pixels = RotateArray(Pixels);
        }

        
    }
}
