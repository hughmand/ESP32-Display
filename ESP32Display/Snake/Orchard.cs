using System;
using System.Diagnostics;

namespace ESP32Display
{
    public class Orchard
    {
        SnakeSettings _snakeSettings;
        Random rnd;

        public CoordinatePoint[] Apples;
        DateTime _lastAppleDrop;

        public Orchard(SnakeSettings snakeSettings)
        {
            _snakeSettings = snakeSettings;
            rnd = new Random();
            Apples = new CoordinatePoint[0];
            _lastAppleDrop = DateTime.MinValue;
        }

        public void TryDropApple()
        {
            if (DateTime.UtcNow - _lastAppleDrop > _snakeSettings._timeBetweenAppleDrops)
            {
                var xCoordinate = rnd.Next(32);
                var yCoordinate = rnd.Next(8);
                var appleCoordinate = new CoordinatePoint(xCoordinate, yCoordinate);
                bool appleAlreadyExists = false;
                for (int i = 0; i < Apples.Length; i++) 
                {
                    if (appleCoordinate.Equals(Apples[i])) appleAlreadyExists = true;
                }

                if (appleAlreadyExists && Apples.Length < 256) TryDropApple();
                else
                {
                    _lastAppleDrop = DateTime.UtcNow;
                    var newApples = new CoordinatePoint[Apples.Length+1];
                    Array.Copy(Apples, newApples, Apples.Length);
                    newApples[Apples.Length] = appleCoordinate;
                    Apples = newApples;
                }
                    return;
            }  
        }
        
        public bool AppleConsumed(CoordinatePoint snakeHead)
        {
            bool appleFound = false;
            int appleIndex = Apples.Length;
            for (int i = 0; i < Apples.Length && !appleFound; i++)
            {
                if (snakeHead.Equals(Apples[i])) //This is defined method, not object.Equals - avoid boxing of value type
                { 
                    appleFound = true;
                    appleIndex = i;
                }
            }

            if (appleFound && appleIndex < Apples.Length)
            {
                var newApples = new CoordinatePoint[Apples.Length-1];
                int j = 0;
                for (int i = 0; i < Apples.Length; i++)
                {
                    if (i != appleIndex)
                    {
                        newApples[j] = Apples[i];
                        j++;
                    }
                }
                Apples = newApples;
            }

            return appleFound;
        }
      
    }
}
