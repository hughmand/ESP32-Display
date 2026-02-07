using nanoFramework.Json;
using System;
using System.Diagnostics;
using System.Text;

namespace ESP32Display
{
    public class RiverLevelDataProvider 
    {
        WirelessController _wirelessController;

        public RiverLevelDataProvider(WirelessController wirelessController)
        {
            _wirelessController = wirelessController;
        }

        public RiverLevel GetRiverLevel(RiverIdentifier identifier)
        {
            if (SystemState.WifiConnected)
            {

                string jsonData = _wirelessController.Request(GenerateTargetUrl(identifier));
                Console.WriteLine(jsonData);
                //JsonConvert.DeserializeObject(jsonData, typeof(RiverLevel));
            }
            return new RiverLevel();
        }

        private string GenerateTargetUrl(RiverIdentifier identifier)
        {
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append(RiverLevelAPIConfiguration.BaseService);

            urlBuilder.Append("/");

            urlBuilder.Append(identifier.Guid);
            urlBuilder.Append("-");
            urlBuilder.Append(RiverLevelAPIConfiguration.TimeSeriesSuffix);

            urlBuilder.Append("/");

            urlBuilder.Append(RiverLevelAPIConfiguration.ReadingsSuffix);
            urlBuilder.Append("?");
            urlBuilder.Append("mineq-date=");
            urlBuilder.Append(GetMinDate());

            return urlBuilder.ToString();
        }

        private string GetMinDate()
        {
            DateTime minDate = DateTime.UtcNow.AddDays(-3);
            return minDate.ToString("yyyy-MM-dd");
        }
    }
}
