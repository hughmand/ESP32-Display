namespace ESP32Display
{
    public enum InputLabel
    {
        Up, Down, Left, Right, Enter
    }
    public interface IInputCollection
    {
        void Subscribe(InputLabel input, InputEventHandler inputHandler);
        void Unsubscribe(InputLabel input, InputEventHandler inputHandler);
        void UnsubscribeAllHandlers(InputLabel inputLabel);
        void UnsubscribeAll();
    }

    public class InputCollection : IInputCollection
    {
        Button[] _buttons;
        IPulseOutput _inputIndicator;

        public InputCollection(IPulseOutput inputIndicator, params int[] pinNumbers)
        {
            _buttons = new Button[pinNumbers.Length];
            _inputIndicator = inputIndicator;
            for (int i = 0; i < pinNumbers.Length; i++)
            {
                _buttons[i] = new Button(pinNumbers[i]);   
                UnsubscribeAll(i);
            }
        }

        public void Subscribe(InputLabel input, InputEventHandler inputHandler)
        {
            int index = (int)input;
            var button = _buttons[index];
            button.Subscribe(inputHandler);
        }

        public void Unsubscribe(InputLabel input, InputEventHandler inputHandler)
        {
            int index = (int)input;
            var button = _buttons[index];
            button.Unsubscribe(inputHandler);
        }

        public void UnsubscribeAll(int inputIndex)
        { 
            var button = _buttons[inputIndex];
            button.UnsubscribeAllHandlers();
        }
        public void UnsubscribeAll()
        {
            foreach (var button in _buttons)
            {
                button.UnsubscribeAllHandlers();
                button.InputTriggered += IndicatePress;
            }
        }

        public void UnsubscribeAllHandlers(InputLabel inputLabel)
        {
            int index = (int)inputLabel;
            UnsubscribeAll(index);
        }

        private void IndicatePress()
        {
            _inputIndicator.Pulse();
        }
    }
}
