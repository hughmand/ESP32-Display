namespace ESP32Display
{
    //These classes serve to turn the underlying value types into reference types. This is important for the segregation of state and element classes.
    //This would be a lovely place to use generics, but they are not supported by nanoframework yet.
    public class IntState
    {
        public int Value;

        public static implicit operator int(IntState state) => state.Value;

        public void SetValue(int state) 
        {
            Value = state;
        }
    }

    public class BoolState
    {
        public bool Value;

        public static implicit operator bool(BoolState state) => state.Value;

        public void SetValue(bool state)
        {
            Value = state;
        }

        public override string ToString()
        {
            return Value ? "True" : "False";
        }
    }
    public class CharState
    {
        public char Value;

        public static implicit operator char(CharState state) => state.Value;

        public CharState() { }
        public CharState(char c)
        {
            SetValue(c);
        }
        public void SetValue(char state)
        {
            Value = state;
        }
    }
}
