namespace ESP32Display
{
    /// <summary>
    /// Base class for a screen element. Holds all information required to render the element into a parent element or screen
    /// </summary>
    public abstract class Element
    {
        public bool[][] Pixels;
        public int XPosition { get; private set; }
        public int YPosition { get; private set; }

        public IElementCollection ChildElements = new ElementCollection();

        public void SetXY(int x, int y)
        {
            XPosition = x;
            YPosition = y;
        }

        /// <summary>
        /// Takes child element pixels and maps them into the current element's pixel array.
        /// </summary>
        public void DrawElement()
        {
            foreach (var element in ChildElements.GetElements())
            {
                PixelMappingHelper.MapPixelArrays(Pixels, element.Pixels, element.XPosition, element.YPosition);
            }
        }
    }

    /// <summary>
    /// Element has a dependency on state and should be updated when new frames are drawn
    /// </summary>
    public interface IStateElement
    {
        /// <summary>
        /// Update pixel elements with a dependency on state
        /// </summary>
        public void UpdateFromState();
    }

    /// <summary>
    /// Element has a animated component, that isn't dependent on system state, but should be updated when new frames are drawn
    /// </summary>
    public interface IAnimatedElement
    {
        /// <summary>
        /// Draw the next frame in the animation
        /// </summary>
        public void NextFrame();
    }
}
