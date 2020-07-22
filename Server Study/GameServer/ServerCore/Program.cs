using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        static Listener _listener = new Listener();
        static void OnAcceptHandler(Socket clientSocket)
        {
            // 손님을 입장시킨다
            // Accept의 return: 대리인(Session)의 Socket
            // Blocking 함수: 손님이 입장을 안하면 다음 단계 안 넘어감(계속 대기)
            try
            {
                // 보내는 부분 + 받는 부분 + Disconnect => Session
                Session session = new Session();    // polling 방식도 가능
                session.Start(clientSocket);

                byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to Server");
                session.Send(sendBuff);

                Thread.Sleep(1000);

                session.Disconnect();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }
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
            _listener.init(endPoint, OnAcceptHandler);
            System.Console.WriteLine("Listening...");

            // 24시간 영업: 무한루프 -> 프로그램이 종료되지 않게 함(아무 일도 하지 않지만)
            while (true) ;
        }
    }
}