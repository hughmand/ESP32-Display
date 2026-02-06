namespace ESP32Display
{
    public delegate void InputEventHandler();

    public class Input
    {
        public event InputEventHandler InputTriggered;

        protected void TriggerInput()
        {
            InputTriggered?.Invoke();
        }

        public void Subscribe(InputEventHandler handler)
        {
            InputTriggered += handler;
        }

        public void Unsubscribe(InputEventHandler handler)
        {
            InputTriggered -= handler;
        }

        public void UnsubscribeAllHandlers()
        {
            var delegates = InputTriggered?.GetInvocationList();
            if (delegates is not null)
            {
                foreach (InputEventHandler del in delegates)
                {
                    InputTriggered -= del;
                }
            }
        }
    }
}
