using System;
using System.Net;
using System.Text;
using ServerCore;

namespace Server
{
    class Program
    {
        static Listener _listener = new Listener();
        public static GameRoom Room = new GameRoom();

        static void Main(string[] args)
        {
            // DNS: Domain Name System: DNS서버가 네트워크 망에 하나가 더 있어서 주소를 찾아준다.
            string host = Dns.GetHostName();
            System.Console.WriteLine("Host = " + host);
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            System.Console.WriteLine("IPHost = " + ipHost.ToString());
            IPAddress ipAddr = ipHost.AddressList[0];  // 분산한 서버에 따라 해당 IP에 여러 개 있을 수도 있다.
            System.Console.WriteLine("IPAddress = " + ipAddr.ToString());
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); // 최종 주소, 포트는 식당 정문, 후문 느낌
            System.Console.WriteLine("EndPoint = " + endPoint.ToString());

            // Initialize
            // 혹시라도 누가 들어오면 OnAcceptHandler로 알려달라
            // 무엇을 만들지 지정만 해달라.(게임 매니저로 or 람다로)
            _listener.init(endPoint, () => { return SessionManager.Instance.Generate(); });      // GameSession을 만든다.
            System.Console.WriteLine("Listening...");

            // 24시간 영업: 무한루프 -> 프로그램이 종료되지 않게 함(아무 일도 하지 않지만)
            while (true) ;
        }
    }
}