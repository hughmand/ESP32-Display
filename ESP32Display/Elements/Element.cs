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


        /// <summary>
        /// This is just a convenience method - sometimes the logical orientation to define arrays in code doesn't match the screen orientation.
        /// See examples in ThreeByThreeCharacter.cs
        /// </summary>
        protected bool[][] RotateArray(bool[][] pixels)
        {
            if (pixels.Length < 2 && pixels[0].Length < 2) return pixels;
            var newXLength = pixels[0].Length;
            var newYLength = pixels.Length;

            bool[][] rotatedPixels = new bool[newXLength][];
            for (int i = 0; i < newXLength; i++)
            {
                rotatedPixels[i] = new bool[newYLength];
            }

            for (int x = 0; x < newXLength; x++)
            {
                for (int y = 0; y < newYLength; y++)
                { 
                    rotatedPixels[x][y] = pixels[newYLength-1-y][x];
                }
            }
            return rotatedPixels;
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
        public void RedrawFromState();
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
