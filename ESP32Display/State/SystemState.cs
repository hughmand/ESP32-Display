namespace ESP32Display
{
    //If using a DI framework, then this wouldn't be a static class - singleton instead. Generally be careful using static 
    //Here it is fine though
    public static class SystemState
    {
        public static IntState Brightness = new IntState();
        public static BoolState WifiConnected = new BoolState();
    }
}
