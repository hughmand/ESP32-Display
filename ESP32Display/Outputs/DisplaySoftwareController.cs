namespace ESP32Display
{
    public enum Command : byte
    {
        Nothing = 0,
        Decode = 9,
        Intensity = 10,
        ScanLimit = 11,
        Shutdown = 12,
        DisplayTest = 15,
    }

    public class DisplaySoftwareController : DisplayHardwareController
    {
        public Screen Screen
        {
            get => _screen;
            set
            {
                if (Screen is not null) _lastScreen = Screen;
                _screen = value;
            }
        }
        private Screen _screen;
        private Screen _lastScreen;

        public DisplaySoftwareController(int dataPinNumber, int csPinNumber, int clkPinNumber) : base(dataPinNumber, csPinNumber, clkPinNumber)
        {
            //Intentionally empty
        }

        public void AdjustBrightness(int brightness)
        {
            if (brightness > (int)Intensity.MaximumBrightness) brightness = (int)Intensity.MaximumBrightness;
            if (brightness < (int)Intensity.MinimumBrightness) brightness = (int)Intensity.MinimumBrightness;
            PrepareAndSendInstructionForAllScreens(Command.Intensity, brightness);
        }

        protected void ConfigureHardware()
        {
            PrepareAndSendInstructionForAllScreens(Command.DisplayTest, 0);
            PrepareAndSendInstructionForAllScreens(Command.ScanLimit, 7);
            PrepareAndSendInstructionForAllScreens(Command.Intensity, (int)Configuration.Brightness);
            PrepareAndSendInstructionForAllScreens(Command.Decode, 0);
            PrepareAndSendInstructionForAllScreens(Command.Shutdown, 1);
        }

        private void PrepareAndSendInstructionForAllScreens(Command command, int value)
        {
            byte[] instruction = PrepareInstructionForAllScreens(command, value);
            SendCommand(instruction);
        }

        protected void SendToDisplay()
        {
            var newScreen = Screen;

            
            var pixels = newScreen.Pixels;
            var bytePixels = BoolToByteArray(pixels);
            
            bool[] rowChanged = Screen.CompareRows(_lastScreen);

            switch (Configuration.RefreshPattern)
            {
                case DisplayRefreshPattern.BottomToTop:
                    for (int j = 0; j < 8; j++)
                    {
                        SendRow(j);
                    }
                    break;
                case DisplayRefreshPattern.TopToBottom:
                    for (int j = 7; j >=0; j--)
                    {
                        SendRow(j);
                    }
                    break;

                case DisplayRefreshPattern.SplitOutwards:
                    for (int j = 0; j < 4; j++)
                    {
                        SendRow(3-j);
                        SendRow(4+j);
                    }
                    break;
                case DisplayRefreshPattern.SplitInwards:
                    for (int j = 3; j >=0; j--)
                    {
                        SendRow(3 - j);
                        SendRow(4 + j);
                    }
                    break;
            }
            
            void SendRow(int j)
            {
                if ((Configuration.RefreshOptimisation && rowChanged[j]) || !Configuration.RefreshOptimisation)
                {
                    int bufferIndex = 0;
                    byte[] buffer = new byte[8];
                    for (int i = 0; i < 4; i++)
                    {
                        buffer[bufferIndex] = (byte)(8 - j);
                        buffer[bufferIndex + 1] = bytePixels[i][j];
                        bufferIndex += 2;
                    }
                    SendCommand(buffer);
                }
                //else, do nothing - row doesn't need updating, optimises refresh time
            }
        }

        /// <summary>
        /// Converts the screen into a byte representation - row of 8 booleans is collapsed into a byte value (8 bits)
        /// </summary>
        /// <param name="screenPixels"></param>
        /// <param name="bytePixels"></param>
        private byte[][] BoolToByteArray(bool[][] screenPixels)
        {
            byte[][] bytePixels = new byte[4][];
            for (int i = 0; i < 4; i++)
            {
                bytePixels[i] = new byte[8];
            }


            for (int i = 0; i < 8; i++)
            {
                int byteIndex = 0;
                for (int j = 0; j < 32; j += 8)
                {
                    byte screenByte = 0;
                    for (int k = 0; k < 8; k++)
                    {

                        screenByte = (byte)((screenByte << 1) | (screenPixels[j + k][i] ? 1 : 0));

                    }
                    bytePixels[byteIndex][i] = screenByte;
                    byteIndex++;
                }
            }
            return bytePixels;
        }

        /// <summary>
        /// Sending identical instructions to all 4 sub displays - useful in setup.
        /// </summary>
        private byte[] PrepareInstructionForAllScreens(Command command, int value)
        {
            byte[] buffer = new byte[8];
            for (int i = 0; i < 8; i += 2)
            {
                buffer[i] = (byte)command;
                buffer[i + 1] = (byte)value;
            }
            return buffer;
        }
    }
}
