namespace ESP32Display
{
    /// <summary>
    /// Main operations class, incharge of responding to input and changing system state
    /// </summary>
    public abstract class Worker
    {
        protected IInputCollection _inputCollection { get; private set; }
        protected IPulseOutput _buzzer { get; private set; }
        protected DisplayState _displayState { get; private set; }
        protected SystemState _systemState { get; private set; }

        protected Worker(IPulseOutput buzzer, IInputCollection inputCollection, DisplayState displayState, SystemState systemState)
        {
            _inputCollection = inputCollection;
            _displayState = displayState;
            _systemState = systemState;
            _buzzer = buzzer;
        }

        public abstract void Run();
    }
}
