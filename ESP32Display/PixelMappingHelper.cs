namespace ESP32Display
{
    public static class PixelMappingHelper
    {
        public static void MapPixelArrays(bool[][] pixels, bool[][] sourcePixels, int startX, int startY)
        {
            if (pixels is not null
                && pixels.Length > 0
                && sourcePixels is not null
                && sourcePixels.Length > 0
                && startX < pixels.Length
                && startY < pixels[0].Length)
            {
                for (int x = 0; x < sourcePixels.Length && x + startX < pixels.Length; x++)
                {
                    for (int y = 0; y < sourcePixels[0].Length && y + startY < pixels[0].Length; y++)
                    {
                        pixels[startX + x][startY + y] = sourcePixels[x][y];
                    }
                }
            }
        }
    }
}
