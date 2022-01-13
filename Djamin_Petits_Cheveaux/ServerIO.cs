using EngineIOSharp.Common;
using EngineIOSharp.Server;
using SocketIOSharp.Server;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace Djamin_Petits_Cheveaux
{
    public class ServerIO : EngineIOServerOption
    {
        public ServerIO(ushort Port, string Path = "/socket.io", bool Secure = false, ulong PingTimeout = 5000, ulong PingInterval = 25000, ulong UpgradeTimeout = 10000, bool Polling = true, bool WebSocket = true, bool AllowUpgrade = true, bool AllowEIO3 = false, bool SetCookie = true, string SIDCookieName = "io", IDictionary<string, string> Cookies = null, Action<HttpListenerRequest, Action<EngineIOException>> AllowHttpRequest = null, Action<WebSocketContext, Action<EngineIOException>> AllowWebSocket = null, object InitialData = null, X509Certificate2 ServerCertificate = null, RemoteCertificateValidationCallback ClientCertificateValidationCallback = null) : base(Port, Path, Secure, PingTimeout, PingInterval, UpgradeTimeout, Polling, WebSocket, AllowUpgrade, AllowEIO3, SetCookie, SIDCookieName, Cookies, AllowHttpRequest, AllowWebSocket, InitialData, ServerCertificate, ClientCertificateValidationCallback)
        {

        }
    }
}
