namespace ESP32Display
{
    public class LargeClockElement : ClockElement
    {
        public LargeClockElement(bool twentyFourHourTime) : base(twentyFourHourTime)
        {
            Pixels = new bool[32][];
            for (int i = 0; i < 32; i++)
            {
                Pixels[i] = new bool[8];
            }
            RedrawFromState();

            var hours1 = ChildElements.AddElement("hours1", new LargeNumberElement(_hours[0]));
            var hours2 = ChildElements.AddElement("hours2", new LargeNumberElement(_hours[1]));
            var colon1 = ChildElements.AddElement("colon1", new AnimatedElement(AnimatedSymbol.LargeFlashingDot));
            var minutes1 = ChildElements.AddElement("minutes1", new LargeNumberElement(_minutes[0]));
            var minutes2 = ChildElements.AddElement("minutes2", new LargeNumberElement(_minutes[1]));
            var colon2 = ChildElements.AddElement("colon2", new AnimatedElement(AnimatedSymbol.LargeFlashingDot));
            var seconds1 = ChildElements.AddElement("seconds1", new LargeNumberElement(_seconds[0]));
            var seconds2 = ChildElements.AddElement("seconds2", new LargeNumberElement(_seconds[1]));

            ChildElements.SetElementPosition(hours1, 0, 0);
            ChildElements.SetElementPosition(hours2, 5, 0);
            ChildElements.SetElementPosition(colon1, 9, 3);
            ChildElements.SetElementPosition(minutes1, 11, 0);
            ChildElements.SetElementPosition(minutes2, 16, 0);
            ChildElements.SetElementPosition(colon2, 20, 3);
            ChildElements.SetElementPosition(seconds1, 22, 0);
            ChildElements.SetElementPosition(seconds2, 27, 0);
        }

    }
}
