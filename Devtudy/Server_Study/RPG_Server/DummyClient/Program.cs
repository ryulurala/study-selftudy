using System;
using System.Net;
using System.Threading;
using ServerCore;

namespace DummyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // DNS: Domain Name System: DNS서버가 네트워크 망에 하나가 더 있어서 주소를 찾아준다.
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];  // 분산한 서버에 따라 해당 IP에 여러 개 있을 수도 있다.
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); // 최종 주소, 포트는 식당 정문, 후문 느낌

            Connector connector = new Connector();

            connector.Connect(endPoint,
                () => { return SessionManager.Instance.Generate(); },
            500);

            while (true)
            {
                try
                {
                    SessionManager.Instance.SendForEach();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.ToString());
                }
                Thread.Sleep(250);
            }
        }
    }
}