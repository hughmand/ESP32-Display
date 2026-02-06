using System;
using System.Diagnostics;
using System.Threading;

namespace ESP32Display
{
    public class DisplayManager : DisplaySoftwareController
    {
        TimeSpan _refreshDelay;
        DateTime _lastFrame;
        FrameTask _preFrameTask;
        FrameTask _postFrameTask;
        Thread _thread;

        public DisplayManager(int dataPinNumber, int csPinNumber, int clkPinNumber) : base(dataPinNumber, csPinNumber, clkPinNumber)
        {
            _refreshDelay = TimeSpan.FromMilliseconds(Configuration.RefreshDelay);
            _lastFrame = DateTime.UtcNow;

            ConfigureHardware();

            _thread = new Thread(() =>
            {
                while (true)
                {
                    //Makes the device feel more responsive, because this thread is heavily limited by the rate at which the display can physically update, not how often these methods run.
                    bool refreshDelayPassed = DateTime.UtcNow - _lastFrame > _refreshDelay;

                    if (!refreshDelayPassed) Thread.Sleep(_refreshDelay);
                    else if (Screen is not null)
                    {
                        TryRunPreFrameTask(); //Important that these tasks cannot cause the display thread to crash
                        Screen.DrawNewFrame();
                        SendToDisplay();
                        _lastFrame = DateTime.UtcNow;
                        TryRunPostFrameTask();
                    }
                }
            });
        }

        public void StartThread()
        {
            _thread.Start();
        }

        public void SetPreFrameTask(FrameTask task)
        {
            _preFrameTask = task;
        }

        public void SetPostFrameTask(FrameTask task)
        {
            _postFrameTask = task;
        }

        public void ClearFrameTasks()
        {
            _preFrameTask = null;
            _postFrameTask = null;
        }

        private void TryRunPreFrameTask()
        {
            try
            {
                _preFrameTask?.Invoke();
            } 
            catch (Exception e)
            {
                Debug.WriteLine("Exception when running pre-frame task: " + e.ToString());
            }
        }

        private void TryRunPostFrameTask()
        {
            try
            {
                _postFrameTask?.Invoke();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception when running post-frame task: " + e.ToString());
            }
        }

    }

    public delegate void FrameTask();
}
