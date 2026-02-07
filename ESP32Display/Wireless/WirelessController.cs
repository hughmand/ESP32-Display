using nanoFramework.Networking;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace ESP32Display
{ 
    public class WirelessController
    {
        X509Certificate rootCACert;

        public WirelessController()
        {
            SystemState.WifiConnected.SetValue(false);
        }

        public void TryConnect()
        {
            Console.WriteLine("Connecting to existing wireless");
            if (!WifiNetworkHelper.Reconnect())
            {
                Console.WriteLine("Failed, trying with new credentials");
                if (!ConnectNewDhcp())
                {
                    Console.WriteLine("Failed, will not connect");
                }
            }

            if (WifiNetworkHelper.Status == NetworkHelperStatus.NetworkIsReady)
            {
                SystemState.WifiConnected.SetValue(true);
                Console.WriteLine("Connection success");
                //DateTime needs to be set from wireless for this to produce a valid certificate
                rootCACert = new X509Certificate(Convert.FromBase64String(Resources.X509CertificateString));
            }
            Console.WriteLine("Current UTC set to: " + DateTime.UtcNow.ToString("HH:mm:ss"));
        }

        public bool ConnectNewDhcp()
        {
            //Give 60 seconds, then cancel
            CancellationTokenSource cs = new(60000);
            return WifiNetworkHelper.ConnectDhcp(Configuration.WifiSsid, Configuration.WifiPassword, requiresDateTime: true, token: cs.Token);
        }

        public string Request(string url)
        {
            if (!SystemState.WifiConnected)
            {
                Console.WriteLine("Wireless disconnected, will not send URL");
                return string.Empty;
            }

            Console.WriteLine("Sending URL Request: " + url);
            string responseString = string.Empty;
            try
            {
                byte[] responseBuffer = null;

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.SslProtocols = System.Net.Security.SslProtocols.Tls12;
                request.HttpsAuthentCert = rootCACert;

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    Console.WriteLine("Response recieved, status: " + response.StatusCode.ToString());
                    using (var stream = response.GetResponseStream())
                    {
                        var responseLength = response.ContentLength;
                        responseBuffer = new byte[responseLength];
                        stream.Read(responseBuffer, 0, (int)responseLength);
                        responseString = ByteBufferToString(responseBuffer);
                        PrintResponseToDebug(responseString); //UNCOMMENT FOR DEBUGGING
                    }
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("HTTPS Socket Exception");
                Console.WriteException(e);
                Console.WriteLine((e.InnerException as SocketException).ErrorCode.ToString());
            }
            catch (Exception e)//Generally not ideal to catch all exceptions, but here we're logging and don't want the device to stop operating just because it couldn't connect
            {
                Console.WriteLine("HTTPS Request Failed");
                Console.WriteException(e);
            }
            return responseString;
        }

        private string ByteBufferToString(byte[] buffer)
        {
            if (buffer is null || buffer.Length == 0) return string.Empty;

            StringBuilder response = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                response.Append((char)buffer[i]);
            }
            return response.ToString();
        }

        private void PrintResponseToDebug(string responseString)
        {
            Console.WriteLine("--- Response begins ---:");
            var lines = responseString.Split('\n', '\r');
            foreach (var line in lines)
            {
                if (String.IsNullOrEmpty(line)) continue;
                Console.WriteLine(line);
            }
            Console.WriteLine("--- End of response ---");
        }
    }
}
