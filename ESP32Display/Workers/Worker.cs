namespace ESP32Display
{
    /// <summary>
    /// Main operations class, incharge of responding to input and changing system state
    /// </summary>
    public abstract class Worker
    {
        protected IInputCollection _inputCollection { get; private set; }
        protected IPulseOutput _buzzer { get; private set; }
        protected SystemState _systemState { get; private set; }
        protected Worker _parentWorker { get; private set; }
        protected DisplayManager _displayManager { get; private set; }

        protected Worker(DisplayManager displayManager, SystemState systemState, Worker parentWorker, IPulseOutput buzzer, IInputCollection inputCollection)
        {
            _inputCollection = inputCollection;
            _systemState = systemState;
            _buzzer = buzzer;
            _parentWorker = parentWorker;
            _displayManager = displayManager;
        }

        public abstract void Run();

        protected virtual void Exit()
        {
            _inputCollection.UnsubscribeAll();
            _parentWorker.Run();
        }
    }
}
