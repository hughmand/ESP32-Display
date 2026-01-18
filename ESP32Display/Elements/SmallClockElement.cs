namespace ESP32Display
{
    public class SmallClockElement : ClockElement
    {
        public SmallClockElement(bool twentyFourHourTime) : base(twentyFourHourTime)
        {
            Pixels = new bool[23][];
            for (int i = 0; i < 23; i++)
            {
                Pixels[i] = new bool[3];
            }

            UpdateFromState();

            var hours1 = ChildElements.AddElement("hours1", new ThreeByThreeCharacterElement(_hours[0]));
            var hours2 = ChildElements.AddElement("hours2", new ThreeByThreeCharacterElement(_hours[1]));
            var colon1 = ChildElements.AddElement("colon1", new AnimatedElement(AnimatedSymbol.FlashingDot));
            var minutes1 = ChildElements.AddElement("minutes1", new ThreeByThreeCharacterElement(_minutes[0]));
            var minutes2 = ChildElements.AddElement("minutes2", new ThreeByThreeCharacterElement(_minutes[1]));
            var colon2 = ChildElements.AddElement("colon2", new AnimatedElement(AnimatedSymbol.FlashingDot));
            var seconds1 = ChildElements.AddElement("seconds1", new ThreeByThreeCharacterElement(_seconds[0]));
            var seconds2 = ChildElements.AddElement("seconds2", new ThreeByThreeCharacterElement(_seconds[1]));

            ChildElements.SetElementPosition(hours1, 0, 0);
            ChildElements.SetElementPosition(hours2, 4, 0);
            ChildElements.SetElementPosition(colon1, 7, 1);
            ChildElements.SetElementPosition(minutes1, 8, 0);
            ChildElements.SetElementPosition(minutes2, 12, 0);
            ChildElements.SetElementPosition(colon2, 15, 1);
            ChildElements.SetElementPosition(seconds1, 16, 0);
            ChildElements.SetElementPosition(seconds2, 20, 0);  
        }
    }
}
