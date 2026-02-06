using System;
using System.Device.Gpio;

namespace ESP32Display
{
    public class Button : Input, IDisposable
    {
        private TimeSpan _debounceTime = TimeSpan.FromMilliseconds(100);
        private long _debounceStartTicks;
        private long _lastPressTicks;
        private int _buttonPin;
        private bool _pressed;
        private bool _debounce => DateTime.UtcNow.Ticks - _debounceStartTicks < _debounceTime.Ticks;

        GpioController _controller;
        GpioPin _pin;

        public Button(int pinNumber)
        {
            _buttonPin = pinNumber;
            _controller= new GpioController();
            _pin = _controller.OpenPin(pinNumber, PinMode.InputPullUp);
            _controller.RegisterCallbackForPinValueChangedEvent(pinNumber, PinEventTypes.Rising | PinEventTypes.Falling, VoltageChanged);
        }

        public void VoltageChanged(object sender, PinValueChangedEventArgs args)
        {
            if (args.ChangeType == PinEventTypes.Falling)
            {
                OnPress();
            }
            else
            {
                OnRelease();
            }
        }

        protected void OnPress()
        {
            if (!_pressed && !_debounce)
            {
                _pressed = true;
                SetSuccesfulPress();
            }
        }
        protected void OnRelease()
        {
            if (!_pressed) //Shouldn't end up here
            {
                return;
            }
            TriggerInput();
            _pressed = false;
            if (_lastPressTicks == DateTime.MinValue.Ticks)
            {
                _lastPressTicks = DateTime.UtcNow.Ticks;
                return;
            }

            _lastPressTicks = DateTime.MinValue.Ticks;
        }
        private void SetSuccesfulPress()
        {
            _debounceStartTicks = DateTime.UtcNow.Ticks;
        }
        public void Dispose()
        {
            _controller.UnregisterCallbackForPinValueChangedEvent(_buttonPin, VoltageChanged);
        }
    }
}
