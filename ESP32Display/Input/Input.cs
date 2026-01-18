using System;

namespace ESP32Display
{
    public delegate void InputEventHandler();

    public class Input
    {
        public event InputEventHandler InputTriggered;

        protected void OnInput()
        {
            InputTriggered?.Invoke();
        }

        public void UnsubscribeAll()
        {
            Delegate[] delegates = InputTriggered?.GetInvocationList();
            foreach (InputEventHandler delegator in delegates)
            {
                InputTriggered -= delegator;
            }
        }
    }
}
