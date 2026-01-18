using nanoFramework.Networking;

using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;

namespace ESP32Display
{
    public interface IWirelessController
    {
        void ConnectDhcp();
        void Request();
    }

    public class WirelessController : IWirelessController
    {
        public void ConnectDhcp()
        {
            const string Ssid = "";
            const string Password = "";
            // Give 60 seconds to the wifi join to happen
            CancellationTokenSource cs = new(60000);
            WifiNetworkHelper.ConnectDhcp(Ssid, Password, requiresDateTime: true, token: cs.Token);
        }

        public void Request()
        {
            //TODO: Implement URL request
            throw new System.NotImplementedException();
        }
    }
}
