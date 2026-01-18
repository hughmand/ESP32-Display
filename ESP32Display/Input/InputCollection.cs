using Iot.Device.Button;
using System;
using System.Device.Gpio;
using System.Text;

namespace ESP32Display
{
    public enum InputLabel
    {
        Up, Down, Left, Right, Enter
    }


    public interface IInputCollection
    {
        void Subscribe(int inputIndex, InputHandler inputHandler);
        void Subscribe(InputLabel input, InputHandler inputHandler);
        void Unsubscribe(int inputIndex, InputHandler inputHandler);
        void Unsubscribe(InputLabel input, InputHandler inputHandler);
    }

    //Allows wrapping no argument handler, as don't really need event args in rest of program
    public delegate void InputHandler();

    public class InputCollection : IInputCollection
    {
        GpioButton[] _buttons;
        IPulseOutput _inputIndicator;

        public InputCollection(IPulseOutput inputIndicator, params int[] pinNumbers)
        {
            _buttons = new GpioButton[pinNumbers.Length];
            for (int i = 0; i < pinNumbers.Length; i++)
            {
                _buttons[i] = new GpioButton(pinNumbers[i]);   
                _buttons[i].Press += IndicatePress;
            }
            _inputIndicator = inputIndicator;
        }

        private void IndicatePress(object sender, EventArgs e)
        {
            _inputIndicator.Pulse();
        }

        public void Subscribe(int inputIndex, ButtonBase.ButtonPressedDelegate inputHandler)
        {
            if (inputIndex < _buttons.Length && inputIndex >= 0) 
            {
                _buttons[inputIndex].Press += inputHandler;
            }
        }

        public void Unsubscribe(int inputIndex, ButtonBase.ButtonPressedDelegate inputHandler)
        {
            if (inputIndex < _buttons.Length && inputIndex > 0)
            {
                _buttons[inputIndex].Press -= inputHandler;
            }
        }

        public void Subscribe(int inputIndex, InputHandler inputEvent)
        {
            Subscribe(inputIndex, Wrapper);

            void Wrapper(object sender, EventArgs e)
            {
                inputEvent.Invoke();
            }
        }

        public void Unsubscribe(int inputIndex, InputHandler inputEvent)
        {
            Unsubscribe(inputIndex, Wrapper);
            void Wrapper(object sender, EventArgs e)
            {
                inputEvent.Invoke();
            }
        }

        public void Subscribe(InputLabel input, InputHandler inputEvent)
        {
            Subscribe((int)input, inputEvent);
        }

        public void Unsubscribe(InputLabel input, InputHandler inputEvent)
        {
            Unsubscribe((int)input, inputEvent);
        }
    }
}
