using System;
using System.Diagnostics;

namespace ESP32Display
{
    /// <summary>
    /// Screen classes should hold state for a screen, as well as methods to map state into pixels. They should not contain logic and methods for updating state
    /// </summary>
    public class Screen
    {
        public bool[][] Pixels { get; protected set; }

        protected IElementCollection Elements = new ElementCollection();

        public Screen()
        {
            Pixels = new bool[32][];
            for (int i = 0; i < 32; i++)
            {
                Pixels[i] = new bool[8];
            }
        }

        public void NewFrame()
        {
            Elements.NewFrame();
            DrawElements();
            //PrintToDebug(); //UNCOMMENT FOR DEBUGGING
        }

        /// <summary>
        /// Compare all pixels on each row between two screens to determine which rows have been changed. If changed, returns true
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public bool[] CompareRows(Screen screen)
        {
            bool[] difference = new bool[8];
            if (screen is not null)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 32 && !difference[i]; j++)
                    {
                        difference[i] = !(screen.Pixels[j][i] ^ Pixels[j][i]);
                    }
                }
            } 
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    difference[i] = true;
                }
            }
            return difference;
        }

        
        /// <summary>
        /// Draws all elements onto the screen pixel array in their current state.
        /// The rendering system is not designed to work with elements overlapping - static state will not necessarily be replaced if an animated element overlaps and subsequently retracts. 
        /// </summary>
        private void DrawElements()
        {
            foreach (Element element in Elements.GetElements()) 
            {
                //TODO: No need to rerender static elements, nothing will have changed
                element.DrawElement();
                PixelMappingHelper.MapPixelArrays(Pixels, element.Pixels, element.XPosition, element.YPosition);
            }
        }

        /// <summary>
        /// Useful method for debugging, has significant performance impact.
        /// </summary>
        private void PrintToDebug()
        {
            for (int i = 7; i >=0; i--)
            {
                string rowString = string.Empty;
                for (int j = 0; j < 32; j++)
                {
                    rowString += Pixels[j][i] ? "1" : "0";
                }
                Debug.WriteLine(rowString);
            }
            Debug.WriteLine(string.Empty);
        }
    }
}
